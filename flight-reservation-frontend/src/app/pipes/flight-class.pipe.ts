import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'flightClassPipe',
  standalone: true
})
export class FlightClassPipe implements PipeTransform {

  transform(fclass: number): string {
    switch (fclass) {
      case 0:
        return 'Ekonomiczna';
      case 1:
        return 'Biznes';
      case 2:
        return 'Pierwsza klasa';
      default:
        return 'Unknown';
    }
  }

}
