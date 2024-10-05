import { Component, OnInit } from '@angular/core';
import { IProduct } from '../shared/Models/Iproduct';
import { IBrand } from '../shared/Models/Ibrands';
import { ShopService } from './shop.service';
import { IProductType } from '../shared/Models/IproductType';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  products?: IProduct[];
  brands: IBrand[] = [];
  types: IProductType[] = [];
  brandIdSelected?: number;
  typeIdSelected?: number;
  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts() {
    this.shopService.getProducts(this.brandIdSelected, this.typeIdSelected).subscribe(response => {
      this.products = response?.data;
    }, error => {
      console.log(error);
    }, () => { 'finalizado' })
  }

  getBrands() {
    this.shopService.getBrands().subscribe(response => {
      this.brands = [{id: 0, name: 'All'}, ...response];
    }, error => { console.log(error) })
  }

  getTypes() {
    this.shopService.getTypes().subscribe(response => {
      this.types = [{id: 0, name: 'All'}, ...response];
    }, error => { console.log(error) })
  }

  onBrandSelected(brandId: number) {
    this.brandIdSelected = brandId;
    this.getProducts();
  }

  OnTypeSelected(typeId: number) {
    this.typeIdSelected = typeId;
    this.getProducts();
  }





}
