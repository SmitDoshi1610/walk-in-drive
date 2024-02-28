import { Component, OnInit } from '@angular/core';
import { NavbarComponent } from '../navbar/navbar.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faArrowUp } from '@fortawesome/free-solid-svg-icons';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EducationalQualificationComponent } from './educational-qualification/educational-qualification.component';
import { ProfessionalQualificationComponent } from './professional-qualification/professional-qualification.component';
import { ReviewAndProceedComponent } from './review-and-proceed/review-and-proceed.component';
import { ResponseMessage, personalInformation, registrationData } from '../models';
import { DataService } from '../services/data.service';
import { ApiService } from '../services/api.service';
import { UtilityService } from '../services/utility.service';
import { NgToastModule, NgToastService } from 'ng-angular-popup';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-registration',
  standalone: true,
  imports: [
    NavbarComponent,
    FontAwesomeModule,
    CommonModule,
    FormsModule,
    EducationalQualificationComponent,
    ProfessionalQualificationComponent,
    ReviewAndProceedComponent,
    ReactiveFormsModule,
    NgToastModule,
    RouterModule
  ],
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css',
})
export class RegistrationComponent implements OnInit {

  faArrowUp = faArrowUp;
  stepCounter: number = 1;
  applicationType: string = '';
  personalInformation!: personalInformation;
  fileName: string = ''
  url: string | ArrayBuffer | null | undefined;
  resumeFileName: string = '';
  registerData?: registrationData;
  len: number = 5;
  userData!: any;
  tempPreferedJobRole: string[];
  items!: { value: string, checked: boolean }[];
  checkedItems: string[] = [];
  User!: any;

  constructor(private data: DataService, private apiService: ApiService, private auth: AuthService ,private utility: UtilityService, private toast: NgToastService, private router: Router) {
    this.personalInformation = data.personalInformation;
    this.tempPreferedJobRole = [];

  }
  ngOnInit(): void {
    this.apiService.getRegistrationData().subscribe((result: registrationData) => {
      this.registerData = result
    })


  }
  nextStep(): void {
    this.stepCounter += 1;
    this.router.navigate([`registration/${this.stepCounter}`]);
    console.log("Personal Information", this.data.personalInformation);
    console.log("Education Qualification", this.data.educationQualification);
    console.log("Professional Qualification", this.data.professionalQualification);
  }

  previousStep(): void {
    this.stepCounter -= 1;
    this.router.navigate([`registration/${this.stepCounter}`]);
  }

  showFile(event: any) {
    console.log(event);
    this.fileName = event.target.files[0].name;
    const files = event.target.files;

    const reader = new FileReader();
    reader.readAsDataURL(files[0]);
    reader.onload = (_event) => {
      this.url = reader.result;
    }
  }

  registeruser() {
    this.auth.registerUser(this.data.personalInformation).subscribe((result: ResponseMessage) => {

      if (result.moveNext == true) {
        this.apiService.insertEducationData(this.data.educationQualification).subscribe((result: ResponseMessage) => {

          if (result.moveNext == true) {
            this.apiService.insertProfessionalData(this.data.professionalQualification).subscribe((result: ResponseMessage) => {

              if (result.moveNext == true) {
                this.toast.success({ detail: "SUCCESS", summary: result.message, duration: 3000, position: 'topCenter' });
                this.router.navigate(['']);
              }
            }, (error) => {
              this.toast.error({ detail: "ERROR", summary: result.message, duration: 3000, position: 'topCenter' });
              this.router.navigate(['registration']);
              
            });
          }
        });
      }
    });
  }

  convertBooleanToIndex() {

    this.personalInformation.preferredJobRoles.forEach((key: any, value: number) => {
      if (key == true) {

        this.personalInformation.preferredJobRoles[value] = (value + 1).toString();
        this.tempPreferedJobRole[value] = (value + 1).toString();
      }
      else {
        if (this.tempPreferedJobRole[value] > "0" && key == true) {
          this.tempPreferedJobRole[value] = "0";
          this.personalInformation.preferredJobRoles[value] = "0";
        }

      }
      this.personalInformation.preferredJobRoles = this.tempPreferedJobRole;
    })
    console.log(this.personalInformation.preferredJobRoles);

  }
}

