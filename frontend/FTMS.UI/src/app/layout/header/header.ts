import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  imports: [],
  templateUrl: './header.html',
  styleUrl: './header.scss',
})
export class Header {

  username = '';
  role = '';

  constructor(
    private router: Router
  ) {

    this.username = localStorage.getItem('username') ?? '';
    this.role = localStorage.getItem('role') ?? '';

  }


  logout(): void {

    localStorage.removeItem('bearerToken');
    localStorage.removeItem('username');
    localStorage.removeItem('role');

    this.router.navigate(['/login']);

  }

}