import { Component, OnInit, inject, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';

import { VehicleService } from '../vehicle.service';

@Component({
  selector: 'app-vehicle-form',
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './vehicle-form.html',
  styleUrl: './vehicle-form.scss',
})
export class VehicleForm implements OnInit {

  private fb = inject(FormBuilder);
  private vehicleService = inject(VehicleService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  vehicleId = signal('');

  form = this.fb.group({

    vehicleCode: ['', Validators.required],
    licensePlate: ['', Validators.required],
    chassisNumber: ['', Validators.required],
    engineNumber: ['', Validators.required],
    brand: ['', Validators.required],
    model: ['', Validators.required],
    year: [2025, Validators.required],
    color: [''],
    vehicleType: ['MPV', Validators.required],
    fuelType: ['Gasoline', Validators.required],
    drivetrain: ['FWD', Validators.required],
    transmission: ['Automatic', Validators.required],
    seatCapacity: [4, Validators.required],
    cargoCapacity: [0]

  });

  ngOnInit(): void {

    this.vehicleId.set(this.route.snapshot.paramMap.get('id') ?? '');

    if (!this.vehicleId()) {
      return;
    }

    this.vehicleService
      .getVehicle(this.vehicleId())
      .subscribe({
        next: (res) => {
          this.form.patchValue(res.data);
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

    if (!this.vehicleId()) {
      this.vehicleService
        .createVehicle(request)
        .subscribe({
          next: () => {
            this.router.navigate(['/vehicles']);
          },
          error: (err) => {
            alert(err.error?.message ?? 'Failed.');
          }
        });

      return;
    }

    this.vehicleService
      .updateVehicle(this.vehicleId(), request)
      .subscribe({
        next: () => {
          this.router.navigate(['/vehicles']);
        },
        error: (err) => {
          alert(err.error?.message ?? 'Failed.');
        }
      });
  }


}