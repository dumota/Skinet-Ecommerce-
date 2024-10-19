import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPagination } from '../shared/Models/Ipagination';
import { IBrand } from '../shared/Models/Ibrands';
import { IProductType } from '../shared/Models/IproductType';
import { map } from 'rxjs/operators';
import { ShopParams } from '../shared/Models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:7092/api/'

  constructor(private http: HttpClient) { }

  getProducts(shopParams: ShopParams){
    //para passar parametros na url precisamos passar o valor da variavel com o nome correto "params"
    let params = new HttpParams();
    if(shopParams.brandId !==0){
      params = params.append('brandId', shopParams.brandId.toString());
    }
    if(shopParams.typeId !==0){
      params = params.append('typeId', shopParams.typeId.toString());
    }

    if(shopParams.search){
      params = params.append('search', shopParams.search)
    }
    params = params.append('sort', shopParams.sort);
    params = params.append('PageIndex', shopParams.pageNumber.toString())
    params = params.append('PageSize', shopParams.pageSize.toString())
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
