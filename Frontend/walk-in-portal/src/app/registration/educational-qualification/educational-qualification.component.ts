import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { educationQualification, registrationData } from '../../models';
import { DataService } from '../../services/data.service';

@Component({
  selector: 'educational-qualification',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './educational-qualification.component.html',
  styleUrl: './educational-qualification.component.css',
})
export class EducationalQualificationComponent implements OnInit{
  @Input() educationChildData!: any;
  // @Input() educationQualification: education_qualification

  educationQualification!: educationQualification;
  constructor(private data: DataService) {
    this.educationQualification = data.educationQualification;
  }
  ngOnInit(): void {}

  
}
