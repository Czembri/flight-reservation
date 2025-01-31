import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ReservationFormComponent } from './components/reservation-form/reservation-form.component';
import { ReservationsComponent } from './components/reservations/reservations.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [ReservationFormComponent, ReservationsComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'flight-reservation-frontend';
}
