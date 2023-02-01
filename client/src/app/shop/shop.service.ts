import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IBrand } from '../shared/interfaces/ibrand';
import { IPagination } from '../shared/interfaces/ipagination';
import { IProduct } from '../shared/interfaces/iproduct';
import { IType } from '../shared/interfaces/itype';
import { ShopParams } from '../shared/interfaces/ShopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  url = environment.url;
  constructor(private http: HttpClient) { }

  /**
   * # Get all products
   * ---
   * @description This method gets all products
   * @returns Obserervable<IPagination>
   */
  getProducts(shopParams: ShopParams) {
    let params = new HttpParams();
    if (shopParams.brandId && shopParams.brandId != 0) {
      params = params.append('brandId', shopParams.brandId.toString());
    }
    if (shopParams.typeId && shopParams.typeId != 0) {
      params = params.append('typeId', shopParams.typeId.toString());
    }
    params = params.append('sort', shopParams.sort);
    params = params.append('pageIndex', shopParams.pageNumber.toString());
    params = params.append('pageSize', shopParams.pageSize.toString());
    if (shopParams.search) params = params.append('search', shopParams.search);
    return this.http.get<IPagination>(this.url + 'product', { observe: 'response', params })
      .pipe(
        map(
          response => { return response.body }
        )
      );
  }

  /**
   * # Get all brands
   * ---
   * @description This method gets all brands
   * @returns Observable<IBrand[]>
   */
  getBrands() {
    return this.http.get<IBrand[]>(this.url + 'product/brands');
  }

  /**
   * # Get all Types
   * ---
   * @description This method gets all types
   * @returns Observable<IType[]>
   */
  getTypes() {
    return this.http.get<IType[]>(this.url + 'product/types');
  }

  getProduct(id: number) {
    return this.http.get<IProduct>(this.url + 'product/' + id);
  }
}
