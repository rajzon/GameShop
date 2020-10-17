import { MessagePopupService } from './../../_services/message-popup.service';
import { Product } from './../../_models/Product';
import { SubCategory } from './../../_models/SubCategory';
import { Languague } from './../../_models/Languague';
import { Category } from './../../_models/Category';
import { AdminService } from './../../_services/admin.service';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { RequirementsModalComponent } from '../requirements-modal/requirements-modal.component';
import { Requirements } from 'src/app/_models/Requirements';
import { FileUploader } from 'ng2-file-upload';
import { Photo } from 'src/app/_models/Photo';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.css']
})
export class EditProductComponent implements OnInit {
  @Input() productIdFromProductsPanel: any;
  @Output() editMode = new EventEmitter();
  bsModalRef: BsModalRef;
  model: Product = <Product>{ };
  categories: Category[];
  languages: Languague[];
  subCategories: SubCategory[];
  currentMainPhoto: Photo;

  productNameMaxLength: number = environment.productNameMaxLength;
  productDescriptionMaxLength: number = environment.productDescriptionMaxLength;
  productPriceMaxValue: number = environment.productPriceMaxValue;
  productPriceMinValue: number = environment.productPriceMinValue;


  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  response: string;
  baseUrl = environment.baseUrl;

  editionState = false;

  constructor(private adminService: AdminService, private modalService: BsModalService, private messagePopup: MessagePopupService) { }

  ngOnInit() {
    console.log(this.productIdFromProductsPanel);
    this.getProductForEdit(this.productIdFromProductsPanel);
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
      url: this.baseUrl + 'admin/product/' + this.productIdFromProductsPanel + '/photos',
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: environment.maxFileSize
    });

    this.uploader.onSuccessItem = (item, response, status, headers ) => {
      if (response) {
        const res: Photo = JSON.parse(response);
        const photo = {
          id: res.id,
          url: res.url,
          dateAdded: res.dateAdded,
          isMain: res.isMain
        };
        this.model.photos.push(photo);
      }
    };

    this.uploader.onErrorItem = (item, response, status, headers ) => {
      this.messagePopup.displayError('Photo was not added');
    };
  }

  setMainPhoto(photo: Photo): void {
    this.adminService.setMainPhoto(this.productIdFromProductsPanel, photo.id).subscribe(() => {
      this.messagePopup.displaySuccess(`Successfully set photo id:${photo.id} as main`);
      this.currentMainPhoto = this.model.photos.filter(p => p.isMain === true)[0];
      this.currentMainPhoto.isMain = false;
      photo.isMain = true;
    }, error => {
      this.messagePopup.displayError(error);
    });
  }

  deletePhoto(id: number): void {
    this.adminService.deletePhoto(this.productIdFromProductsPanel, id).subscribe(() => {
      this.model.photos.splice(this.model.photos.findIndex(p => p.id === id), 1);
      this.messagePopup.displayInfo(`Photo id:${id} has been deleted`);
    }, error => {
      this.messagePopup.displayError(error);
    });
  }

  createRequirementsModal(): void {
    if (this.model.requirements != null ) {
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

  getCategories(): void {
    this.adminService.getCategories().subscribe((next: Category[]) => {
      this.categories = next;
    }, error => {
      this.messagePopup.displayError(error);
    });
  }

  getLanguages(): void {
    this.adminService.getLanguages().subscribe((next: Languague[]) => {
      this.languages = next;
    }, error => {
      this.messagePopup.displayError(error);
    });
  }

  getSubCategories(): void {
    this.adminService.getSubCategories().subscribe((next: SubCategory[]) => {
      this.subCategories = next;
      console.log(this.subCategories);
    }, error => {
      this.messagePopup.displayError(error);
    });
  }

  editProduct(): void {
    console.log(this.model);
    this.editionState = true;
    this.adminService.editProduct(this.model, this.productIdFromProductsPanel).subscribe(() => {
      this.messagePopup.displaySuccess('Product edited successfully');
      this.uploadPhotos();
    }, error => {
      this.editionState = false;
      this.messagePopup.displayError(error);
    });
  }

  private uploadPhotos(): void {
    if (this.uploader.queue.length > 0) {
      const photosUploadedCount = this.uploader.queue.length;

      this.uploader.uploadAll();
      this.uploader.onCompleteAll = () => {
        this.messagePopup.displaySuccess(`${photosUploadedCount} Photos Uploaded`);
        this.editMode.emit(false);
    };
    } else {
      this.editMode.emit(false);
    }
  }

  getProductForEdit(id: Number): void {
    this.adminService.getProductForEdit(id).subscribe((next: Product) => {
      this.model = next;
      console.log(this.model);
    }, error => {
      this.messagePopup.displayError(error);
    });
  }

  cancelButton(): void {
    if (this.uploader.queue.length > 0) {
      this.uploader.clearQueue();
    }
    this.editMode.emit(false);
  }

}
