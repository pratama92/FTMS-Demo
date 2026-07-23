import { Component, computed, inject, signal } from '@angular/core';

import { Booking } from '../booking';
import { BookingService } from '../booking.service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-booking-list',
  imports: [RouterLink],
  templateUrl: './booking-list.html',
  styleUrl: './booking-list.scss',
})
export class BookingList {

  private bookingService = inject(BookingService);

  bookings = signal<Booking[]>([]);
  searchText = signal('');
  statusFilter = signal('All');

  constructor() {
    this.loadBookings();
  }

  loadBookings(): void {

    this.bookingService.getBookings()
      .subscribe({
        next: (res) => {
          this.bookings.set(res.data);
        },
        error: (err) => {
          alert(
            err.error?.message ?? 'Failed.'
          );
        }
      });
  }

  filteredBookings = computed(() => {

    const search = this.searchText()
      .toLowerCase();

    const status = this.statusFilter();


    return this.bookings()
      .filter(booking => {

        const matchSearch =
          !search ||
          booking.bookingNumber.toLowerCase().includes(search) ||
          booking.destinationLocation.toLowerCase().includes(search) ||
          booking.vehicleCode.toLowerCase().includes(search) ||
          (booking.driverName ?? '').toLowerCase().includes(search);


        const matchStatus =
          status === 'All' ||
          booking.status === status;


        return matchSearch && matchStatus;

      });

  });

  updateSearch(value: string): void {

    this.searchText.set(value);

  }


  updateStatus(value: string): void {

    this.statusFilter.set(value);

  }

  formatBusinessTime(value: string | null): string {

    if (!value) {
      return '-';
    }

    return value.substring(0, 16).replace('T', ' ');
  }

}