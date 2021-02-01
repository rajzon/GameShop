import { OrderInfo } from "../_models/OrderInfo";

export function IsOrderInfoValid(obj: OrderInfo): boolean {
    if (!obj) {
        return false;
    }

    const requiredFields = ['name', 'surName', 'street', 'postCode', 'deliveryType'];
    const notRequiredFields = ['city', 'phone', 'email', 'country'];
    const checker = (arr, target) => target.every(v => arr.includes(v));
    
    if (!checker(Object.keys(obj), requiredFields) ||
        Object.entries(obj).filter(entry => !notRequiredFields.some(x => x === entry[0])).some(propVal => propVal[1].length === 0)){
        // console.log(Object.entries(obj).filter(entry => !notRequiredFields.some(x => x === entry[0])).some(propVal => propVal[1].length === 0));
        // console.log(Object.entries(obj).filter(entry => entry[0] !== 'city'));
        
        return false;
    }
    return true;
}
