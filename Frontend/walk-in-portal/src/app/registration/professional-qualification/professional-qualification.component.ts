import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { professionalQualification } from '../../models';
import { DataService } from '../../services/data.service';
import { UtilityService } from '../../services/utility.service';

@Component({
  selector: 'professional-qualification',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './professional-qualification.component.html',
  styleUrl: './professional-qualification.component.css'
})
export class ProfessionalQualificationComponent implements OnInit {
  @Input() professionalChildData!: any;
  tempFresherTechnology: string[]
  tempTechnologyExpert: string[]
  tempExperienceTechnology: string[]

  professionalQualification!: professionalQualification;

  constructor(private data: DataService, private utility: UtilityService) {
    this.tempFresherTechnology = []
    this.tempTechnologyExpert = []
    this.tempExperienceTechnology = []
  }

  ngOnInit(): void {
    this.professionalQualification = this.data.professionalQualification;
  }
  fresherFConvertBooleanToIndex() {
    this.professionalQualification.fresherTechnologiesFamiliar.forEach((key: any, value: number) => {
      if (key == true) {
        this.professionalQualification.fresherTechnologiesFamiliar[value] = (value + 1).toString();
        this.tempFresherTechnology[value] = (value + 1).toString();
      }
      else {
        if (this.tempFresherTechnology[value] > "0" && key == true) {
          this.tempFresherTechnology[value] = "0";
          this.professionalQualification.fresherTechnologiesFamiliar[value] = "0";
        }
      }
      this.professionalQualification.fresherTechnologiesFamiliar = this.tempFresherTechnology;
    })
  }

  expertConvertBooleanToIndex() {
    this.professionalQualification.technologiesExpert.forEach((key: any, value: number) => {
      if (key == true) {
        this.professionalQualification.technologiesExpert[value] = (value + 1).toString();
        this.tempTechnologyExpert[value] = (value + 1).toString();
      }
      else {
        if (this.tempTechnologyExpert[value] > "0" && key == true) {
          this.tempTechnologyExpert[value] = "0";
          this.professionalQualification.technologiesExpert[value] = "0";
        }
      }
      this.professionalQualification.technologiesExpert = this.tempTechnologyExpert;
    })
  }

  experienceFConvertBooleanToIndex() {
    this.professionalQualification.experienceTechnologiesFamiliar.forEach((key: any, value: number) => {
      if (key == true) {
        // this.tempPreferedJobRole.push(value + 1);
        this.professionalQualification.experienceTechnologiesFamiliar[value] = (value + 1).toString();
        this.tempExperienceTechnology[value] = (value + 1).toString();
      }
      else {
        if (this.tempExperienceTechnology[value] > "0" && key == true)
        {
          this.tempExperienceTechnology[value] = "0";
          this.professionalQualification.experienceTechnologiesFamiliar[value] = "0";
        }
      }
      this.professionalQualification.experienceTechnologiesFamiliar = this.tempExperienceTechnology;
    })
  }


}
