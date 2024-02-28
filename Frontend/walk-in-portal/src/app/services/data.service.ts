import { Injectable } from '@angular/core';
import { Login, User, educationQualification, personalInformation, professionalQualification } from '../models';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  login!: Login;
  user: User;
  personalInformation!: personalInformation;
  educationQualification!: educationQualification;
  professionalQualification!: professionalQualification

  constructor() {
    this.personalInformation = {
      firstName: '',
      lastName: '',
      email: '',
      password: '',
      emailNotification: false,
      phone: '',
      portflioUrl: '',
      preferredJobRoles: new Array<string>(),
      referredBy: '',
      resume: '',
      profilePicture: '',
    };
    this.educationQualification = {
      college: '',
      collegeLocation: '',
      otherCollege: '',
      percentage: 0,
      qualification: '',
      stream: '',
      passingYear: 0,
    };
    this.professionalQualification = {

      applicationType: '',
      fresherOtherFamiliar: '',
      fresherRoleAppliedFor: '',
      fresherTechnologiesFamiliar: new Array<string>(),
      haveAppliedTestBefore: "",
      currentCTC: '',
      expectedCTC: '',
      isInNoticePeriod: "", 
      noticePeriodDuration: 0,
      othersExpertise: '',
      experienceOtherFamiliar: '',
      noticePeriodEnd: new Date(),
      experienceRoleAppliedFor: '',
      technologiesExpert: new Array<string>(),
      experienceTechnologiesFamiliar: new Array<string>(),
      yearsOfExperience: 0
    };
    this.login = {

      email: '',
      password: ''
    }
    this.user = {
      id: '',
      firstname: '',
      lastname: '',
      phone: ''
    }
  }
}
