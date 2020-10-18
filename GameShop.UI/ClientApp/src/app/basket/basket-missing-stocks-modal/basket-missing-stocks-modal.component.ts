import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { NotEnoughStockInfoFromServer } from 'src/app/_models/NotEnoughStockInfoFromServer';

@Component({
  selector: 'app-basket-missing-stocks-modal',
  templateUrl: './basket-missing-stocks-modal.component.html',
  styleUrls: ['./basket-missing-stocks-modal.component.css']
})
export class BasketMissingStocksModalComponent implements OnInit {
  error: NotEnoughStockInfoFromServer[];

  constructor(public bsModalRef: BsModalRef) { }

  ngOnInit() {
  }

}
