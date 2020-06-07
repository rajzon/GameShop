import { ProductForSearching } from './../_models/ProductForSearching';
import { ShopSearchingService } from './../_services/shop-searching.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  products = <ProductForSearching>{};

  constructor(private shopSearchingService: ShopSearchingService) { }

  ngOnInit() {
    this.getProductsForSearching();
  }

  getProductsForSearching() {
    this.shopSearchingService.getProductsForSearching().subscribe( (next: ProductForSearching) => {
      this.products = next;
      console.log(this.products);
    }, error => {
      console.log(error);
    });
  }
}
