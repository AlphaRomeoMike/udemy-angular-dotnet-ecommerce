import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { ShopComponent } from './shop.component';

const Routes: Routes = [
  { path: '', component: ShopComponent},
  { path: ':id', component: ProductDetailsComponent, data : { breadcrumb: { alias: 'productDetails' } }},
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(Routes)
  ],
  exports: [
    RouterModule
  ]
})
export class ShopRoutingModule { }
