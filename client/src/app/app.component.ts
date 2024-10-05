import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IProduct } from './shared/Models/Iproduct';
import { IPagination } from './shared/Models/Ipagination';


//decorator que fala que este cara é um componente angular
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
 title:string = 'Skinet';

 constructor(){}

  ngOnInit(): void {
   
  }
}
