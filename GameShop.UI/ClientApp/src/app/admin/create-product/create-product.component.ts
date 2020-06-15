import { Product } from './../../_models/Product';
import { RequirementsModalComponent } from './../requirements-modal/requirements-modal.component';
import { Languague } from './../../_models/Languague';
import { SubCategory } from './../../_models/SubCategory';
import { Category } from './../../_models/Category';
import { Requirements } from './../../_models/Requirements';
import { AdminService } from './../../_services/admin.service';
import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { FileUploader } from 'ng2-file-upload';
import { ProductFromServer } from 'src/app/_models/ProductFromServer';

@Component({
  selector: 'app-create-product',
  templateUrl: './create-product.component.html',
  styleUrls: ['./create-product.component.css']
})
export class CreateProductComponent implements OnInit {
  @Output() creationMode = new EventEmitter();
  model: Product = <Product>{ };
  categories: Category[];
  bsModalRef: BsModalRef;
  subCategories: SubCategory[];
  languages: Languague[];
  productIdFromServer: number;


  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  response: string;
  baseUrl = 'api/';

  constructor(
    private adminService: AdminService,
    private modalService: BsModalService
  ) {}

  ngOnInit() {
    this.model.categoryId = null;
    this.getCategories();
    this.getSubCategories();
    this.getLanguages();

    this.initializeUploader();
  }

  public fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024 //10MB
    });
  }

  createProduct() {
    console.log(this.model.subCategoriesId);
    console.log(this.model.requirements);
    this.adminService.createProduct(this.model).subscribe(
      (next: ProductFromServer) => {
        console.log('Product Created');
        this.productIdFromServer = next.id;
        console.log(this.productIdFromServer);
        if (this.uploader.queue.length > 0) {
          this.uploader.onBeforeUploadItem  = (file) => {
            file.url = this.baseUrl + 'admin/product/' + this.productIdFromServer + '/photos'};
          console.log(this.uploader.options.url);
          this.uploader.uploadAll();
          this.uploader.onCompleteAll = () => {
            this.creationMode.emit(false);
          };
      } else {
        this.creationMode.emit(false);
      }
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

  cancelButton() {
    this.creationMode.emit(false);
  }

  nameof<T>(key: keyof T, instance?: T): keyof T {
    return key;
  }
}
