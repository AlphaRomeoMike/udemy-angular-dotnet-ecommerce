import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { BasketService } from 'src/app/basket/basket.service';
import { IDeliveryMethod } from 'src/app/shared/interfaces/idelivery-methods';
import { CheckoutService } from '../checkout.service';

@Component({
  selector: 'app-checkout-delivery',
  templateUrl: './checkout-delivery.component.html',
  styleUrls: ['./checkout-delivery.component.scss']
})
export class CheckoutDeliveryComponent implements OnInit {
  @Input() checkoutForm?: FormGroup
  deliveryMethods: IDeliveryMethod[] = [];

  constructor(private checkoutSerivce: CheckoutService, private basketService: BasketService) { }

  ngOnInit(): void {
    this.checkoutSerivce.getDeliveryMethods().subscribe({
      next: dm => this.deliveryMethods = dm
    })
  } 

  setShippingPrice(deliveryMethod: IDeliveryMethod) {
    this.basketService.setShippingPrice(deliveryMethod);
  }
}
