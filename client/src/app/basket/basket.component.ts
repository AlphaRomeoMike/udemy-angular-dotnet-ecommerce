import { Component } from '@angular/core';
import { IBasketItem } from '../shared/interfaces/basket';
import { BasketService } from './basket.service';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent {

  constructor(public basketService: BasketService) { }

  increment(item: IBasketItem) {
    this.basketService.addItemToBasket(item);
  }

  remove(id: number) {
    this.basketService.removeItemFromBasket(id);
  }
}
