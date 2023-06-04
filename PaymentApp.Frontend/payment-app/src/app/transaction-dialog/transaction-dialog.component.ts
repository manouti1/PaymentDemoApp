import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-transaction-dialog',
  templateUrl: './transaction-dialog.component.html',
  styleUrls: ['./transaction-dialog.component.css']
})

export class TransactionDialogComponent implements OnInit {
  title: string = '';
  confirmCallback: () => void = () => { };
  constructor(public bsModalRef: BsModalRef) { }

  ngOnInit(): void {
    this.title = this.bsModalRef.content.title;
    this.confirmCallback = this.bsModalRef.content.confirmCallback;
  }

  confirm(): void {
    this.confirmCallback();
    this.bsModalRef.hide();
  }

  decline(): void {
    this.bsModalRef.hide();
  }
}
