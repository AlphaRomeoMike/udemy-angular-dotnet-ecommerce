import { HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IBrand } from '../shared/interfaces/ibrand';
import { IProduct } from '../shared/interfaces/iproduct';
import { IType } from '../shared/interfaces/itype';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  products: IProduct[];
  brands: IBrand[];
  types: IType[];
  brandIdSelected: number = 0;
  typeIdSelected: number = 0;
  sortSelected = 'name';
  sortOption = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price: Low to High', value: 'priceAsc'},
    {name: 'Price: High to Low', value: 'priceDesc'},
  ]
  constructor(private shopservice: ShopService) { }

  ngOnInit(): void {
    this.getBrands();
    this.getTypes();
    this.getProducts();
  }

  getProducts() {
    this.shopservice.getProducts(this.brandIdSelected, this.typeIdSelected, this.sortSelected).subscribe((response) => {
      this.products = response.data;
    }, (error) => {
      console.log(error);
    });
  }

  getBrands() {
    this.shopservice.getBrands().subscribe((response) => {
      this.brands = [{ id: 0, name: 'All'},...response];
    }, (error) => {
      console.log(error);
    });
  }

  getTypes() {
    this.shopservice.getTypes().subscribe((response) => {
      this.types = [{ id: 0, name: 'All'},...response];
    }, (error) => {
      console.log(error);
    });
  }

  onBrandSelected(brandId: number) {
    this.brandIdSelected = brandId;
    this.getProducts();
  }

  onTypeSelected(typeId: number) {
    this.typeIdSelected = typeId;
    this.getProducts();
  }

  onSortSelected(sort: string) {
    this.sortSelected = sort;
    this.getProducts();
  }
}
