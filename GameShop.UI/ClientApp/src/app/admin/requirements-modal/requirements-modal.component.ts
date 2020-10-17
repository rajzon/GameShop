import { Requirements } from './../../_models/Requirements';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-requirements-modal',
  templateUrl: './requirements-modal.component.html',
  styleUrls: ['./requirements-modal.component.css']
})
export class RequirementsModalComponent implements OnInit {
  @Output() createdRequirements = new EventEmitter();
  requirements: Requirements = <Requirements>{ };

  osMaxLength: number = environment.osMaxLength;
  processorMaxLength: number = environment.processorMaxLength;
  graphicsCardMaxLength: number = environment.graphicsCardMaxLength;

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
