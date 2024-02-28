import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Login, ResponseMessage, personalInformation } from '../models';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  registerUser(user: personalInformation) {

    let url = "http://localhost:5161/api/User/RegisterUser";
    return this.http.post<ResponseMessage>(url, user);
  }

  loginUser(user: Login){
    let url = "http://localhost:5161/api/User/Login";
    return this.http.post<ResponseMessage>(url, user);
  }
  
}
