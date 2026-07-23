import { FormsModule } from '@angular/forms';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';

import { Component, computed, inject, signal } from '@angular/core';

import { DashboardService } from './dashboard.service';
import { BookingService } from '../booking/booking.service';

import { DashboardBooking } from './models/dashboard-booking.model';
import { DashboardVehicle } from './models/dashboard-vehicle.model';

import { DashboardTimeline } from './components/timeline/timeline';
import { VehicleService } from '../vehicle/vehicle.service';

@Component({
  selector: 'app-dashboard',
  imports: [
    FormsModule,
    MatFormFieldModule,
    MatSelectModule,
    DashboardTimeline,
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss',
})
export class Dashboard {

  private dashboardService = inject(DashboardService);
  private bookingService = inject(BookingService);
  private vehicleService = inject(VehicleService);


  selectedDate = signal<Date>(
    new Date(
      new Date().getFullYear(),
      new Date().getMonth(),
      new Date().getDate()
    )
  );


  vehicles = signal<DashboardVehicle[]>([]);
  bookings = signal<DashboardBooking[]>([]);
  selectedVehicleIds = signal<string[]>([]);

  loading = signal(true);


  private vehiclesLoaded = false;
  private bookingsLoaded = false;


  constructor() {

    this.loadVehicles();
    this.loadBookings(this.selectedDate());

  }


  private checkLoadingComplete(): void {

    if (
      this.vehiclesLoaded
      &&
      this.bookingsLoaded
    ) {

      this.loading.set(false);

    }

  }


  loadBookings(date: Date): void {

    this.bookingsLoaded = false;
    this.loading.set(true);


    this.dashboardService
      .getBookingsByDate(date)
      .subscribe({

        next: (res) => {

          this.bookings.set(res.data);

          this.bookingsLoaded = true;

          this.checkLoadingComplete();

        },

        error: (err) => {

          console.error(
            'Failed Load Dashboard Bookings',
            err
          );

          this.loading.set(false);

        }

      });

  }


  loadVehicles(): void {

    this.vehiclesLoaded = false;
    this.loading.set(true);


    this.vehicleService
      .getVehicles()
      .subscribe({

        next: (res) => {

          this.vehicles.set(
            res.data.map(vehicle => ({

              vehicleId: vehicle.vehicleId,
              vehicleCode: vehicle.vehicleCode

            }))
          );


          this.vehiclesLoaded = true;

          this.checkLoadingComplete();

        },


        error: (err) => {

          console.error(
            'Failed Load Vehicles',
            err
          );

          this.loading.set(false);

        }

      });

  }


  filteredBookings = computed(() => {

    const date = this.selectedDate();

    return this.bookings()
      .filter(booking => {

        const bookingDate =
          new Date(
            booking.estimatedDepartureTime
          );


        return (
          bookingDate.getFullYear()
          === date.getFullYear()
          &&
          bookingDate.getMonth()
          === date.getMonth()
          &&
          bookingDate.getDate()
          === date.getDate()
        );

      });

  });


  visibleVehicles = computed(() => {

    const selected =
      this.selectedVehicleIds();


    if (selected.length === 0) {

      return this.vehicles();

    }


    return this.vehicles()
      .filter(vehicle =>
        selected.includes(
          vehicle.vehicleId
        )
      );

  });


  changeDate(days: number): void {

    const current =
      this.selectedDate();


    const next =
      new Date(
        current.getFullYear(),
        current.getMonth(),
        current.getDate()
      );


    next.setDate(
      next.getDate() + days
    );


    this.selectedDate.set(next);


    this.loadBookings(next);

  }


  formatDate(): string {

    return this.selectedDate()
      .toLocaleDateString(
        'en-GB',
        {
          day: '2-digit',
          month: 'short',
          year: 'numeric'
        }
      );

  }


  openBooking(booking: DashboardBooking): void {

    this.bookingService
      .openBookingDetail(booking);

  }

}