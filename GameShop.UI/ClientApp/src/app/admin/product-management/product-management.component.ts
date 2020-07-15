import { Router } from '@angular/router';
import { ProductFromServer } from './../../_models/ProductFromServer';
import { AdminService } from './../../_services/admin.service';
import { Component, OnInit, } from '@angular/core';
import { PaginatedResult, Pagination } from 'src/app/_models/Pagination';

@Component({
  selector: 'app-product-management',
  templateUrl: './product-management.component.html',
  styleUrls: ['./product-management.component.css']
})
export class ProductManagementComponent implements OnInit {
  products: ProductFromServer[];
  createButtonClicked: boolean;
  editButtonClicked: boolean;
  productIdToEdit: Number;
  pagination: Pagination;


  constructor(private adminService: AdminService, private router: Router) { }

  ngOnInit() {
    this.createButtonClicked = false;
    this.editButtonClicked = false;
    const pageNumber = 1;
    const pageSize = 5;
    this.getProducts(pageNumber, pageSize);
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.getProducts(this.pagination.currentPage, this.pagination.itemsPerPage);
  }

  getProducts(pageNumber: number, pageSize: number): void {
    this.adminService.getProducts(pageNumber, pageSize).subscribe((products: PaginatedResult<Array<ProductFromServer>>) => {
      this.products = products.result;
      this.pagination = products.pagination;
    }, error => {
      console.log(error);
    });
  }

  deleteProduct(id: Number): void {
    this.adminService.deleteProduct(id).subscribe(() => {
      console.log('Porduct of Id:' + id + 'has been deleted');
      this.refreshComponent();
    }, error => {
      console.log(error);
    });
  }

  editProductButtonSelected(id: Number): void {
    this.editButtonClicked = true;
    this.productIdToEdit = id;
  }

  createProductButtonSelected(): void {
    this.createButtonClicked = true;
  }

  canceledOrSuccesfullCreation(creationMode: boolean): void {
    this.createButtonClicked = creationMode;
    this.refreshComponent();
  }

  canceledOrSuccesfullEdit(creationMode: boolean): void {
    this.editButtonClicked = creationMode;
    this.refreshComponent();
  }

  private refreshComponent(): void {
    this.router.navigateByUrl('/RefreshComponent', { skipLocationChange: true }).then(() => {
      this.router.navigate(['/admin']);
     });
  }

}
