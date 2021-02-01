import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class MessagePopupService {

constructor(private toastr: ToastrService) { }

public displayError(error: any): void {
  if (error instanceof Array) {
      let errors = '';
      error.forEach(x => errors += x.description + '; ' );
      this.toastr.error(errors);
  } else
  if (error instanceof Object) {
      this.toastr.error(error.title);
  } else {
      this.toastr.error(error);
  }
}

public displayInfo(message: any): void {
  this.toastr.info(message);
}

public displaySuccess(message: any): void {
  this.toastr.success(message);
}

}
