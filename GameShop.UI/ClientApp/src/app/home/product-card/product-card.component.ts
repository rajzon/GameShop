import { ActivatedRoute } from '@angular/router';
import { ShopSearchingService } from './../../_services/shop-searching.service';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent implements OnInit {
  product: any;

  constructor(private shopSearchingService: ShopSearchingService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.getProduct();
  }

  getProduct() {
    this.shopSearchingService.getProductForCard(+this.route.snapshot.params['id'])
        .subscribe(response => {
         this.product = response;
         console.log(this.product);
    }, error => {
      console.log(error);
    });
  }

}
