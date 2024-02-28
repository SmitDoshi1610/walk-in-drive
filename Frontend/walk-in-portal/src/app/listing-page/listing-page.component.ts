import { Component, OnInit, inject } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import {faLocationDot} from '@fortawesome/free-solid-svg-icons'
import { ApiService } from '../services/api.service';
import { WalkInDrive } from '../models';

@Component({
  selector: 'app-listing-page',
  standalone: true,
  imports: [FontAwesomeModule, RouterModule],
  templateUrl: './listing-page.component.html',
  styleUrl: './listing-page.component.css'
})
export class ListingPageComponent implements OnInit {

   locationIcon = faLocationDot
   drives!: WalkInDrive[]
   constructor(private router : Router, private api: ApiService)
   {

   }
   ngOnInit(): void {
    this.api.getWalkInDrives().subscribe((result: WalkInDrive[]) => {
       this.drives = result
      
    })
   }
   move(){
    this.router.navigate(['/job/1'])
   }
}
