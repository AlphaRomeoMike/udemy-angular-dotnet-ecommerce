import { HttpParams } from '@angular/common/http';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IBrand } from '../shared/interfaces/ibrand';
import { IProduct } from '../shared/interfaces/iproduct';
import { IType } from '../shared/interfaces/itype';
import { ShopParams } from '../shared/interfaces/ShopParams';
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
  shopParams = new ShopParams();
  sortOption = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low to High', value: 'priceAsc' },
    { name: 'Price: High to Low', value: 'priceDesc' },
  ];
  totalCount = 0;
  @ViewChild('search') searchTerm?: ElementRef;

  constructor(private shopservice: ShopService) { }

  ngOnInit(): void {
    this.getBrands();
    this.getTypes();
    this.getProducts();
  }

  getProducts() {
    this.shopservice.getProducts(this.shopParams).subscribe((response) => {
      this.products = response.data;
      this.shopParams.pageNumber = response.pageIndex;
      this.shopParams.pageSize = response.pageSize;
      this.totalCount = response.count;
    }, (error) => {
      console.log(error);
    });
  }

  getBrands() {
    this.shopservice.getBrands().subscribe((response) => {
      this.brands = [{ id: 0, name: 'All' }, ...response];
    }, (error) => {
      console.log(error);
    });
  }

  getTypes() {
    this.shopservice.getTypes().subscribe((response) => {
      this.types = [{ id: 0, name: 'All' }, ...response];
    }, (error) => {
      console.log(error);
    });
  }

  onBrandSelected(brandId: number) {
    this.shopParams.brandId = brandId;
    this.shopParams.pageNumber = 1
    this.getProducts();
  }

  onTypeSelected(typeId: number) {
    this.shopParams.typeId = typeId;
    this.shopParams.pageNumber = 1
    this.getProducts();
  }

  onSortSelected(sort: string) {
    this.shopParams.sort = sort;
    this.getProducts();
  }

  onPageChanged(event: any) {
    if (this.shopParams.pageNumber != event) {
      this.shopParams.pageNumber = event;
      this.getProducts();
    }
  }

  onSearch() {
    this.shopParams.search = this.searchTerm?.nativeElement.value;
    this.shopParams.pageNumber = 1
    this.getProducts();
  }

  onReset() {
    if (this.searchTerm) this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProducts();
  }
}
