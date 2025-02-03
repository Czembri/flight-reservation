export interface Reservation {
  id: string;
  passengerName: string;
  flightNumber: string;
  departureTime: string;
  arrivalTime: string;
  class: number;
}

export const classes = new Map<number, string>(
  [[1, 'First'], [2, 'Business'], [3, 'Economy']]
);