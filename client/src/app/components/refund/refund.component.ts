import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Notyf } from 'notyf';
import { Response } from 'src/app/interfaces/response';

@Component({
  selector: 'app-refund',
  templateUrl: './refund.component.html',
  styleUrls: ['./refund.component.scss'],
})
export class RefundComponent {
  private baseApiUrl: string = 'http://localhost:5044/api/';
  id!: string;
  code!: string;
  notyf = new Notyf();
  constructor(
    private http: HttpClient,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.id = this.route.snapshot.paramMap.get('id') || '0';
    this.getRefund();
  }

  public getRefund() {
    this.http
      .get(this.baseApiUrl + 'Refund/get-code/?id=' + this.id, {
        headers: new HttpHeaders().append(
          'Authorization',
          `Bearer ${localStorage.getItem('accessToken')}`
        ),
      })
      .subscribe((res: any) => {
        let response: Response = res;
        if (response.statusCode === 200) {
          this.code = response.responseModel.refundCode;
        }
      });
  }
}
