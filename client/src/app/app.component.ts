import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IProduct } from './interfaces/iproduct';
import { IPagination } from './interfaces/ipagination';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'client';
  products: IProduct[];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.http.get('http://localhost:5001/api/product').subscribe((response: IPagination) => {
      this.products = response.data
    }, (err) => {
      console.log(err);
    });
  }
}
