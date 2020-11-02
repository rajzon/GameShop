import { NotEnoughStockInfoFromServer } from 'src/app/_models/NotEnoughStockInfoFromServer';

export function IsArrayOfNotEnoughStockInfoFromServer(error: any): boolean {
    if (error instanceof Array) {
      if (error.every(x => (x as NotEnoughStockInfoFromServer).stockId !== undefined
          && (x as NotEnoughStockInfoFromServer).productName !== undefined
          && (x as NotEnoughStockInfoFromServer).availableStockQty !== undefined)) {
        return true;
      }
        return false;
    }


    return false;

  }