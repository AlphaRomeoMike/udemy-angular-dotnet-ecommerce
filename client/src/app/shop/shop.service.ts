import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IBrand } from '../shared/interfaces/ibrand';
import { IPagination } from '../shared/interfaces/ipagination';
import { IType } from '../shared/interfaces/itype';

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
  getProducts(brandId?: number, typeId?: number, sortOption? : string) {
    let params = new HttpParams();
    if (brandId) {
      params = params.append('brandId', brandId.toString());
    }
    if (typeId) {
      params = params.append('typeId', typeId.toString());
    }
    if (sortOption) {
      params = params.append('sort', sortOption);
    }
    return this.http.get<IPagination>(this.url + 'product?pageSize=50', { observe: 'response', params })
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
}
