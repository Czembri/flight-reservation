export interface Reservation extends ReservationBase {
  class: number;
}

export interface ReservationBase {
  id: string;
  lname: string;
  fname: string;
  flightNumber: string;
  departureTime: string;
  arrivalTime: string;
}

export interface ReservationFormModel extends ReservationBase {
  class: string;
}

export const classes = new Map<number, string>(
  [[1, 'First'], [2, 'Business'], [3, 'Economy']]
);