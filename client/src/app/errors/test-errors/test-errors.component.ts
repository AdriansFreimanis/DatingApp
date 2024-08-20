import { HttpClient } from '@angular/common/http';
import { Component, inject, TrackByFunction } from '@angular/core';

@Component({
  selector: 'app-test-errors',
  standalone: true,
  imports: [],
  templateUrl: './test-errors.component.html',
  styleUrl: './test-errors.component.css'
})
export class TestErrorsComponent {
  baseUrl = 'https://localhost:5001/api/';
  private http = inject(HttpClient);
  validationErrors: string[] = [];
  
  //get method
  get400Error(){
    this.http.get(this.baseUrl + 'buggy/bad-request').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }
  //get method
  get401Error(){
    this.http.get(this.baseUrl + 'buggy/auth').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }
  //get method
  get404Error(){
    this.http.get(this.baseUrl + 'buggy/not-found').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }
  //get method
  get500Error(){
    this.http.get(this.baseUrl + 'buggy/server-error').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }
  //POST method
  get400ValidationError(){
    this.http.post(this.baseUrl + 'account/register', {}).subscribe({
      next: response => console.log(response),
      error: error => {
        console.log(error);
        this.validationErrors = error;
      }
      })
  }
}
