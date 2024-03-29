import { Component, OnInit } from '@angular/core';

interface WeekSpan {
  start: Date;
  end: Date;
}

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.scss'],
})
export class CalendarComponent implements OnInit {
  constructor() {}
  ngOnInit(): void {
    console.log('as');
  }
  weekdays = [
    'Monday',
    'Tuesday',
    'Wednesday',
    'Thursday',
    'Friday',
    'Saturday',
    'Sunday',
  ];
  hours = [
    '7:50',
    '8:00',
    '10:00',
    '11:00',
    '12:00',
    '13:00',
    '14:00',
    '15:00',
    '16:00',
    '17:00',
  ];
  currentWeek: WeekSpan = {
    start: getMonday(new Date()),
    end: getLastDayOfWeek(new Date()),
  };

  previousWeek() {
    this.currentWeek.start.setDate(this.currentWeek.start.getDate() - 7);
    this.currentWeek.end.setDate(this.currentWeek.end.getDate() - 7);
  }

  nextWeek() {
    this.currentWeek.start.setDate(this.currentWeek.start.getDate() + 7);
    this.currentWeek.end.setDate(this.currentWeek.end.getDate() + 7);
  }

  blockClicked(weekdayId: string, hourId: string) {
    console.log(weekdayId, hourId);
  }
}
function getMonday(d: Date) {
  d = new Date(d);
  var day = d.getDay(),
    diff = d.getDate() - day + (day == 0 ? -6 : 1); // adjust when day is sunday
  return new Date(d.setDate(diff));
}

function getLastDayOfWeek(d: Date) {
  d = new Date(d);
  var day = d.getDay(),
    diff = d.getDate() + (6 - day); // add the difference between the current day and Saturday
  return new Date(d.setDate(diff));
}
