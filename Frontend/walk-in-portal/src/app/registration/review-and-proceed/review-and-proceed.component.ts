import { Component, Input } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faPen } from '@fortawesome/free-solid-svg-icons'
import { DataService } from '../../services/data.service';
import { educationQualification, personalInformation, professionalQualification } from '../../models';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { EducationalQualificationComponent } from '../educational-qualification/educational-qualification.component';
import { ProfessionalQualificationComponent } from '../professional-qualification/professional-qualification.component';
import { RegistrationComponent } from "../registration.component";

@Component({
    selector: 'review-and-proceed',
    standalone: true,
    templateUrl: './review-and-proceed.component.html',
    styleUrl: './review-and-proceed.component.css',
    imports: [FontAwesomeModule, FormsModule, RouterModule, EducationalQualificationComponent,
        ProfessionalQualificationComponent, RouterModule, RegistrationComponent]
})
export class ReviewAndProceedComponent {

  @Input() reviewChildData!: any;
  editIcon = faPen
  personalInformation!: personalInformation;
  educationQualification!: educationQualification;
  professionalQualification!: professionalQualification

  constructor(private data: DataService, private router: Router) {
    this.personalInformation = data.personalInformation;
    this.educationQualification = data.educationQualification
    this.professionalQualification = data.professionalQualification
  }
}
