import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';
import { ListingPageComponent } from './listing-page/listing-page.component';
import { JobRoleComponent } from './job-role/job-role.component';
import { HallTicketComponent } from './hall-ticket/hall-ticket.component';

export const routes: Routes = [
    {
        path:'',
        component: LoginComponent
    },
    {
        path:'registration',
        component: RegistrationComponent
    },
    {
        path: 'list-job',
        component: ListingPageComponent
    },
    {
        path: 'list-job/job/:id',
        component: JobRoleComponent
    },
    {
        path: 'drive/applied/:id',
        component: HallTicketComponent
    }
];
