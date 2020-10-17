import { MessagePopupService } from './../_services/message-popup.service';
import { Pagination } from './../_models/Pagination';
import { Photo } from './../_models/Photo';
import { ProductForSearching } from './../_models/ProductForSearching';
import { ShopSearchingService } from './../_services/shop-searching.service';
import { Component, OnInit } from '@angular/core';
import { PaginatedResult } from '../_models/Pagination';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  products = Array<ProductForSearching>();
  pagination: Pagination;


  constructor(private shopSearchingService: ShopSearchingService, private messagePopup: MessagePopupService) { }

  ngOnInit() {
    const pageNumber = environment.pageNumber;
    const pageSize = environment.pageSize;
    this.getProductsForSearching(pageNumber, pageSize);
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.getProductsForSearching(this.pagination.currentPage, this.pagination.itemsPerPage);
  }

  getProductsForSearching(currentPageNumber: number, pageSize: number): void {
    this.shopSearchingService.getProductsForSearching(currentPageNumber, pageSize).subscribe(
        (next: PaginatedResult<Array<ProductForSearching>>) => {
      this.pagination = next.pagination;
      this.products = next.result;
      this.initPhotoIfItsNull();
      console.log(this.products);
    }, error => {
      this.messagePopup.displayError(error);
    });

    console.log(this.pagination);

  }

  private initPhotoIfItsNull(): void {
    this.products.forEach(element => {
      if (!element.photo) {
        element.photo = <Photo>{ };
      }
    });
  }
}
