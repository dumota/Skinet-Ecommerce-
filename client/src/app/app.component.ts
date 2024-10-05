import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IProduct } from './Models/Iproduct';
import { IPagination } from './Models/Ipagination';

//decorator que fala que este cara é um componente angular
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
 title:string = 'Skinet';
 products:IProduct[] = [];

 constructor(private http: HttpClient){}

  ngOnInit(): void {
    this.http.get('https://localhost:7092/api/Product?pageSize=50').subscribe(
      (response: any)=>{
      console.log(response);
      this.products = response.data;
      
    }, error =>{
      console.log(error);
      
    })
  }
}
