import { Router } from '@angular/router';
import { ProductFromServer } from './../../_models/ProductFromServer';
import { AdminService } from './../../_services/admin.service';
import { Component, OnInit, } from '@angular/core';


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


  constructor(private adminService: AdminService, private router: Router) { }

  ngOnInit() {
    this.createButtonClicked = false;
    this.editButtonClicked = false;
    this.getProducts();
  }

  getProducts() {
    this.adminService.getProducts().subscribe((products: ProductFromServer[]) => {
      this.products = products;
    }, error => {
      console.log(error);
    });
  }

  deleteProduct(id: Number) {
    this.adminService.deleteProduct(id).subscribe(() => {
      console.log('Porduct of Id:' + id + 'has been deleted');
      this.router.navigateByUrl('/RefreshComponent', { skipLocationChange: true }).then(() => {
        this.router.navigate(['/admin']);
       });
    }, error => {
      console.log(error);
    });
    
  }

  editProductButtonSelected(id: Number) {
    this.editButtonClicked = true;
    // this.productIdToEdit = id;
    this.productIdToEdit = id;
  }

  createProductButtonSelected() {
    this.createButtonClicked = true;
  }

  canceledOrSuccesfullCreation(creationMode: boolean) {
    this.createButtonClicked = creationMode;
    this.router.navigateByUrl('/RefreshComponent', { skipLocationChange: true }).then(() => {
      this.router.navigate(['/admin']);
     });
  }

  canceledOrSuccesfullEdit(creationMode: boolean) {
    this.editButtonClicked = creationMode;
    this.router.navigateByUrl('/RefreshComponent', { skipLocationChange: true }).then(() => {
      this.router.navigate(['/admin']);
     });
  }

}
