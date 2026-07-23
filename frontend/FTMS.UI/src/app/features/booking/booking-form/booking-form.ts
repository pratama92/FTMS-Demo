import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';

import { BookingService } from '../booking.service';
import { LookupService } from '../../../core/services/lookup';
import { LookupItem } from '../../../core/models/lookup-item.model';
import { LookupDialog } from '../../../core/components/lookup-dialog/lookup-dialog';


@Component({
  selector: 'app-booking-form',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    LookupDialog
  ],
  templateUrl: './booking-form.html',
  styleUrl: './booking-form.scss',
})
export class BookingForm implements OnInit {

  private fb = inject(FormBuilder);
  private bookingService = inject(BookingService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private lookupService = inject(LookupService);
  bookingId = '';

  vehicles: LookupItem[] = [];
  showVehicleLookup = false;
  selectedVehicle = '';

  form = this.fb.group({

    vehicleId: [
      '',
      Validators.required
    ],
    destinationLocation: [
      '',
      Validators.required
    ],
    estimatedDepartureTime: [
      '',
      Validators.required
    ],
    estimatedArrivalTime: [
      '',
      Validators.required
    ]

  });

  ngOnInit(): void {

    this.lookupService.getVehicles().subscribe(res => {
      this.vehicles = res.data;
    });

    this.bookingId =
      this.route.snapshot.paramMap.get('id') ?? '';

    if (!this.bookingId) {
      return;
    }

    this.bookingService
      .getBooking(this.bookingId)
      .subscribe({
        next: (res) => {
          this.form.patchValue(res.data);
          const vehicle = this.vehicles.find(
            x => x.lookupId === res.data.vehicleId
          );
          if (vehicle) {
            this.selectedVehicle = vehicle.lookupName;
          }
        },
        error: (err) => {
          alert(
            err.error?.message ?? 'Failed.'
          );
        }
      });
  }

  save(): void {

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const request = this.form.getRawValue();

    if (!this.bookingId) {
      this.bookingService
        .createBooking(request)
        .subscribe({
          next: () => {
            this.router.navigate(['/bookings']);
          },
          error: (err) => {
            alert(
              err.error?.message ?? 'Failed.'
            );
          }
        });
      return;
    }

    this.bookingService
      .updateBooking(this.bookingId, request)
      .subscribe({
        next: () => {
          this.router.navigate(['/bookings']);
        },
        error: (err) => {
          alert(
            err.error?.message ?? 'Failed.'
          );
        }
      });
  }

  openVehicleLookup(): void {
    this.showVehicleLookup = true;
  }

  selectVehicle(item: LookupItem): void {

    this.form.patchValue({
      vehicleId: item.lookupId
    });

    this.form.controls.vehicleId.markAsTouched();

    this.selectedVehicle = item.lookupName;
    this.showVehicleLookup = false;
  }


}