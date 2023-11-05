import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { InvestmentRequest } from 'src/app/shared/models/investmentRequest';
import { InvestmentResponse } from 'src/app/shared/models/investmentResponse';

@Injectable({
  providedIn: 'root'
})
export class InvestmentService {
  baseUrl: string = 'http://localhost:5225/';

  constructor(private http: HttpClient) { }

  calculateInvestment(investmentRequest: InvestmentRequest): Observable<InvestmentResponse> {
    return this.http.post<InvestmentResponse>(this.baseUrl + 'Investment/CalculateCDB', investmentRequest);
  }
}
