import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { Reservation } from '../../models/reservation.model';
import { ReservationService } from '../../services/reservation.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.css'],
  standalone: true,
  imports: [CommonModule]
})
export class ReservationsComponent {
  reservations$: Observable<Reservation[]>;
  selectedReservation: Reservation | null = null;

  constructor(private reservationService: ReservationService) {
    this.reservations$ = this.reservationService.getReservations();
  }

  viewReservation(reservation: Reservation): void {
    this.selectedReservation = reservation;
  }

  closeDetails(): void {
    this.selectedReservation = null;
  }

  deleteReservation(id: string): void {
    if (confirm('Czy na pewno chcesz usunąć rezerwację?')) {
      this.reservationService.deleteReservation(id).subscribe();
    }
  }
}
