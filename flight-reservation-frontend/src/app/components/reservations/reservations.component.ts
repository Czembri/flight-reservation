import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Reservation } from '../../models/reservation.model';
import { map, Observable, switchMap, tap } from 'rxjs';
import { ReservationService } from '../../services/reservation.service';
import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule,ReactiveFormsModule]
})
export class ReservationsComponent {
  reservations$ = this.reservationService.getReservations().pipe(tap(() => this.updatePagination()));
  selectedReservation: Reservation | null = null;
  editingReservation: Reservation | null = null;
  editForm: FormGroup;
  paginatedReservations: Reservation[] = [];
  currentPage: number = 1;
  itemsPerPage: number = 5;
  Math = Math;

  constructor(private reservationService: ReservationService, private fb: FormBuilder) {
    this.editForm = this.fb.group({
      id: [''],
      fname: ['', [Validators.required, Validators.minLength(3)]],
      lname: ['', [Validators.required, Validators.minLength(3)]],
      flightNumber: [''],
      departureTime: [''],
      arrivalTime: [''],
      class: ['']
    });
  }

  sortColumn: string = '';
sortDirection: 'asc' | 'desc' = 'asc';

sort(column: string): Observable<Reservation[]> {
  if (this.sortColumn === column) {
    this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
  } else {
    this.sortColumn = column;
    this.sortDirection = 'asc';
  }

  this.updatePagination();

  return this.reservations$.pipe(tap(reservations => {
    reservations.sort((a, b) => {
      const aValue = (a as any)[column];
      const bValue = (b as any)[column];

      if (aValue < bValue) return this.sortDirection === 'asc' ? -1 : 1;
      if (aValue > bValue) return this.sortDirection === 'asc' ? 1 : -1;
      return 0;
    });
  }), switchMap(() => this.updatePagination()));
}


  viewReservation(reservation: Reservation): void {
    this.selectedReservation = reservation;
  }

  closeDetails(): void {
    this.selectedReservation = null;
  }

  editReservation(reservation: Reservation): void {
    this.editForm.reset();
    this.editingReservation = reservation;
    
    this.editForm.patchValue(reservation);
  }

  cancelEdit(): void {
    this.editingReservation = null;
  }

  updateReservation(): void {
    if (this.editForm.valid) {
      const updatedReservation = this.editForm.value;
      updatedReservation.passengerName = updatedReservation.fname + ' ' + updatedReservation.lname;
      this.reservationService.updateReservation(updatedReservation.id, updatedReservation).subscribe(() => {
        this.editingReservation = null;
        alert('Rezerwacja zaktualizowana!');
      });
    }
  }

  deleteReservation(id: string): void {
    if (confirm('Czy na pewno chcesz usunąć rezerwację?')) {
      this.reservationService.deleteReservation(id).subscribe();
    }
  }

  updatePagination(): Observable<Reservation[]> {
    const start = (this.currentPage - 1) * this.itemsPerPage;
    const end = start + this.itemsPerPage;
    return this.reservations$.pipe(map(reservations => reservations.slice(start, end)));
  }

  goToPage(page: number): void {
    this.currentPage = page;
    this.updatePagination();
  }
}
