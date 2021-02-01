import { BasketFromServer } from './BasketFromServer';
import { OrderInfo } from 'src/app/_models/OrderInfo';
export interface PaymentInfo {
    basket: BasketFromServer;
    orderInfo: OrderInfo;
}
