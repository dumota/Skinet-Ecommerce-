import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IProduct } from '../shared/Models/Iproduct';
import { IBrand } from '../shared/Models/Ibrands';
import { ShopService } from './shop.service';
import { IProductType } from '../shared/Models/IproductType';
import { ShopParams } from '../shared/Models/shopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  @ViewChild('search',{static: true}) searchTerm!: ElementRef;
  products?: IProduct[];
  brands: IBrand[] = [];
  types: IProductType[] = [];
  shopParams = new ShopParams();
  totalCount: number = 0;

  sortOptions =[
    {name: 'Alphabetical', value : 'name'},
    {name: 'Price: Low to High', value : 'priceAsc'},
    {name: 'Price: High to Low', value : 'name'}
  ]


  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe(response => {
      this.products = response?.data;
      this.shopParams.pageNumber = response?.pageIndex as number;
      this.shopParams.pageSize = response?.pageSize as number;
      this.totalCount = response?.count as number;
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
    this.shopParams.brandId = brandId;
    this.shopParams.pageNumber =1;
    this.getProducts();
  }

  OnTypeSelected(typeId: number) {
    this.shopParams.typeId = typeId;
    this.shopParams.pageNumber =1;
    this.getProducts();
  }

  onSortSelected(event: Event){
    let filterValue = (event.target as HTMLInputElement).value;
    this.shopParams.sort = filterValue;
    this.getProducts();
  }

  //action para paginação. mas iremos ter que fazer este componente ainda
  onPageChanged(event:any){
    // console.log(event);
    if(event !== this.shopParams.pageNumber){
      this.shopParams.pageNumber = event;
      this.getProducts();
    }
  }

  onSearch(){
    this.shopParams.search = this.searchTerm?.nativeElement.value;
    this.shopParams.pageNumber =1;
    this.getProducts();
  }

  onReset(){
    this.searchTerm.nativeElement.value = "";
    this.shopParams = new ShopParams();
    this.getProducts();
  }



}
