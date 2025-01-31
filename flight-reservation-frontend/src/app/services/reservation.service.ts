import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, BehaviorSubject, catchError, throwError } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Reservation } from '../models/reservation.model';

@Injectable({
  providedIn: 'root',
})
export class ReservationService {
  private apiUrl = 'https://localhost:7222/reservations';
  
  private reservationsSubject = new BehaviorSubject<Reservation[]>([]);
  reservations$ = this.reservationsSubject.asObservable();

  constructor(private http: HttpClient) {
    this.loadReservations();
  }

  private loadReservations(): void {
    this.http.get<Reservation[]>(this.apiUrl).pipe(
      catchError(this.handleError)
    ).subscribe(reservations => this.reservationsSubject.next(reservations));
  }

  getReservations(): Observable<Reservation[]> {
    return this.reservations$;
  }

  addReservation(reservation: Reservation): Observable<Reservation> {
    return this.http.post<Reservation>(this.apiUrl, reservation).pipe(
      tap(() => this.loadReservations()),
      catchError(this.handleError)
    );
  }

  updateReservation(id: string, reservation: Reservation): Observable<Reservation> {
    return this.http.put<Reservation>(`${this.apiUrl}/${id}`, reservation).pipe(
      tap(() => this.loadReservations()),
      catchError(this.handleError)
    );
  }

  deleteReservation(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.loadReservations()),
      catchError(this.handleError)
    );
  }

  private handleError(e: HttpErrorResponse) {
    return throwError(() => new Error(`Error: ${e.error.message}`));
  }
}
