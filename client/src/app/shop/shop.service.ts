import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPagination } from '../shared/Models/Ipagination';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:7092/api/'

  constructor(private http: HttpClient) { }

  getProducts(){
    return this.http.get<IPagination>(this.baseUrl + 'product?pageSize=50');
  }
}
