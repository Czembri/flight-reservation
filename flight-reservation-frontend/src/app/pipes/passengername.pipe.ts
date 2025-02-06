import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'passengername',
  standalone: true
})
export class PassengernamePipe implements PipeTransform {

  transform(fname: string, lname: string): string {
    return `${fname} ${lname}`;
  }

}
