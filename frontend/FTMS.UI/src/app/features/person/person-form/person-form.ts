import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PersonService } from '../person.service';

@Component({
  selector: 'app-person-form',
  imports: [ReactiveFormsModule],
  templateUrl: './person-form.html',
  styleUrl: './person-form.scss',
})
export class PersonForm implements OnInit {

  private personService = inject(PersonService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private fb = inject(FormBuilder);
  personId = signal('');

  form = this.fb.group({

    name: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    phone: ['', Validators.required]

  });

  ngOnInit(): void {

    this.personId.set(
      this.route.snapshot.paramMap.get('id') ?? ''
    );

    if (!this.personId()) {
      return;
    }

    this.personService
      .getPerson(this.personId())
      .subscribe({
        next: (res) => {
          this.form.patchValue({
            name: res.data.name,
            email: res.data.email,
            phone: res.data.phone
          });
        },
        error: (err) => {
          alert(err.error?.message ?? 'Failed.');
        }
      });

  }

  save(): void {

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const request = this.form.getRawValue();

    if (!this.personId()) {

      this.personService
        .createPerson(request)
        .subscribe({
          next: () => {
            this.router.navigate(['/persons']);
          },
          error: (err) => {
            alert(err.error?.message ?? 'Failed to create person.');
          }
        });

      return;

    }

    this.personService
      .updatePerson(this.personId(), request)
      .subscribe({
        next: () => {
          this.router.navigate(['/persons']);
        },
        error: (err) => {
          alert(err.error?.message ?? 'Failed to update person.');
        }
      });

  }

}