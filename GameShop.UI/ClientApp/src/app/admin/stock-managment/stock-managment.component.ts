import { MessagePopupService } from './../../_services/message-popup.service';
import { AdminService } from './../../_services/admin.service';
import { Component, OnInit } from '@angular/core';
import { PaginatedResult, Pagination } from 'src/app/_models/Pagination';
import { ProductWithStockFromServer } from 'src/app/_models/ProductWithStockFromServer';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-stock-managment',
  templateUrl: './stock-managment.component.html',
  styleUrls: ['./stock-managment.component.css']
})
export class StockManagmentComponent implements OnInit {
  products: ProductWithStockFromServer[];
  pagination: Pagination;

  pageNumber = environment.pageNumber;
  pageSize = environment.pageSize;

  constructor(private adminService: AdminService, private messagePopup: MessagePopupService) { }

  ngOnInit() {
    this.getProductsWithStock(this.pageNumber, this.pageSize);
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.getProductsWithStock(this.pagination.currentPage, this.pagination.itemsPerPage);
  }

  getProductsWithStock(pageNumber: number, pageSize: number): void {
    this.adminService.getProductsWithStock(pageNumber, pageSize)
        .subscribe((products: PaginatedResult<Array<ProductWithStockFromServer>>) =>
    {
      this.products = products.result;
      this.pagination = products.pagination;
    }, error => {
      this.messagePopup.displayError(error);
    });
  }

  editStockForProduct(productid: number, stockQuantity: number) {
      this.adminService.editStockForProduct(productid, stockQuantity)
            .subscribe(() => {
              this.messagePopup.displaySuccess(`Successfully changed stock for productId: ${productid}`);
            }, error => {
              this.messagePopup.displayError(error);
            });
  }
}
