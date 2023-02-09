import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { HotToastService } from '@ngneat/hot-toast';
import { AccountService } from 'src/app/account/account.service';

@Component({
  selector: 'app-checkout-address',
  templateUrl: './checkout-address.component.html',
  styleUrls: ['./checkout-address.component.scss']
})
export class CheckoutAddressComponent {
  @Input() checkoutForm: FormGroup
  
  constructor (private accountService: AccountService, private toast: HotToastService) { }

  saveUserAddress() {
    this.accountService.updateUserAddress(this.checkoutForm?.get('addressForm')?.value).subscribe({
      next: () => {
        this.toast.success('Address saved');
        this.checkoutForm.get('addressForm')?.reset(this.checkoutForm?.get('addressForm')?.value);

      }
    })
  }
}
