import { Component, OnInit, inject, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { BookingService } from '../booking.service';
import { Booking } from '../booking';

import { TripService } from '../../trip/trip.service';
import { LookupDialog } from '../../../core/components/lookup-dialog/lookup-dialog';
import { LookupService } from '../../../core/services/lookup';
import { LookupItem } from '../../../core/models/lookup-item.model';


@Component({
  selector: 'app-booking-detail',
  imports: [LookupDialog],
  templateUrl: './booking-detail.html',
  styleUrl: './booking-detail.scss',
})
export class BookingDetail implements OnInit {
  private route = inject(ActivatedRoute);
  private bookingService = inject(BookingService);
  private tripService = inject(TripService);
  private lookupService = inject(LookupService);

  booking = signal<Booking | null>(null);
  trip = signal<any | null>(null);

  showDriverLookup = false;
  drivers = signal<LookupItem[]>([]);

  showPassengerLookup = false;
  passengers = signal<LookupItem[]>([]);

  selectedPassenger = signal<LookupItem | null>(null);
  pickupLocation = signal('');

  showGuestPassenger = false;
  guestName = signal('');
  guestPhone = signal('');

  showCancelTripDialog = false;
  cancelReason = signal('');

  showChangePickupLocation = false;
  bookingPassengerId = signal('');

  showStartTripDialog = false;
  showFinishTripDialog = false;
  tripTimeRequest = signal('');

  ngOnInit(): void {

    this.lookupService.getDrivers().subscribe(res => {
      this.drivers.set(res.data);
    });

    this.lookupService.getPersons().subscribe(res => {
      this.passengers.set(res.data);
    });

    const id =
      this.route.snapshot.paramMap.get('id');
    if (!id) {
      return;
    }
    this.loadBooking(id);
  }

  loadBooking(id: string): void {
    this.bookingService
      .getBooking(id)
      .subscribe(res => {
        this.booking.set(res.data);

        if (res.data.status === 'Confirmed' || res.data.status === 'Completed') {
          this.loadTrip(res.data.bookingId);
        } else {
          this.trip.set(null);
        }

      });
  }

  loadTrip(bookingId: string): void {
    this.tripService
      .getByBookingId(bookingId)
      .subscribe({
        next: (res: any) => {
          this.trip.set(res.data);
        },
        error: () => {
          this.trip.set(null);
        }
      });
  }

  confirmBooking(): void {
    const item = this.booking();
    if (!item) {
      return;
    }

    this.bookingService
      .confirmBooking(item.bookingId)
      .subscribe({
        next: () => {
          this.loadBooking(item.bookingId);
        },
        error: (err) => {
          alert(err.error?.message ?? 'Failed to confirm booking.');
        }
      });
  }

  cancelBooking(): void {

    if (!confirm('Are you sure you want to cancel this booking?')) {
      return;
    }

    const item = this.booking();
    if (!item) {
      return;
    }

    this.bookingService
      .cancelBooking(item.bookingId)
      .subscribe({
        next: () => {
          this.loadBooking(item.bookingId);
        },
        error: (err) => {
          alert(err.error?.message ?? 'Failed to cancel booking.');
        }
      });
  }

  selectDriver(item: LookupItem): void {
    const booking = this.booking();

    if (!booking) {
      return;
    }

    this.bookingService
      .assignDriver(
        booking.bookingId,
        item.lookupId
      )
      .subscribe({
        next: () => {
          this.showDriverLookup = false;
          this.loadBooking(booking.bookingId);
        },
        error: (err) => {
          alert(err.error?.message ?? 'Failed select driver.');
        }
      });
  }

  openDriverLookup(): void {
    this.showDriverLookup = true;
  }

  removeDriver(): void {
    const booking = this.booking();
    if (!booking) {
      return;
    }
    this.bookingService
      .removeDriver(booking.bookingId)
      .subscribe({
        next: () => {
          this.loadBooking(booking.bookingId);
        },
        error: (err) => {
          alert(err.error?.message ?? 'Failed remove driver.');
        }
      });
  }

  selectPassenger(item: LookupItem): void {
    this.selectedPassenger.set(item);
    this.showPassengerLookup = false;
  }

  openPassengerLookup(): void {
    this.showPassengerLookup = true;
  }

  cancelSavePassenger(): void {
    this.pickupLocation.set('');
    this.selectedPassenger.set(null);
  }

  savePassenger(): void {

    const booking = this.booking();
    const passenger = this.selectedPassenger();

    if (!booking || !passenger) {
      return;
    }

    if (!this.pickupLocation().trim()) {
      alert('Pickup location is required');
      return;
    }

    const request = {
      personId: passenger.lookupId,
      pickupLocation: this.pickupLocation()
    };

    this.bookingService
      .addRegularPassenger(
        booking.bookingId,
        request
      )
      .subscribe({
        next: () => {
          this.selectedPassenger.set(null);
          this.pickupLocation.set('');
          this.loadBooking(booking.bookingId);
        },
        error: (err) => {
          alert(err.error?.message ?? 'Failed to add passenger.');
        }
      });

  }

  removePassenger(bookingPassengerId: string): void {
    const booking = this.booking();
    if (!booking) {
      return;
    }
    this.bookingService
      .removePassenger(booking.bookingId, bookingPassengerId)
      .subscribe({
        next: () => {
          this.loadBooking(booking.bookingId);
        },
        error: (err) => {
          alert(err.error?.message ?? 'Failed to remove passenger.');
        }
      });
  }

  openChangePickupLocation(passengerId: string,) {
    this.bookingPassengerId.set(passengerId);
    this.showChangePickupLocation = true;
  }

  cancelChangePickupLocation(): void {
    this.pickupLocation.set('');
    this.showChangePickupLocation = false;
  }

  changePickupLocation(): void {
    const booking = this.booking();

    if (!booking) {
      return;
    }

    if (!this.pickupLocation().trim()) {
      alert('Pickup location is required');
      return;
    }

    const request = {
      bookingPassengerId: this.bookingPassengerId(),
      pickupLocation: this.pickupLocation()
    };

    this.bookingService
      .changePickupLocationPassenger(booking.bookingId, request)
      .subscribe({
        next: () => {
          this.showChangePickupLocation = false;
          this.pickupLocation.set('');
          this.loadBooking(booking.bookingId);
        },
        error: (err) => {
          alert(err.error?.message ?? 'Failed to change pickup location.');
        }
      });
  }

  openGuestPassenger(): void {
    this.showGuestPassenger = true;
  }

  closeGuestPassenger(): void {
    this.guestName.set('');
    this.guestPhone.set('');
    this.pickupLocation.set('');
    this.showGuestPassenger = false;
  }

  saveGuestPassenger(): void {

    const booking = this.booking();

    if (!booking) {
      return;
    }

    if (
      !this.guestName().trim()
      ||
      !this.guestPhone().trim()
      ||
      !this.pickupLocation().trim()
    ) {
      alert('Guest name, phone and pickup location are required');
      return;
    }
    const request = {
      guestName: this.guestName(),
      guestPhone: this.guestPhone(),
      pickupLocation: this.pickupLocation()
    };
    this.bookingService
      .addGuestPassenger(
        booking.bookingId,
        request
      )
      .subscribe({
        next: () => {
          this.showGuestPassenger = false;

          this.guestName.set('');
          this.guestPhone.set('');
          this.pickupLocation.set('');

          this.loadBooking(booking.bookingId);
        },
        error: (err) => {
          alert(err.error?.message ?? 'Failed add guest.');
        }
      });

  }

  openCancelTrip(): void {

    this.cancelReason.set('');
    this.showCancelTripDialog = true;

  }

  confirmCancelTrip(): void {

    const currentTrip = this.trip();
    if (!currentTrip) {
      return;
    }

    if (!this.cancelReason().trim()) {
      alert('Cancel reason is required');
      return;
    }

    const request = {
      cancelReason: this.cancelReason(),
    };

    this.tripService
      .cancelTrip(currentTrip.tripId, request)
      .subscribe({
        next: () => {
          this.showCancelTripDialog = false;
          this.cancelReason.set('');
          this.loadTrip(currentTrip.bookingId);
        },
        error: (err) => {
          alert(err.error?.message ?? 'Failed cancel trip.');
        }
      });

  }

  closeConfirmCancel(): void {
    this.showCancelTripDialog = false;
    this.cancelReason.set('');
  }

  showStartTrip(): void {
    this.tripTimeRequest.set('');
    this.showStartTripDialog = true;
  }

  confirmStartTrip(): void {

    const currentTrip = this.trip();
    if (!currentTrip) {
      return;
    }

    if (!this.tripTimeRequest().trim()) {
      alert('Time is required.');
      return;
    }

    const request = {
      StartTripTime: this.tripTimeRequest(),
    };

    this.tripService
      .startTrip(currentTrip.tripId, request)
      .subscribe({
        next: () => {
          this.tripTimeRequest.set('');
          this.showStartTripDialog = false;
          this.loadTrip(currentTrip.bookingId);
        },
        error: (err) => {
          alert(err.error?.message ?? 'Failed to start trip.');
        }
      });

  }

  closeConfirmStartTrip(): void {
    this.tripTimeRequest.set('');
    this.showStartTripDialog = false;
  }

  showFinishTrip(): void {
    this.tripTimeRequest.set('');
    this.showFinishTripDialog = true;
  }

  confirmFinishTrip(): void {

    const currentTrip = this.trip();
    if (!currentTrip) {
      return;
    }

    if (!this.tripTimeRequest().trim()) {
      alert('Time is required.');
      return;
    }

    const request = {
      FinishTripTime: this.tripTimeRequest(),
    };

    this.tripService
      .finishTrip(currentTrip.tripId, request)
      .subscribe({
        next: () => {
          this.tripTimeRequest.set('');
          this.showFinishTripDialog = false;
          this.loadTrip(currentTrip.bookingId);
        },
        error: (err) => {
          alert(err.error?.message ?? 'Failed to start trip.');
        }
      });

  }

  closeConfirmFinishTrip(): void {
    this.tripTimeRequest.set('');
    this.showFinishTripDialog = false;
  }

  formatBusinessTime(value: string | null): string {

    if (!value) {
      return '-';
    }

    return value.substring(0, 16).replace('T', ' ');
  }

}
