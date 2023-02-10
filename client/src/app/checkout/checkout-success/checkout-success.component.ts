import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { IOrder } from 'src/app/shared/interfaces/iorder';

@Component({
  selector: 'app-checkout-success',
  templateUrl: './checkout-success.component.html',
  styleUrls: ['./checkout-success.component.scss']
})
export class CheckoutSuccessComponent {
  order?: IOrder

  constructor(private router: Router) {
    const navigation = this.router.getCurrentNavigation();
    this.order = navigation?.extras?.state as IOrder
  }
}
