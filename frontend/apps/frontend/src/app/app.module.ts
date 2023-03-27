import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { CalendarComponent } from './calendar/calendar.component';

@NgModule({
  declarations: [
    AppComponent,
    CalendarComponent
  ],
  imports: [
    BrowserModule
  ],
  // use environment.backend as reference to the backend URL
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
