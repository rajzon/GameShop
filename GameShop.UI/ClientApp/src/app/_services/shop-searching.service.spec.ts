/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ShopSearchingService } from './shop-searching.service';

describe('Service: ShopSearching', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ShopSearchingService]
    });
  });

  it('should ...', inject([ShopSearchingService], (service: ShopSearchingService) => {
    expect(service).toBeTruthy();
  }));
});
