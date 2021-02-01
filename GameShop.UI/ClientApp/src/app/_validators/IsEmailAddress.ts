import { ValidatorFn, AbstractControl } from '@angular/forms';
import { environment } from 'src/environments/environment';
export function IsEmailAddress(): ValidatorFn  {
    return (control: AbstractControl) => {
        return (control.value !== null) ?
             ( control.value.match(environment.emailPattern) ? null : {IsInvalidEmail: true} ) :
             {IsInvalidEmail: true};
}
}
