import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-sub-categories-modal',
  templateUrl: './sub-categories-modal.component.html',
  styleUrls: ['./sub-categories-modal.component.css']
})
export class SubCategoriesModalComponent implements OnInit {
  @Output() selectedSubCategories = new EventEmitter();
  subCategories: any[];

 
  constructor(public bsModalRef: BsModalRef) {}

  ngOnInit() {
    this.subCategories.forEach(element => {
      element.checked = false;
    });
    console.log(this.subCategories);
  }

  selectSubCategories() {
    this.selectedSubCategories.emit(this.subCategories);
    this.bsModalRef.hide();
  }

}
