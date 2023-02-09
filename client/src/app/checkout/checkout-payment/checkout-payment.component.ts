import { Component, Input } from '@angular/core'
import { FormGroup } from '@angular/forms'
import { NavigationExtras, Router } from '@angular/router'
import { HotToastService } from '@ngneat/hot-toast'
import { BasketService } from 'src/app/basket/basket.service'
import { Basket } from 'src/app/shared/interfaces/basket'
import { IOrderToCreate } from 'src/app/shared/interfaces/iorder'
import { IAddress } from 'src/app/shared/interfaces/iuser'
import { CheckoutService } from '../checkout.service'

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss'],
})
export class CheckoutPaymentComponent {
  @Input() checkoutForm?: FormGroup

  constructor(
    private basketService: BasketService,
    private checkoutService: CheckoutService,
    private toastr: HotToastService,
    private router: Router
  ) {}

  submitOrder() {
    const basket = this.basketService.getCurrentBasketValue()
    if (!basket) return
    const orderToCreate = this.getOrderToCreate(basket)
    if (!orderToCreate) return
    this.checkoutService.createOrder(orderToCreate).subscribe({
      next: (order) => {
        this.toastr.success('Order created succesfully')
        this.basketService.deleteLocalBasket()
        console.log(order)
        const navExtras: NavigationExtras = {
          state: order
        };
        this.router.navigate(['checkout/success'], navExtras);
      },
    })
  }

  getOrderToCreate(basket: Basket) {
    const deliveryMethodId = this.checkoutForm
      ?.get('deliveryForm')
      ?.get('deliveryMethod')?.value
    const shipToAddress = this.checkoutForm?.get('addressForm')
      ?.value as IAddress
    if (!shipToAddress || !deliveryMethodId) return
    return {
      basketId: basket.id,
      deliveryMethodId: +deliveryMethodId,
      shipToAddress: shipToAddress,
    }
  }
}
