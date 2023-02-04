import { Component, OnInit } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasketItem } from 'src/app/shared/interfaces/basket';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {

  constructor(
    public basketService: BasketService
  ) { }

  ngOnInit(): void {
  }

  getCount(items: IBasketItem[]) {
    return items.reduce((sum, item) => sum + item.quantity, 0)
  }

}
