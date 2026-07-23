import { Component, input, output, signal, effect } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardVehicle } from '../../models/dashboard-vehicle.model';
import { DashboardBooking } from '../../models/dashboard-booking.model';


@Component({
  selector: 'app-dashboard-timeline',
  imports: [CommonModule],
  templateUrl: './timeline.html',
  styleUrl: './timeline.scss',
})
export class DashboardTimeline {

  constructor() {
    effect(() => {
      console.log('TIMELINE VEHICLES:', this.vehicles());
      console.log('TIMELINE BOOKINGS:', this.bookings());
    });
  }

  vehicles = input<DashboardVehicle[]>([]);
  bookings = input<DashboardBooking[]>([]);

  bookingSelected = output<DashboardBooking>();

  hoveredBooking = signal<DashboardBooking | null>(null);

  tooltipX = 0;
  tooltipY = 0;

  readonly hourWidth = 60;
  readonly minutesPerHour = 60;


  readonly hours = Array.from(
    { length: 24 },
    (_, i) => i.toString().padStart(2, '0')
  );


  getBookings(vehicleId: string): DashboardBooking[] {

    return this.bookings()
      .filter(x => x.vehicleId === vehicleId);

  }


  getHourPosition(hour: string): number {

    return Number(hour) * this.hourWidth;

  }


  /**
   * Timeline uses business time.
   * API sends DateTimeOffset UTC (+00:00),
   * but timeline should display the original scheduled time.
   */
  getLeft(time: string): number {

    const minutes = this.getTimeMinutes(time);

    return this.toPixel(minutes);

  }


  getWidth(
    start: string,
    end: string
  ): number {

    const durationMinutes =
      (
        new Date(end).getTime()
        -
        new Date(start).getTime()
      ) / 60000;


    return this.toPixel(durationMinutes);

  }


  private getTimeMinutes(time: string): number {

    const hour = Number(time.substring(11, 13));
    const minute = Number(time.substring(14, 16));

    return (
      hour * this.minutesPerHour
      +
      minute
    );

  }


  private toPixel(minutes: number): number {

    return minutes *
      (this.hourWidth / this.minutesPerHour);

  }


  getBookingColor(status: string): string {

    switch (status) {

      case 'Created':
        return '#9e9e9e';

      case 'Pending':
        return '#f9a825';

      case 'Confirmed':
        return '#1976d2';

      case 'Completed':
        return '#2e7d32';

      case 'Cancelled':
        return '#c62828';

      default:
        return '#757575';
    }

  }


  getTripColor(status: string | null): string {

    switch (status) {

      case 'Ready':
        return '#757575';

      case 'EnRoute':
        return '#1565c0';

      case 'Completed':
        return '#2e7d32';

      case 'Cancelled':
        return '#c62828';

      default:
        return '#757575';
    }

  }


  openBookingDetail(booking: DashboardBooking): void {

    this.bookingSelected.emit(booking);

  }


  showBookingInfo(
    booking: DashboardBooking,
    event: MouseEvent
  ): void {

    this.hoveredBooking.set(booking);

    this.tooltipX = event.clientX + 10;
    this.tooltipY = event.clientY + 10;

  }


  hideBookingInfo(): void {

    this.hoveredBooking.set(null);

  }


  getNowPosition(): number {

    const now = new Date();

    const minutes =
      now.getHours() * this.minutesPerHour
      +
      now.getMinutes();


    return this.toPixel(minutes);

  }

  formatBusinessTime(value: string | null | undefined): string {

    if (!value) {
      return '-';
    }

    return value.substring(0, 16).replace('T', ' ');
  }

}