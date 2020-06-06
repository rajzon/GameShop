import { Product } from './../../_models/Product';
import { SubCategory } from './../../_models/SubCategory';
import { Languague } from './../../_models/Languague';
import { Category } from './../../_models/Category';
import { AdminService } from './../../_services/admin.service';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { RequirementsModalComponent } from '../requirements-modal/requirements-modal.component';
import { Requirements } from 'src/app/_models/Requirements';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.css']
})
export class EditProductComponent implements OnInit {
  @Input() productIdFromProductsPanel: any;
  @Output() editMode = new EventEmitter();
  bsModalRef: BsModalRef;
  model: any = {};
  categories: Category[];
  languages: Languague[];
  subCategories: SubCategory[];
  //product: Product;

  constructor(private adminService: AdminService, private modalService: BsModalService) { }

  ngOnInit() {
    console.log(this.productIdFromProductsPanel);
    this.getProductForEdit(this.productIdFromProductsPanel);
    this.getCategories();
    this.getSubCategories();
    this.getLanguages();
  }


  createRequirementsModal() {

    if( this.model.requirements != null ) {
      const initialState = {
        requirements: this.model.requirements
      }
      console.log(initialState);

      this.bsModalRef = this.modalService.show(RequirementsModalComponent, {initialState});
    } else {
      this.bsModalRef = this.modalService.show(RequirementsModalComponent);
    }

    this.bsModalRef.content.createdRequirements.subscribe((requirements: Requirements) => {
      this.model.requirements = requirements;
    });
  }

  getCategories() {
    this.adminService.getCategories().subscribe((next: Category[]) => {
      this.categories = next;
    }, error => {
      console.log(error);
    });
  }

  getLanguages() {
    this.adminService.getLanguages().subscribe((next: Languague[]) => {
      this.languages = next;
    }, error => {
      console.log(error);
    });
  }

  getSubCategories() {
    this.adminService.getSubCategories().subscribe((next: SubCategory[]) => {
      this.subCategories = next;
      console.log(this.subCategories);
    }, error => {
      console.log(error);
    });
  }



  editProduct() {
    console.log(this.model);
    this.parsePhotosUrlToArray();
    this.adminService.editProduct(this.model, this.productIdFromProductsPanel).subscribe();
  }

  getProductForEdit(id: Number) {
    this.adminService.getProductForEdit(id).subscribe((next: Product) => {
      this.model = next;
      console.log(this.model);
    }, error => {
      console.log(error);
    });
  }

  parsePhotosUrlToArray() {
    let array = [];
    if (this.model.photos != null) {
    if (this.model.photos.includes(',')) {
    array = this.model.photos.split(',');
    this.model.photos = array;
    }
  } else {
    this.model.photos = array;
  }
  }

  cancelButton() {
    this.editMode.emit(false);
  }

}
