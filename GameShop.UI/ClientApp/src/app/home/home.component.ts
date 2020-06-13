import { Photo } from './../_models/Photo';
import { ProductForSearching } from './../_models/ProductForSearching';
import { ShopSearchingService } from './../_services/shop-searching.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  products = Array<ProductForSearching>();

  constructor(private shopSearchingService: ShopSearchingService) { }

  ngOnInit() {
    this.getProductsForSearching();
  }

  getProductsForSearching() {
    this.shopSearchingService.getProductsForSearching().subscribe( (next: Array<ProductForSearching>) => {
      this.products = next;
      this.initPhotoIfItsNull();
      console.log(this.products);
    }, error => {
      console.log(error);
    });
  }

  initPhotoIfItsNull() {
    this.products.forEach(element => {
      if (!element.photo) {
        element.photo = <Photo>{ };
      }
    });
  }
} 
