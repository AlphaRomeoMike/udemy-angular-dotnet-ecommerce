import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core'
import { map } from 'rxjs'
import { environment } from 'src/environments/environment'
import { IDeliveryMethod } from '../shared/interfaces/idelivery-methods'
import { IOrder, IOrderToCreate } from '../shared/interfaces/iorder'

@Injectable({
  providedIn: 'root',
})
export class CheckoutService {
  url = environment.url

  constructor(private http: HttpClient) {}

  getDeliveryMethods() {
    return this.http
      .get<IDeliveryMethod[]>(this.url + 'orders/DeliveryMethods')
      .pipe(
        map((dm) => {
          return dm.sort((a, b) => b.price - a.price)
        }),
      )
  }

  createOrder(order: IOrderToCreate) {
    return this.http.post<IOrder>(this.url + 'orders', order);
  }
}
