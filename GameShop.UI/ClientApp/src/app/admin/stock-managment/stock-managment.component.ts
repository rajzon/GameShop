import { AdminService } from './../../_services/admin.service';
import { Component, OnInit } from '@angular/core';
import { PaginatedResult, Pagination } from 'src/app/_models/Pagination';
import { ProductWithStockFromServer } from 'src/app/_models/ProductWithStockFromServer';

@Component({
  selector: 'app-stock-managment',
  templateUrl: './stock-managment.component.html',
  styleUrls: ['./stock-managment.component.css']
})
export class StockManagmentComponent implements OnInit {
  products: ProductWithStockFromServer[];
  pagination: Pagination;

  pageNumber = 1;
  pageSize = 5;

  constructor(private adminService: AdminService) { }

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
      console.log(error);
    });
  }

  editStockForProduct(productid: number, stockQuantity: number) {
      this.adminService.editStockForProduct(productid, stockQuantity)
            .subscribe(() => {
              console.log(`Successfully changed stock for productId: ${productid}`);
            }, error => {
              console.log(error);
            });
  }
}
