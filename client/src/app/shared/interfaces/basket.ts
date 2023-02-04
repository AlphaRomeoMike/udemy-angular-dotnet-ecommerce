import * as cuid from "cuid";

export interface IBasket {
  id: string
  items: IBasketItem[]
}

export interface IBasketItem {
  id: number
  productName: string
  price: number
  quantity: number
  pictureUrl: string
  brand: string
  type: string
}

export class Basket implements IBasket {
  id = cuid();
  items: IBasketItem[] = [];
}

export interface BasketTotals {
  shipping: number;
  subtotal: number;
  total: number;
}
