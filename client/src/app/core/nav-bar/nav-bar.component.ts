import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasketItem } from 'src/app/shared/interfaces/basket';
import { IUser } from 'src/app/shared/interfaces/iuser';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {

  constructor(
    public basketService: BasketService,
    public accountService: AccountService
  ) { }

  ngOnInit(): void { }

  getCount(items: IBasketItem[]) {
    return items.reduce((sum, item) => sum + item.quantity, 0)
  }
}
