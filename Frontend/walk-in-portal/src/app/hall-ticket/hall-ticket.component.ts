import { Component } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faCheck } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-hall-ticket',
  standalone: true,
  imports: [FontAwesomeModule],
  templateUrl: './hall-ticket.component.html',
  styleUrl: './hall-ticket.component.css'
})
export class HallTicketComponent {

   correctIcon = faCheck
}
