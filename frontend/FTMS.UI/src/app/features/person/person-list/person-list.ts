import { Component, OnInit, inject, signal } from '@angular/core';

import { Person } from '../person';
import { PersonService } from '../person.service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-person-list',
  imports: [RouterLink],
  templateUrl: './person-list.html',
  styleUrl: './person-list.scss',
})

export class PersonList implements OnInit {

  private personService = inject(PersonService);
  persons = signal<Person[]>([]);

  ngOnInit(): void {
    this.loadPersons();
  }


  loadPersons(): void {

    this.personService.getPersons()
      .subscribe({
        next: (res) => {
          this.persons.set(res.data);
        },
        error: (err) => {
          alert(err.error?.message ?? 'Failed.');
        }
      });
  }

  deletePerson(id: string): void {

    if (!confirm('Delete this person?')) {
      return;
    }

    this.personService.deletePerson(id)
      .subscribe({
        next: () => {
          this.loadPersons();
        },
        error: err => {
          alert(err.error?.message ?? 'Failed.');
        }
      });

  }

  addDriverRole(id: string): void {

    if (!confirm('Assign Driver Role?')) {
      return;
    }

    this.personService.addDriverRolePerson(id)
      .subscribe({
        next: () => {
          this.loadPersons();
        },
        error: err => {
          alert(err.error?.message ?? 'Failed.');
        }
      });
  }

  removeDriverRole(id: string): void {

    if (!confirm('Remove Driver Role?')) {
      return;
    }

    this.personService.removeDriverRolePerson(id)
      .subscribe({
        next: () => {
          this.loadPersons();
        },
        error: err => {
          alert(err.error?.message ?? 'Failed.');
        }
      });
  }

}