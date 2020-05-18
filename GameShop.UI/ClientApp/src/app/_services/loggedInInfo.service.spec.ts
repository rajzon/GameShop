/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { LoggedInInfoService } from './loggedInInfo.service';

describe('Service: LoggedInInfo', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LoggedInInfoService]
    });
  });

  it('should ...', inject([LoggedInInfoService], (service: LoggedInInfoService) => {
    expect(service).toBeTruthy();
  }));
});
