import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss']
})
export class TestErrorComponent implements OnInit {

  base = environment.url;
  validationErrors: string[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit(): void { }

  get404Error() {
    this.http.get(this.base + 'product/44').subscribe({
      next: response => console.log(response),
      error: error => console.error(error)
    })
  }

  get500Error() {
    this.http.get(this.base + 'buggy/servererror').subscribe({
      next: response => console.log(response),
      error: error => console.error(error)
    })
  }

  get400Error() {
    this.http.get(this.base + 'buggy/badrequest').subscribe({
      next: response => console.log(response),
      error: error => console.error(error)
    })
  }

  getValidationError() {
    this.http.get(this.base + 'product/fourtyTwo').subscribe({
      next: response => console.log(response),
      error: error => {
        this.validationErrors = error.errors
      }
    })
  }


}
