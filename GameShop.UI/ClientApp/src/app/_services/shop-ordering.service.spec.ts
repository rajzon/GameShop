/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ShopOrderingService } from './shop-ordering.service';

describe('Service: ShopOrdering', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ShopOrderingService]
    });
  });

  it('should ...', inject([ShopOrderingService], (service: ShopOrderingService) => {
    expect(service).toBeTruthy();
  }));
});
