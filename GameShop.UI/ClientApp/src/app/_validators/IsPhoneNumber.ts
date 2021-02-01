import { ValidatorFn, AbstractControl } from '@angular/forms';
import { environment } from 'src/environments/environment';

export function IsPhoneNumber(): ValidatorFn {
    return (control: AbstractControl) => {
            return (control.value !== null) ?
                 ( control.value.match(environment.phonePattern) ? null : {IsInvalidPhoneNum: true} ) :
                 {IsInvalidPhoneNum: true};
    }
}
