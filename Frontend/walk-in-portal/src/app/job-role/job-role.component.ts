import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faAngleDown, faAngleUp, faArrowUp, faLocationDot } from '@fortawesome/free-solid-svg-icons';
import { ResponseMessage, User, WalkInDrive, appliedJobRole } from '../models';
import { ApiService } from '../services/api.service';
import { FormsModule } from '@angular/forms';
import { UtilityService } from '../services/utility.service';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-job-role',
  standalone: true,
  imports: [FontAwesomeModule, FormsModule, RouterModule],
  templateUrl: './job-role.component.html',
  styleUrl: './job-role.component.css'
})
export class JobRoleComponent implements OnInit {

  jobId?: number;
  jobRole!: WalkInDrive;
  locationIcon = faLocationDot
  accordianIconUp = faAngleUp
  accordianIconDown = faAngleDown
  isapplicationBodyVisible!: boolean;
  resumeIcon = faArrowUp;
  isJobApplicationVisible!: boolean[]
  userAppliedJobRole!: appliedJobRole
  tempPreference!: string[]

  constructor(private activatedRoute: ActivatedRoute, private router: Router, private api: ApiService, private utility: UtilityService, private toast: NgToastService) { }

  ngOnInit(): void {

    this.jobId = Number(this.activatedRoute.snapshot.paramMap.get('id'))

    this.api.getWalkInDrive(this.jobId).subscribe((result: WalkInDrive) => {
      this.jobRole = result;
      console.log(this.jobRole);
    })

    this.isapplicationBodyVisible = false
    this.isJobApplicationVisible = new Array<boolean>(false);
    this.userAppliedJobRole = {
      userId:0,
      driveId: 0,
      jobPreference: new Array<string>(),
      resume: '',
      timeSlot: ''
    }
    this.tempPreference = new Array<string>();
  }
  isApplicationAccordianVisible(event: any) {
    event.stopPropagation()
    this.isapplicationBodyVisible = !this.isapplicationBodyVisible
  }
  isJobAccordianVisible(id: number) {
    this.isJobApplicationVisible[id] = !this.isJobApplicationVisible[id]
  }
  convertBooleanToIndex() {

    this.userAppliedJobRole.jobPreference.forEach((key: any, value: number) => {
      if (key == true) {

        this.userAppliedJobRole.jobPreference[value] = (value).toString();
        this.tempPreference[value] = (value).toString();
      }
      else {
        if (this.tempPreference[value] > "0" && key == true) {
          this.tempPreference[value] = "0";
          this.userAppliedJobRole.jobPreference[value] = "0";
        }

      }
      this.userAppliedJobRole.jobPreference = this.tempPreference;
    })
    console.log(this.userAppliedJobRole.jobPreference);
  }

  applyForJob()
  {
     if (this.utility.isLoggedIn())
     {
        const user:User = this.utility.getUser();
        this.userAppliedJobRole.userId = Number(user.id);
        this.userAppliedJobRole.driveId = this.jobRole.id;

        this.api.insertUserAppliedJob(this.userAppliedJobRole).subscribe((result: ResponseMessage) => {
           if (result.statusCode == 409)
           {
             this.toast.error({detail:"ERROR", summary:result.message, duration:3000, position:'topCenter'});
             this.router.navigate(['list-job']);
           }
           else if(result.moveNext)
           {
             this.toast.success({detail:"SUCCESS", summary:result.message, duration:3000, position:'topCenter'});
             this.router.navigate([`drive/applied/${user.id}`]);
           }
           else
           {
              this.toast.error({detail: "ERROR", summary:result.message, duration:3000, position:'topCenter'});
           }
        })
     }
     else
     {
        this.toast.error({detail:"Error", summary:"Please Login", duration:3000, position:'topCenter'});
        this.router.navigate(['']);
     }
  }
}
