import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ResponseMessage, WalkInDrive, appliedJobRole, educationQualification, professionalQualification, registrationData } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  constructor(private http: HttpClient) { }

  getRegistrationData() {
    let url = "http://localhost:5161/RegisterData";
    return this.http.get<registrationData>(url)
  }

  insertEducationData(user: educationQualification) {
    let url = "http://localhost:5161/api/User/InsertEducationData";
    return this.http.post<ResponseMessage>(url, user);
  }

  insertProfessionalData(user: professionalQualification) {
    let url = "http://localhost:5161/api/User/InsertProfessionalData";
    return this.http.post<ResponseMessage>(url, user);
  }

  getWalkInDrives(){
    let url = "http://localhost:5161/api/Job/WalkInDrives";
    return this.http.get<WalkInDrive[]>(url);
  }

  getWalkInDrive(id: number){

    let url = `http://localhost:5161/api/Job/WalkInDrive/${id}`;
    return this.http.get<WalkInDrive>(url)
  }

  insertUserAppliedJob(job: appliedJobRole){
     
    let url = "http://localhost:5161/api/Job/InsertUserAppliedJob";
    return this.http.post<ResponseMessage>(url, job);
  }


}
