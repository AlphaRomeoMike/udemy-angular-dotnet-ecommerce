import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Basket, BasketTotals, IBasket, IBasketItem } from '../shared/interfaces/basket';
import { IProduct } from '../shared/interfaces/iproduct';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  api = environment.url;
  private basketSource = new BehaviorSubject<Basket | null>(null);
  basketSource$ = this.basketSource.asObservable();
  private basketTotalSource = new BehaviorSubject<BasketTotals | null>(null);
  basketTotal$ = this.basketTotalSource.asObservable();

  constructor(private http: HttpClient) { }

  /**
   * # Get Basket
   * ---
   * @description To get all the items in the basket
   * @param string id
   * @returns Observable<Basket>
   */
  getBasket(id: string) {
    return this.http.get<Basket>(`${this.api}basket?id=${id}`)
    .subscribe({
      next: basket => {
        this.basketSource.next(basket);
        this.calculateTotals();
      }
    });
  }

  /**
   * # Add or Update item to basket
   * ---
   * @param IBasket basket
   * @returns
   */
  setBasket(basket: IBasket) {
    return this.http.post<Basket>(`${this.api}basket`, basket)
    .subscribe({
      next: basket => {
        this.basketSource.next(basket);
        this.calculateTotals();
      }
    });
  }

  getCurrentBasketValue() {
    return this.basketSource.value;
  }

  deleteBasket(id: string) {
    return this.http.get<Basket>(`${this.api}basket?id=${id}`)
  }

  addItemToBasket(item: IProduct, quantity = 1) {
    const itemToMap = this.mapProductItemToBasketItem(item);
    const basket = this.getCurrentBasketValue() ?? this.createBasket();
    basket.items = this.addOrUpdateItem(basket.items, itemToMap, quantity);
    this.setBasket(basket);
  }

  private addOrUpdateItem(items: IBasketItem[], itemToAdd: IBasketItem, quantity: number): IBasketItem[] {
    const item = items.find(x => x.id === itemToAdd.id)
    if (item) item.quantity += quantity
    else {
      itemToAdd.quantity = quantity;
      items.push(itemToAdd);
    }
    return items;
  }

  private createBasket(): Basket {
    const basket = new Basket();
    localStorage.setItem('basket_id', basket.id);
    return basket;
  }

  private mapProductItemToBasketItem(item: IProduct): IBasketItem {
    return {
      id: item.id,
      productName: item.name,
      price: item.price,
      quantity: 0,
      brand: item.productBrand,
      type: item.productType,
      pictureUrl: item.pictureUrl
    }
  }

  private calculateTotals () {
    const basket = this.getCurrentBasketValue();
    if (!basket) return;
    const shipping = 0;
    const subtotal = basket.items.reduce((a, b) => (b.price * b.quantity) + a, 0)
    const total = subtotal + shipping;
    this.basketTotalSource.next({
      shipping, total, subtotal
    });
  }
}