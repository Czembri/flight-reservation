import { AbstractControl, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { Reservation, ReservationFormModel } from '../../models/reservation.model';
import { catchError, finalize, map, Observable, Subject, switchMap, takeUntil, tap } from 'rxjs';
import { ReservationService } from '../../services/reservation.service';
import { CommonModule } from '@angular/common';
import { Component, OnDestroy } from '@angular/core';
import { PassengernamePipe } from '../../pipes/passengername.pipe';
import { FlightClassPipe } from '../../pipes/flight-class.pipe';

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule,ReactiveFormsModule, PassengernamePipe, FlightClassPipe]
})
export class ReservationsComponent implements OnDestroy {
  reservations$ = this.reservationService.getReservations().pipe(map((res) => this.updatePagination(res)));
  selectedReservation: Reservation | null = null;
  editingReservation: ReservationFormModel | null = null;
  editForm: FormGroup;
  paginatedReservations: Reservation[] = [];
  currentPage: number = 1;
  itemsPerPage: number = 3;
  Math = Math;
  totalPages: number = 1;
  
  private destroy$ = new Subject<void>();

  constructor(private reservationService: ReservationService, private fb: FormBuilder) {
    this.editForm = this.fb.group({
      id: [''],
      fname: ['', [Validators.required, Validators.minLength(3)]],
      lname: ['', [Validators.required, Validators.minLength(3)]],
      flightNumber: ['', [Validators.required, Validators.pattern('^[A-Z0-9]+$')]],
      departureTime: ['', Validators.required],
      arrivalTime: ['', Validators.required],
      class: ['', Validators.required],
    });
  }
  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
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

  return this.reservations$.pipe(tap(reservations => {
    reservations.sort((a, b) => {
      const aValue = (a as any)[column];
      const bValue = (b as any)[column];

      if (aValue < bValue) return this.sortDirection === 'asc' ? -1 : 1;
      if (aValue > bValue) return this.sortDirection === 'asc' ? 1 : -1;
      return 0;
    });
  }), map(res => this.updatePagination(res)));
}


  viewReservation(reservation: Reservation): void {
    this.selectedReservation = reservation;
  }

  closeDetails(): void {
    this.selectedReservation = null;
  }

  editReservation(reservation: Reservation, lname: string): void {
    this.editForm.reset();
    this.editingReservation = {
      ...reservation,
      lname: lname,
      class: reservation.class.toString() 
    };
    this.editForm.patchValue(this.editingReservation);

    console.warn(this.editingReservation);

    this.editForm.markAsPristine();
    this.editForm.updateValueAndValidity();
  }

  cancelEdit(): void {
    this.editingReservation = null;
  }

  updateReservation(): void {
    const updatedReservation = this.editForm.value;
    updatedReservation.class = parseInt(updatedReservation.class, 0)
    updatedReservation.arrivalTime = new Date(updatedReservation.arrivalTime).toISOString();
    updatedReservation.departureTime = new Date(updatedReservation.departureTime).toISOString();
    this.reservationService.updateReservation(updatedReservation.id, updatedReservation)
    .pipe(switchMap(() => this.reservationService.getReservations()), catchError(err => {
      console.error(err);
      return [];
    }), finalize(() => alert('Rezerwacja zaktualizowana!')))
    .subscribe(() => {
      this.editingReservation = null;
    });
  }

  deleteReservation(id: string): void {
    if (confirm('Czy na pewno chcesz usunąć rezerwację?')) {
      this.reservationService.deleteReservation(id).subscribe();
    }
  }

  updatePagination(res: Reservation[]): Reservation[] {
    this.totalPages = Math.max(1, Math.ceil(res.length / this.itemsPerPage));
    const start = (this.currentPage - 1) * this.itemsPerPage;
    const end = start + this.itemsPerPage;
    return res.slice(start, end);
  }
  
  

  goToPage(page: number): void {
    if (page < 1 || page > this.totalPages) return;
    this.currentPage = page;
    this.reservations$.pipe(takeUntil(this.destroy$), map(res => this.updatePagination(res))).subscribe();
  }
  

  get departureTime() {
    return this.editForm.get('departureTime');
  }

  get arrivalTime() {
    return this.editForm.get('arrivalTime');
  }
}
