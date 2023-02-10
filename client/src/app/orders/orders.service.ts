import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IOrder } from '../shared/interfaces/iorder';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {
  baseUrl = environment.url;

  constructor(private http: HttpClient) { }

  getOrdersForUser() {
    return this.http.get<IOrder[]>(this.baseUrl + 'orders');
  }

  getOrderDetailed(id: number) {
    return this.http.get<IOrder>(this.baseUrl + 'orders/' + id);
  }
}
