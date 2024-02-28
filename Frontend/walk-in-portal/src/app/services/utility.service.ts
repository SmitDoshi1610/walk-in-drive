import { Injectable } from '@angular/core';
import { User, educationQualification, personalInformation, professionalQualification } from '../models';
import { JwtHelperService } from '@auth0/angular-jwt';


@Injectable({
  providedIn: 'root'
})
export class UtilityService {


  constructor(private jwt: JwtHelperService) { }

  getUser(): User {
    let token = this.jwt.decodeToken();
    let user: User = {

      id: token.id,
      firstname: token.firstname,
      lastname: token.lastname,
      phone: token.phone
    };
    return user;

  }
  setUser(token: string) {
    localStorage.setItem('user', token);
  }

  isLoggedIn(): boolean {
    return localStorage.getItem('user') ? true : false;
  }

  logoutUser() {
    localStorage.removeItem('user');
  }

}
