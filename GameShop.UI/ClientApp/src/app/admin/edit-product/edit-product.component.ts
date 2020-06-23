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


  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  response: string;
  baseUrl = 'api/';

  constructor(private adminService: AdminService, private modalService: BsModalService) { }

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

  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'admin/product/' + this.productIdFromProductsPanel + '/photos',
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024 //10MB
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
  }

  setMainPhoto(photo: Photo) {
    this.adminService.setMainPhoto(this.productIdFromProductsPanel, photo.id).subscribe(() => {
      console.log('Successfully set to main');
      this.currentMainPhoto = this.model.photos.filter(p => p.isMain === true)[0];
      this.currentMainPhoto.isMain = false;
      photo.isMain = true;
    }, error => {
      console.log(error);
    });
  }

  deletePhoto(id: number) {
    this.adminService.deletePhoto(this.productIdFromProductsPanel, id).subscribe(() => {
      this.model.photos.splice(this.model.photos.findIndex(p => p.id === id), 1);
      console.log('Photo has been deleted')
    }, error => {
      console.log(error);
    });
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
    //this.parsePhotosUrlToArray();
    this.adminService.editProduct(this.model, this.productIdFromProductsPanel).subscribe(() => {
      console.log('Product edited successfully');
      if (this.uploader.queue.length > 0) {
        this.uploader.uploadAll();
        this.uploader.onCompleteAll = () => {
        this.editMode.emit(false);
      };
      } else {
        this.editMode.emit(false);
      }
    }, error => {
      console.log(error);
    });
  }

  getProductForEdit(id: Number) {
    this.adminService.getProductForEdit(id).subscribe((next: Product) => {
      this.model = next;
      console.log(this.model);
    }, error => {
      console.log(error);
    });
  }

  cancelButton() {
    this.editMode.emit(false);
  }

}
