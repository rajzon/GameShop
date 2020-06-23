import { Pagination } from './../_models/Pagination';
import { Photo } from './../_models/Photo';
import { ProductForSearching } from './../_models/ProductForSearching';
import { ShopSearchingService } from './../_services/shop-searching.service';
import { Component, OnInit } from '@angular/core';
import { PaginatedResult } from '../_models/Pagination';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  products = Array<ProductForSearching>();
  pagination: Pagination;


  constructor(private shopSearchingService: ShopSearchingService) { }

  ngOnInit() {
    const pageNumber = 1;
    const pageSize = 5;
    this.getProductsForSearching(pageNumber, pageSize);
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.getProductsForSearching(this.pagination.currentPage, this.pagination.itemsPerPage);
  }

  getProductsForSearching(currentPageNumber: number, pageSize: number) {
    this.shopSearchingService.getProductsForSearching(currentPageNumber, pageSize).subscribe(
        (next: PaginatedResult<Array<ProductForSearching>>) => {
      this.pagination = next.pagination;
      this.products = next.result;
      this.initPhotoIfItsNull();
      console.log(this.products);
    }, error => {
      console.log(error);
    });

    console.log(this.pagination);

  }

  initPhotoIfItsNull() {
    this.products.forEach(element => {
      if (!element.photo) {
        element.photo = <Photo>{ };
      }
    });
  }
} 
