import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasketItem } from '../interfaces/basket';

@Component({
  selector: 'app-basket-summary',
  templateUrl: './basket-summary.component.html',
  styleUrls: ['./basket-summary.component.scss']
})
export class BasketSummaryComponent implements OnInit {

  @Output() addItem = new EventEmitter<IBasketItem>()
  @Output() removeItem = new EventEmitter<{id: number, quantity: number}>()
  @Input() isBasket = true;

  constructor(public basketService: BasketService) { }

  ngOnInit(): void {
  }

  addBasketItem(item: IBasketItem) {
    this.addItem.emit(item);
  }

  removeBasketItem(id: number, quantity = 1) {
    this.removeItem.emit({id, quantity});
  }
}
