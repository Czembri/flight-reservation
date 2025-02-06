import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { ReservationService } from '../../services/reservation.service';
import { Reservation } from '../../models/reservation.model';
import { CommonModule } from '@angular/common';
import { parse } from 'node:path';

@Component({
  selector: 'app-reservation-form',
  templateUrl: './reservation-form.component.html',
  styleUrls: ['./reservation-form.component.css'],
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule]
})
export class ReservationFormComponent {
  reservationForm: FormGroup;
  errorMessage: string | null = null;

  constructor(private fb: FormBuilder, private reservationService: ReservationService) {
    this.reservationForm = this.fb.group({
      fname: ['', [Validators.required, Validators.minLength(3)]],
      lname: ['', [Validators.required, Validators.minLength(3)]],
      flightNumber: ['', [Validators.required, Validators.pattern('^[A-Z0-9]+$')]],
      departureTime: ['', Validators.required],
      arrivalTime: ['', Validators.required, { validators: this.validateDates }],
      class: [null, Validators.required],
    });
  }

  private validateDates(group: AbstractControl): ValidationErrors | null {
    const departure = group.get('departureTime')?.value;
    const arrival = group.get('arrivalTime')?.value;
  
    if (departure && arrival && new Date(departure) >= new Date(arrival)) {
      return { invalidDates: true };
    }
    return null;
  }

  submit(): void {
    this.errorMessage = null;

    if (this.reservationForm.invalid) {
      this.errorMessage = "Formularz zawiera błędy. Proszę je poprawić.";
      return;
    }

    const reservation: Reservation = { ...this.reservationForm.value };
    reservation.class = parseInt(reservation.class as any, 0);
    this.reservationService.addReservation(reservation).subscribe({
      next: () => {
        alert('Rezerwacja dodana!');
        this.reservationForm.reset();
      },
      error: (error) => {
        this.errorMessage = 'Wystąpił błąd podczas dodawania rezerwacji: ' + error;
        console.error(error);
      }
    });
  }

  get fname() {
    return this.reservationForm.get('fname');
  }

  get lname() {
    return this.reservationForm.get('lname');
  }

  get flightNumber() {
    return this.reservationForm.get('flightNumber');
  }

  get departureTime() {
    return this.reservationForm.get('departureTime');
  }

  get arrivalTime() {
    return this.reservationForm.get('arrivalTime');
  }

  get classField() {
    return this.reservationForm.get('class');
  }
}
