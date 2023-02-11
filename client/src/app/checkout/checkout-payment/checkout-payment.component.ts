import { Component, ElementRef, Input, ViewChild, OnInit } from '@angular/core'
import { FormGroup } from '@angular/forms'
import { NavigationExtras, Router } from '@angular/router'
import { HotToastService } from '@ngneat/hot-toast'
import { loadStripe, Stripe } from '@stripe/stripe-js'
import {
  StripeCardCvcElement,
  StripeCardExpiryElement,
  StripeCardNumberElement,
} from '@stripe/stripe-js/types/stripe-js'
import { firstValueFrom } from 'rxjs'
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
export class CheckoutPaymentComponent implements OnInit {
  @Input() checkoutForm?: FormGroup
  @ViewChild('cardNumber') cardNumberElement?: ElementRef
  @ViewChild('cardExpiry') cardExpiryElement?: ElementRef
  @ViewChild('cardCvc') cardCvcElement?: ElementRef
  stripe: Stripe | null = null
  cardNumber?: StripeCardNumberElement
  cardExpiry?: StripeCardExpiryElement
  cardCvc?: StripeCardCvcElement

  cardNumberComplete = false
  cardExpiryComplete = false
  cardCvcComplete = false

  cardErrors: any
  loading = false

  constructor(
    private basketService: BasketService,
    private checkoutService: CheckoutService,
    private toastr: HotToastService,
    private router: Router,
  ) { }

  ngOnInit(): void {
    loadStripe(
      'pk_test_51MZgDAINlpvfEnr6eagVfV3lqJtsw9sMCTCY8I7b1o6rxZSd0Do8ypdKuxibeGWdo5L5q10Xt1KICo7JQGwRvzgu00HMWCJDPI',
    ).then((stripe) => {
      this.stripe = stripe
      const elements = stripe?.elements()
      if (elements) {
        // For card number
        this.cardNumber = elements.create('cardNumber')
        this.cardNumber.mount(this.cardNumberElement?.nativeElement)
        this.cardNumber.on('change', (event) => {
          this.cardNumberComplete = event.complete
          if (event.error) this.cardErrors = event.error.message
          else this.cardErrors = null
        })

        // For card expiry
        this.cardExpiry = elements.create('cardExpiry')
        this.cardExpiry.mount(this.cardExpiryElement?.nativeElement)
        this.cardExpiry.on('change', (event) => {
          this.cardExpiryComplete = event.complete
          if (event.error) this.cardErrors = event.error.message
          else this.cardErrors = null
        })

        // For card cvc
        this.cardCvc = elements.create('cardCvc')
        this.cardCvc.mount(this.cardCvcElement?.nativeElement)
        this.cardCvc.on('change', (event) => {
          this.cardCvcComplete = event.complete
          if (event.error) this.cardErrors = event.error.message
          else this.cardErrors = null
        })
      }
    })
  }

  get paymentFormComplete() {
    return (
      this.checkoutForm.get('paymentForm').valid &&
      this.cardNumberComplete &&
      this.cardExpiryComplete &&
      this.cardCvcComplete
    )
  }
  async submitOrder() {
    this.loading = true
    const basket = this.basketService.getCurrentBasketValue()
    if (!basket) throw new Error('Cannot get basket');
    try {
      const createdOrder = await this.createOrder(basket)
      const paymentResult = await this.confirmPaymentWithStripe(basket)
      if (paymentResult.paymentIntent) {
        this.basketService.deleteBasket(basket.id);
        console.log(createdOrder)
        const navExtras: NavigationExtras = {
          state: createdOrder,
        }
        this.router.navigate(['checkout/success'], navExtras)
      } else {
        this.toastr.error(paymentResult.error.message, {
          autoClose: false,
          dismissible: true,
          icon: '❌',
          theme: 'snackbar',
          position: 'top-right',
        })
      }
    } catch (error) {
      console.log(error)
      this.toastr.error(error.message, {
        autoClose: true,
        dismissible: true,
        icon: '❌',
        theme: 'snackbar',
        position: 'top-right',
      })
    } finally {
      this.loading = false
    }
  }

  private async confirmPaymentWithStripe(basket: Basket | null) {
    if (!basket) throw new Error('Basket is null.')
    const result = this.stripe.confirmCardPayment(basket.clientSecret!, {
      payment_method: {
        card: this.cardNumber,
        billing_details: {
          name: this.checkoutForm.get('paymentForm')?.get('nameOnCard')?.value,
        },
      },
    })
    if (!result) throw new Error('Problem attempting payment with Stripe')
    return result
  }

  private async createOrder(basket: Basket | null) {
    if (!basket) throw new Error('Basket is null.')
    const orderToCreate = this.getOrderToCreate(basket)
    return firstValueFrom(this.checkoutService.createOrder(orderToCreate))
  }

  getOrderToCreate(basket: Basket): IOrderToCreate {
    const deliveryMethodId = this.checkoutForm
      ?.get('deliveryForm')
      ?.get('deliveryMethod')?.value
    const shipToAddress = this.checkoutForm?.get('addressForm')
      ?.value as IAddress
    if (!shipToAddress || !deliveryMethodId)
      throw new Error('Something went wrong with basket')
    return {
      basketId: basket.id,
      deliveryMethodId: +deliveryMethodId,
      shipToAddress: shipToAddress,
    }
  }
}
