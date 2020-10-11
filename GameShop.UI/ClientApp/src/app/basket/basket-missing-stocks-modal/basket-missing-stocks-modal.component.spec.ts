/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { BasketMissingStocksModalComponent } from './basket-missing-stocks-modal.component';

describe('BasketMissingStocksModalComponent', () => {
  let component: BasketMissingStocksModalComponent;
  let fixture: ComponentFixture<BasketMissingStocksModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BasketMissingStocksModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BasketMissingStocksModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
