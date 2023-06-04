import { Component, OnInit } from '@angular/core';
import { Payment } from '../../models/payment';
import { PaymentService } from '../services/payment.service';
import { Observable } from 'rxjs';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { TransactionDialogComponent } from '../transaction-dialog/transaction-dialog.component';

@Component({
  selector: 'app-payment-list',
  templateUrl: './payment-list.component.html',
  styleUrls: ['./payment-list.component.css']
})
export class PaymentListComponent implements OnInit {
  payments: Payment[] = [];
  totalRecords: number = 0;
  page: number = 1;
  itemsPerPage: number = 10;
  filterStatus: string = '';
  allPayments: Payment[] = [];  // The original list
  bsModalRef!: BsModalRef;

  constructor(private paymentService: PaymentService, private modalService: BsModalService) { }

  ngOnInit() {
    this.getPayments(this.page, this.itemsPerPage);
  }

  getPayments(page: number, itemsPerPage: number) {
    this.paymentService.getTransactions(page, itemsPerPage).subscribe(
      data => {
        this.allPayments = data.items;
        this.totalRecords = data.totalCount;
        this.filterPayments();
      },
      error => {
        console.log(error);
      }
    );
  }

  onStatusFilterChange() {
    this.filterPayments();
  }

  filterPayments() {
    if (this.filterStatus === '') {
      this.payments = this.allPayments;  // No filter
    } else {
      // Filter by status
      this.payments = this.allPayments.filter(payment => payment.status === this.filterStatus);
    }
  }

  onPageChange(page: number) {
    this.page = page;
    this.getPayments(this.page, this.itemsPerPage);
  }

  openVoidTransactionModal(payment: Payment) {
    const initialState = {
      title: 'Are you sure you want to void the transaction?',
      confirmCallback: () => this.paymentService.voidPayment(payment.id, payment.orderReference).subscribe(
        () => this.getPayments(this.page, this.itemsPerPage),
        error => console.error('Error:', error)
      )
    };
    this.bsModalRef = this.modalService.show(TransactionDialogComponent, { initialState });
  }

  openCaptureTransactionModal(payment: Payment) {
    const initialState = {
      title: 'Are you sure you want to capture the transaction?',
      confirmCallback: () => this.paymentService.capturePayment(payment.id, payment.orderReference).subscribe(
        () => this.getPayments(this.page, this.itemsPerPage),
        error => console.error('Error:', error)
      )
    };
    this.bsModalRef = this.modalService.show(TransactionDialogComponent, { initialState });
  }
}