import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { PaymentListComponent } from './payment-list/payment-list.component';
import { PaymentService } from './services/payment.service';
import { HttpClientModule } from '@angular/common/http';
import { PaginationModule, PaginationConfig } from 'ngx-bootstrap/pagination';
import { ModalModule } from 'ngx-bootstrap/modal';
import { FormsModule } from '@angular/forms';
import { TransactionDialogComponent } from './transaction-dialog/transaction-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    PaymentListComponent,
    TransactionDialogComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ModalModule.forRoot(),
    PaginationModule
  ],
  providers: [PaymentService, PaginationConfig],
  bootstrap: [AppComponent]
})
export class AppModule { }
