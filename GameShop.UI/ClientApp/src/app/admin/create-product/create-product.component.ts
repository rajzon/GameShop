import { RequirementsModalComponent } from './../requirements-modal/requirements-modal.component';
import { Languague } from './../../_models/Languague';
import { SubCategory } from './../../_models/SubCategory';
import { SubCategoriesModalComponent } from './../sub-categories-modal/sub-categories-modal.component';
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

  constructor(private adminService: AdminService, private modalService: BsModalService) { }

  ngOnInit() {
    this.model.categoryId = null;
    this.getCategories();
    this.getSubCategories();
    this.getLanguages();
  }


  createProduct() {
    console.log(this.model.subCategoriesId);
    //this.parseStringToRequirements();
    this.parsePhotosUrlToArray();
    //this.parseLanguageIdToArray();
    //this.parseSubCategoriesIdToArray();
    console.log(this.model.requirements);
    this.adminService.createProduct(this.model).subscribe(next => {
      console.log('Product Created');
      this.creationMode.emit(false);
    }, error => {
      console.log(error);
    });
  }

  selectSubCategoriesModal() {
    console.log(this.subCategories);
    const initialState = {
      subCategories: this.subCategories
    };   
    this.bsModalRef = this.modalService.show(SubCategoriesModalComponent, {initialState});
    this.bsModalRef.content.selectedSubCategories.subscribe((values) => {
      const selectedSubCategories = {
        id: [...values.filter(el => el.checked === true).map(el => el.id)],
        name: [...values.filter(el => el.checked === true).map(el => el.name)],
        // description: [...values.filter(el => el.checked === true).map(el => el.description)]
      };
      this.model.subCategoriesId = [...values.filter(el => el.checked === true).map(el => el.id)];
      console.log(selectedSubCategories);
      console.log(this.model.subCategoriesId);
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

  getCategories() {
    this.adminService.getCategories().subscribe((next: Category[]) => {
      this.categories = next;
      console.log(this.categories);
    }, error => {
      console.log(error);
    });
  }


  parseLanguageIdToArray() {
    if  (this.model.languagesId != null) {
    let array = [Number];
    if (this.model.languagesId.indexOf(',')) {
    array = (this.model.languagesId.split(',')).map(Number);
    this.model.languagesId = array;
    } else {
      this.model.languagesId.map(Number);
    }
  }

  }

  parsePhotosUrlToArray() {
    let array = [];
    if(this.model.photos != null) {
    if (this.model.photos.indexOf(',')) {
    array = this.model.photos.split(',');
    this.model.photos = array;
    }
  } else {
    this.model.photos = array;
  }
  }


  parseSubCategoriesIdToArray() {
    if (this.model.subCategoriesId != null) { 
    let array = [Number];
    if (this.model.subCategoriesId.indexOf(',')){
      array = (this.model.subCategoriesId.split(',')).map(Number);
      this.model.subCategoriesId = array;
      } else {
        this.model.subCategoriesId.map(Number);
      }
    }
  }

  parseStringToRequirements() {
    const req = <Requirements>{ };
    let array = [];
    if (this.model.requirements != null && this.model.requirements.indexOf(',')) {
      array = (this.model.requirements.split(','));
      array.forEach(element => {
        if (element.indexOf(':')) {
          const prop = element.substring(0, element.indexOf(':'));

          if (this.nameof<Requirements>(prop.toLowerCase()))
          {
            if (prop.toLowerCase() === 'isnetworkconnectionrequire') {
              const stringToBool =  (element.substring(element.indexOf(':') + 1) === 'true');
              req[prop.toLowerCase()] = stringToBool;
            } else {
              req[prop.toLowerCase()] = element.substring(element.indexOf(':') + 1);
            }

          }
        }
      });
      console.log(req);
      this.model.requirements = req;
      console.log(this.model.requirements);
    }

  }

  cancelButton() {   
    this.creationMode.emit(false);
  }

  nameof<T>(key: keyof T, instance?: T): keyof T {
    return key;
  }

}
