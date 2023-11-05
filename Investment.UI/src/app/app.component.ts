import { Component } from '@angular/core';
import { InvestmentService } from './core/services/investment.service';
import { InvestmentRequest } from './shared/models/investmentRequest';
import { InvestmentResponse } from './shared/models/investmentResponse';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public investmentRequest: InvestmentRequest = {
    initialValue: 0,
    months: 0
  };

  calculated: boolean = false;

  public investmentResponse: InvestmentResponse | null = null;

  constructor(private investmentService: InvestmentService) { }

  isInvestmentValid(): boolean {
    return this.investmentRequest.initialValue > 0 && this.investmentRequest.months > 1;
  }

  calculateInvestment(): void {
    this.investmentService.calculateInvestment(this.investmentRequest).subscribe(investmentResponse => {
      this.investmentResponse = investmentResponse;
    });
    this.calculated = true;
  }
}
