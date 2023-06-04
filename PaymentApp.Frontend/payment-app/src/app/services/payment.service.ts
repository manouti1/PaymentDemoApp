import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PaymentResponse } from '../../models/payment-response';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  private apiUrl = 'https://localhost:7149/api'; // Replace with your actual API URL

  constructor(private http: HttpClient) { }

  getTransactions(pageNumber: number, pageSize: number, filters?: any): Observable<PaymentResponse> {
    let params = new HttpParams()
      .set('PageNumber', pageNumber.toString())
      .set('PageSize', pageSize.toString());

    for (const filter in filters) {
      if (filter) {
        params = params.set(filter, filters[filter]);
      }
    }
    return this.http.get<any>(`${this.apiUrl}/Transactions`, { params });
  }

  voidPayment(id: string, orderReference: string): Observable<any> {
    const url = `${this.apiUrl}/authorize/${id}/voids`;
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http.post(url, JSON.stringify(orderReference), { headers });
  }

  capturePayment(id: string, orderReference: string): Observable<any> {
    const url = `${this.apiUrl}/authorize/${id}/capture`;
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http.post(url, JSON.stringify(orderReference), { headers });
  }
}