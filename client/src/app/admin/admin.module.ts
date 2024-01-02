import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { LoginComponent } from './components/login/login.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    LoginComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    SharedModule
  ]
})
export class AdminModule { }
