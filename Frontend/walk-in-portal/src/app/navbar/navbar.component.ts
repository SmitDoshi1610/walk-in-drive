import { Component, OnInit } from '@angular/core';
import { UtilityService } from '../services/utility.service';
import { User } from '../models';

@Component({
  selector: 'navbar',
  standalone: true,
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit{

  currentUrl?:string
  userName:string = ""
  constructor(private utility: UtilityService){}
  ngOnInit(): void {
    if (typeof window !== "undefined")
    {
      this.currentUrl = window.location.pathname
    }

    if (this.utility.isLoggedIn())
    {
       const user:User = this.utility.getUser();
       this.userName = `Welcome ${user.firstname}`;
    }
    
  }
}
