import { ProductForBasketFromServer } from './ProductForBasketFromServer';
export interface BasketFromServer {
    basketPrice: number;
    basketProducts: ProductForBasketFromServer[];
}
