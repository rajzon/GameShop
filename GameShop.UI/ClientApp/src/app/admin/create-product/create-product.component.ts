import { MessagePopupService } from './../../_services/message-popup.service';
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
import { environment } from 'src/environments/environment';

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

  productNameMaxLength: number = environment.productNameMaxLength;
  productDescriptionMaxLength: number = environment.productDescriptionMaxLength;
  productPriceMaxValue: number = environment.productPriceMaxValue;
  productPriceMinValue: number = environment.productPriceMinValue;


  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  response: string;
  baseUrl = environment.baseUrl;

  creationState = false;

  constructor(
    private adminService: AdminService,
    private modalService: BsModalService,
    private messagePopup: MessagePopupService
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

  initializeUploader(): void {
    this.uploader = new FileUploader({
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: environment.maxFileSize
    });

    this.uploader.onErrorItem = (item, response, status, headers ) => {
      this.messagePopup.displayError('Error occured during uploading photo');
    };
  }

  createProduct(): void {
    this.creationState = true;
    this.adminService.createProduct(this.model).subscribe(
      (next: ProductFromServer) => {
        this.messagePopup.displaySuccess('Product Created');

        const productIdFromServer = next.id;
        this.uploadPhotos(productIdFromServer);
      },
      error => {
        this.messagePopup.displayError(error);
        this.creationState = false;
      }
    );
  }

  private uploadPhotos(productIdFromServer: number): void {
      if (this.uploader.queue.length > 0) {
        const photosUploadedCount = this.uploader.queue.length;

        this.uploader.onBeforeUploadItem  = (file) => {
          file.url = this.baseUrl + 'admin/product/' + productIdFromServer + '/photos'};

        this.uploader.uploadAll();
        this.uploader.onCompleteAll = () => {
          this.messagePopup.displaySuccess(`${photosUploadedCount} Photos Uploaded`);
          this.creationMode.emit(false);
        };
      } else {
      this.creationMode.emit(false);
    }
  }


  createRequirementsModal(): void {
    if (this.model.requirements != null) {
      const initialState = {
        requirements: this.model.requirements
      };
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

  getLanguages(): void {
    this.adminService.getLanguages().subscribe(
      (next: Languague[]) => {
        this.languages = next;
      },
      error => {
        this.messagePopup.displayError(error);
      }
    );
  }

  getSubCategories(): void {
    this.adminService.getSubCategories().subscribe(
      (next: SubCategory[]) => {
        this.subCategories = next;
      },
      error => {
        this.messagePopup.displayError(error);
      }
    );
  }

  getCategories(): void {
    this.adminService.getCategories().subscribe(
      (next: Category[]) => {
        this.categories = next;
      },
      error => {
        this.messagePopup.displayError(error);
      }
    );
  }

  cancelButton(): void {
    this.creationMode.emit(false);
  }
}
