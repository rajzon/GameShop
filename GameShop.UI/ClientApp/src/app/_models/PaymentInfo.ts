import { BasketFromServer } from './BasketFromServer';
import { CustomerInfo } from 'src/app/_models/CustomerInfo';
export interface PaymentInfo {
    basket: BasketFromServer;
    customerInfo: CustomerInfo;
}
