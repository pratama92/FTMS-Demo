import { Component, OnInit, inject, signal } from '@angular/core';

import { Vehicle } from '../vehicle';
import { VehicleService } from '../vehicle.service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-vehicle-list',
  imports: [RouterLink],
  templateUrl: './vehicle-list.html',
  styleUrl: './vehicle-list.scss',
})
export class VehicleList implements OnInit {

  private vehicleService = inject(VehicleService);
  vehicles = signal<Vehicle[]>([]);

  ngOnInit(): void {
    this.loadVehicles();
  }

  loadVehicles(): void {

    this.vehicleService.getVehicles()
      .subscribe({
        next: (res) => {
          this.vehicles.set(res.data);
        },
        error: (err) => {
          alert(err.error?.message ?? 'Failed.');
        }
      });

  }

  delete(id: string): void {

    if (!confirm('Delete this vehicle?')) {
      return;
    }

    this.vehicleService
      .deleteVehicle(id)
      .subscribe({
        next: () => {
          this.loadVehicles();
        },
        error: (err) => {
          alert(err.error?.message ?? 'Failed.');
        }
      });

  }

  markAsMaintenance(id: string): void {

    if (!confirm('Mark as Maintenance?')) {
      return;
    }

    this.vehicleService
      .markasmaintenance(id)
      .subscribe({
        next: () => {
          this.loadVehicles();
        },
        error: (err) => {
          alert(err.error?.message ?? 'Failed.');
        }
      });

  }

  markAsAvailable(id: string): void {

    if (!confirm('Mark as Available?')) {
      return;
    }

    this.vehicleService
      .markasavailable(id)
      .subscribe({
        next: () => {
          this.loadVehicles();
        },
        error: (err) => {
          alert(err.error?.message ?? 'Failed.');
        }
      });

  }

}