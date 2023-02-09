import { IAddress } from "./iuser";

export interface IShippedToAddress {
  firstName: string;
  lastName: string;
  street: string;
  city: string;
  state: string;
  zipCode: string;
}

export interface IOrderItem {
  productId: number;
  productName: string;
  pictureUrl: string;
  price: number;
  quantity: number;
}

export interface IOrder {
  id: number;
  buyerEmail: string;
  orderDate: Date;
  shippedToAddress: IShippedToAddress;
  shippingPrice: number;
  deliveryMethod: string;
  orderItems: IOrderItem[];
  subTotal: number;
  total: number;
  status: string;
}

export interface IOrderToCreate {
  basketId: string;
  deliveryMethodId: number;
  shipToAddress: IAddress;
}
