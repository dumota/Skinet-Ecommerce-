import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPagination } from '../shared/Models/Ipagination';
import { IBrand } from '../shared/Models/Ibrands';
import { IProductType } from '../shared/Models/IproductType';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:7092/api/'

  constructor(private http: HttpClient) { }

  getProducts(brandId?: number, typeId?:number){
    //para passar parametros na url precisamos passar o valor da variavel com o nome correto "params"
    let params = new HttpParams();
    if(brandId){
      params = params.append('brandId', brandId.toString());
    }
    if(typeId){
      params = params.append('typeId', typeId.toString());
    }
    return this.http.get<IPagination>(this.baseUrl + 'product', {
      observe: 'response',
      params
      })
      .pipe(
        map(response => {
          return response.body;
        })
      );
  }

  getBrands(){
    return this.http.get<IBrand[]>(this.baseUrl+ 'product/brands');
  }
  getTypes(){
    return this.http.get<IProductType[]>(this.baseUrl+ 'product/types');
  }
}
