import { RequirementsModalComponent } from './../requirements-modal/requirements-modal.component';
import { Languague } from './../../_models/Languague';
import { SubCategory } from './../../_models/SubCategory';
import { Category } from './../../_models/Category';
import { Requirements } from './../../_models/Requirements';
import { AdminService } from './../../_services/admin.service';
import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-create-product',
  templateUrl: './create-product.component.html',
  styleUrls: ['./create-product.component.css']
})
export class CreateProductComponent implements OnInit {
  @Output() creationMode = new EventEmitter();
  model: any = {};
  categories: Category[];
  bsModalRef: BsModalRef;
  subCategories: SubCategory[];
  languages: Languague[];

  constructor(
    private adminService: AdminService,
    private modalService: BsModalService
  ) {}

  ngOnInit() {
    this.model.categoryId = null;
    this.getCategories();
    this.getSubCategories();
    this.getLanguages();
  }

  createProduct() {
    console.log(this.model.subCategoriesId);
    this.parsePhotosUrlToArray();
    console.log(this.model.requirements);
    this.adminService.createProduct(this.model).subscribe(
      next => {
        console.log('Product Created');
        this.creationMode.emit(false);
      },
      error => {
        console.log(error);
      }
    );
  }


  createRequirementsModal() {
    if (this.model.requirements != null) {
      const initialState = {
        requirements: this.model.requirements
      };
      console.log(initialState);
      this.bsModalRef = this.modalService.show(RequirementsModalComponent, {
        initialState
      });
    } else {
      this.bsModalRef = this.modalService.show(RequirementsModalComponent);
    }

    this.bsModalRef.content.createdRequirements.subscribe(
      (requirements: Requirements) => {
        this.model.requirements = requirements;
      }
    );
  }

  getLanguages() {
    this.adminService.getLanguages().subscribe(
      (next: Languague[]) => {
        this.languages = next;
      },
      error => {
        console.log(error);
      }
    );
  }

  getSubCategories() {
    this.adminService.getSubCategories().subscribe(
      (next: SubCategory[]) => {
        this.subCategories = next;
        console.log(this.subCategories);
      },
      error => {
        console.log(error);
      }
    );
  }

  getCategories() {
    this.adminService.getCategories().subscribe(
      (next: Category[]) => {
        this.categories = next;
        console.log(this.categories);
      },
      error => {
        console.log(error);
      }
    );
  }

  parsePhotosUrlToArray() {
    let array = [];
    if (this.model.photos != null) {
      if (this.model.photos.includes(',')) {
        array = this.model.photos.split(',');
        this.model.photos = array;
      } else {
        array.push(this.model.photos);
        this.model.photos = array;
      }
    } else {
      this.model.photos = array;
    }
  }

  cancelButton() {
    this.creationMode.emit(false);
  }

  nameof<T>(key: keyof T, instance?: T): keyof T {
    return key;
  }
}
