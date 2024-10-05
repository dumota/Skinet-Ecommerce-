import { Component, Input, OnInit } from '@angular/core';
import { IProduct } from 'src/app/shared/Models/Iproduct';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent implements OnInit{
  @Input() product?: IProduct;

  ngOnInit(): void {
    // throw new Error('Method not implemented.');
  }

}
