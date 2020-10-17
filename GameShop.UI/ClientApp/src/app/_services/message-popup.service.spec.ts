/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { MessagePopupService } from './message-popup.service';

describe('Service: MessagePopup', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MessagePopupService]
    });
  });

  it('should ...', inject([MessagePopupService], (service: MessagePopupService) => {
    expect(service).toBeTruthy();
  }));
});
