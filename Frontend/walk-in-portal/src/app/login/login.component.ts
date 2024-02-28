import { Component, OnInit, inject } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Login, ResponseMessage } from '../models';
import { DataService } from '../services/data.service';
import { AuthService } from '../services/auth.service';
import { FormsModule } from '@angular/forms';
import { NgToastModule, NgToastService } from 'ng-angular-popup';
import { Router } from '@angular/router';
import { UtilityService } from '../services/utility.service';

@Component({
  selector: 'login',
  standalone: true,
  imports: [FormsModule, NgToastModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {

  login: Login;
  passwordInputType: string;
  constructor(private utility: UtilityService ,private auth: AuthService, private data: DataService, private toast: NgToastService, private router: Router) {
    this.login = this.data.login;
    this.passwordInputType = "password";
  }

  ngOnInit(): void { }

  loginUser(event: any) {
    event.preventDefault();
    this.auth.loginUser(this.login).subscribe((result: ResponseMessage) => {

      if (result.moveNext) {
        this.login.email = "";
        this.login.password = "";
        this.utility.setUser(result.message.toString());
        console.log(this.utility.getUser());
        this.toast.success({ detail: "SUCCESS", summary: "Login Successful", duration: 3000, position: 'topCenter' });
        this.router.navigateByUrl("/list-job");

      }
      else {
        this.login.email = "";
        this.login.password = "";
        this.toast.error({ detail: "ERROR", summary: "Invalid Username & Password", duration: 3000, position: 'topCenter' });
      }

    })
  }
  togglePassword() {
    this.passwordInputType == "password" ? this.passwordInputType = "text" : this.passwordInputType = "password";
  }
}
