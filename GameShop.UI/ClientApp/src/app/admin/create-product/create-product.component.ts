import { Requirements } from './../../_models/Requirements';
import { AdminService } from './../../_services/admin.service';
import { Component, OnInit, EventEmitter, Output } from '@angular/core';


@Component({
  selector: 'app-create-product',
  templateUrl: './create-product.component.html',
  styleUrls: ['./create-product.component.css']
})
export class CreateProductComponent implements OnInit {
  @Output() creationMode = new EventEmitter();
  model: any = {};

  constructor(private adminService: AdminService) { }

  ngOnInit() {
  }

  createProduct() {
    this.parseStringToRequirements();
    this.parsePhotosUrlToArray();
    this.parseLanguageIdToArray();
    this.parseSubCategoriesIdToArray();
    console.log(this.model.requirements);
    this.adminService.createProduct(this.model).subscribe(next => {
      console.log('Product Created');
      this.creationMode.emit(false);
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
