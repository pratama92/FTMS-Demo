import { Routes } from '@angular/router';

import { Login } from './features/auth/login/login';
import { MainLayout } from './layout/main-layout/main-layout';
import { Dashboard } from './features/dashboard/dashboard';

import { PersonList } from './features/person/person-list/person-list';
import { PersonForm } from './features/person/person-form/person-form';

import { VehicleList } from './features/vehicle/vehicle-list/vehicle-list';
import { VehicleForm } from './features/vehicle/vehicle-form/vehicle-form';

import { BookingList } from './features/booking/booking-list/booking-list';
import { BookingForm } from './features/booking/booking-form/booking-form';
import { BookingDetail } from './features/booking/booking-detail/booking-detail';


export const routes: Routes = [

  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },


  {
    path: 'login',
    component: Login
  },


  {
    path: '',
    component: MainLayout,
    children: [

      {
        path: 'dashboard',
        component: Dashboard
      },

      {
        path: 'persons',
        component: PersonList
      },
      {
        path: 'persons/create',
        component: PersonForm
      },
      {
        path: 'persons/edit/:id',
        component: PersonForm
      },

      {
        path: 'vehicles',
        component: VehicleList
      },
      {
        path: 'vehicles/create',
        component: VehicleForm
      },
      {
        path: 'vehicles/edit/:id',
        component: VehicleForm
      },

      {
        path: 'bookings',
        component: BookingList
      },
      {
        path: 'bookings/create',
        component: BookingForm
      },
      {
        path: 'bookings/edit/:id',
        component: BookingForm
      },
      {
        path: 'bookings/:id',
        component: BookingDetail,
      }            

    ]
  },


  {
    path: '**',
    redirectTo: 'login'
  }

];