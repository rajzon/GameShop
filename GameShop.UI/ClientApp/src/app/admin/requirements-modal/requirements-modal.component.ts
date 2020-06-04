import { Requirements } from './../../_models/Requirements';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-requirements-modal',
  templateUrl: './requirements-modal.component.html',
  styleUrls: ['./requirements-modal.component.css']
})
export class RequirementsModalComponent implements OnInit {
  @Output() createdRequirements = new EventEmitter();
  requirements: Requirements = <Requirements>{ };

  constructor(public bsModalRef: BsModalRef) { }

  ngOnInit() {
    console.log(this.requirements);
    if (Object.keys(this.requirements).length === 0) {
    this.requirements.isNetworkConnectionRequire = false;
    }
  }

  createRequirements() {
    console.log(this.requirements);
    this.createdRequirements.emit(this.requirements);
    this.bsModalRef.hide();
  }

}
