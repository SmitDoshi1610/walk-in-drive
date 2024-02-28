import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class JobRoleService {
  jobRoles = [
    {
      id: 1,
      title: 'Walk In for Multiple Job Roles',
      date: '03-Jul-2021 to 04-Jul-2021',
      location: 'Mumbai',
      job: [
        {
          image: 'Instructional Designer',
          name: 'Instructional Designer',
        },
        {
          image: 'Software Engineer',
          name: 'Software Engineer',
        },
      ],
      timeSlots: [
        "9:00 AM to 11:00 AM",
        "1:00 PM to 3:00 PM"
      ],
      preference: [
        "Instructional Designer",
        "Software Engineer",
        "Software Quality Engineer"
      ],
      role: {
        "Instructional Designer": {
          compansation: 500000,
          roleDescription: "Generate highly interactive and innovative instructional strategies for e-learning solutions",
          requirements: "Experience in creating instructional plans and course maps."

        },
        "Software Engineer": {
          compansation: 800000,
          roleDescription: "Generate highly interactive and innovative instructional strategies for e-learning solutions",
          requirements: "Experience in creating instructional plans and course maps."
        },
        "Software Quality Engineer": {
          compansation: 700000,
          roleDescription: "Generate highly interactive and innovative instructional strategies for e-learning solutions",
          requirements: "Experience in creating instructional plans and course maps."
        }
      }
    }
  ];
  constructor() { }
}
