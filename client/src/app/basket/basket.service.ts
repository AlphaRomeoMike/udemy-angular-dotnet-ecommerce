import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core'
import { BehaviorSubject, map } from 'rxjs'
import { environment } from 'src/environments/environment'
import {
  Basket,
  BasketTotals,
  IBasket,
  IBasketItem,
} from '../shared/interfaces/basket'
import { IDeliveryMethod } from '../shared/interfaces/idelivery-methods'
import { IProduct } from '../shared/interfaces/iproduct'

@Injectable({
  providedIn: 'root',
})
export class BasketService {
  api = environment.url
  private basketSource = new BehaviorSubject<Basket | null>(null)
  basketSource$ = this.basketSource.asObservable()
  private basketTotalSource = new BehaviorSubject<BasketTotals | null>(null)
  basketTotal$ = this.basketTotalSource.asObservable()

  constructor(private http: HttpClient) {}

  setShippingPrice(deliveryMethod: IDeliveryMethod) {
    const basket = this.getCurrentBasketValue()
    if (basket) {
      basket.shippingPrice = deliveryMethod.price
      basket.deliveryMethodId = deliveryMethod.id
      this.setBasket(basket)
    }
  }

  /**
   * # Get Basket
   * ---
   * @description To get all the items in the basket
   * @param string id
   * @returns Observable<Basket>
   */
  getBasket(id: string) {
    return this.http.get<Basket>(`${this.api}basket?id=${id}`).subscribe({
      next: (basket) => {
        this.basketSource.next(basket)
        this.calculateTotals()
      },
    })
  }

  /**
   * # Add or Update item to basket
   * ---
   * @param IBasket basket
   * @returns
   */
  setBasket(basket: IBasket) {
    return this.http.post<Basket>(`${this.api}basket`, basket).subscribe({
      next: (basket) => {
        this.basketSource.next(basket)
        this.calculateTotals()
      },
    })
  }

  getCurrentBasketValue() {
    return this.basketSource.value
  }

  deleteBasket(id: string) {
    return this.http.delete(`${this.api}basket?id=${id}`).subscribe({
      next: () => {
        this.basketSource.next(null)
        this.basketTotalSource.next(null)
        localStorage.removeItem('basket_id')
      },
    })
  }

  addItemToBasket(item: IProduct | IBasketItem, quantity = 1) {
    if (this.isProduct(item)) item = this.mapProductItemToBasketItem(item)
    const basket = this.getCurrentBasketValue() ?? this.createBasket()
    basket.items = this.addOrUpdateItem(basket.items, item, quantity)
    this.setBasket(basket)
  }

  removeItemFromBasket(id: number, quantity = 1) {
    const basket = this.getCurrentBasketValue()
    if (!basket) return
    const item = basket.items.find((x) => x.id === id)
    if (item) {
      item.quantity -= quantity
      if (item.quantity == 0) {
        basket.items = basket.items.filter((x) => x.id != id)
      }
      if (basket.items.length > 0) this.setBasket(basket)
      else {
        this.deleteBasket(basket.id)
      }
    }
  }

  private addOrUpdateItem(
    items: IBasketItem[],
    itemToAdd: IBasketItem,
    quantity: number,
  ): IBasketItem[] {
    const item = items.find((x) => x.id === itemToAdd.id)
    if (item) item.quantity += quantity
    else {
      itemToAdd.quantity = quantity
      items.push(itemToAdd)
    }
    return items
  }

  private createBasket(): Basket {
    const basket = new Basket()
    localStorage.setItem('basket_id', basket.id)
    return basket
  }

  private mapProductItemToBasketItem(item: IProduct): IBasketItem {
    return {
      id: item.id,
      productName: item.name,
      price: item.price,
      quantity: 0,
      brand: item.productBrand,
      type: item.productType,
      pictureUrl: item.pictureUrl,
    }
  }

  private calculateTotals() {
    const basket = this.getCurrentBasketValue()
    if (!basket) return
    const subtotal = basket.items.reduce((a, b) => b.price * b.quantity + a, 0)
    const total = subtotal + basket.shippingPrice;
    this.basketTotalSource.next({
      shipping: basket.shippingPrice,
      total,
      subtotal,
    })
  }

  private isProduct(item: IProduct | IBasketItem): item is IProduct {
    return (item as IProduct).productBrand != undefined
  }

  deleteLocalBasket() {
    this.basketSource.next(null)
    this.basketTotalSource.next(null)
    localStorage.removeItem('basket_id')
  }

  createPaymentIntent() {
    return this.http
      .post<Basket>(
        this.api + 'payment/' + this.getCurrentBasketValue()?.id,
        {},
      )
      .pipe(
        map((basket) => {
          this.basketSource.next(basket)
          console.log(basket)
        }),
      )
  }
}
