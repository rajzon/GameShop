import { MessagePopupService } from './../../_services/message-popup.service';
import { ProductForCardFromServer } from './../../_models/ProductForCardFromServer';
import { ShopOrderingService } from './../../_services/shop-ordering.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ShopSearchingService } from './../../_services/shop-searching.service';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
// TO DO: change endpoint for getting product, because i do not need for example: Category.Description here
export class ProductCardComponent implements OnInit {
  product: ProductForCardFromServer;

  constructor(private shopSearchingService: ShopSearchingService,
              private shopOrderingService: ShopOrderingService , private route: ActivatedRoute, private router: Router, 
              private messagePopup: MessagePopupService) { }

  ngOnInit() {
    this.getProduct();
  }

  getProduct(): void {
    this.shopSearchingService.getProductForCard(+this.route.snapshot.params['id'])
        .subscribe((response: ProductForCardFromServer) => {
         this.product = response;
         console.log(this.product);
    }, error => {
      this.messagePopup.displayError(error);
    });
  }


  addToBasket() {
    console.log(this.product.stock.quantity);
    this.shopOrderingService.addStockToBasket(this.product.stock.id, this.product.stock.quantity).subscribe( () => {
      console.log('Added Product to Basket TEST');
      this.router.navigate(['/basket']);
    }, error => {
      this.messagePopup.displayError(error);
    });
  }

}
