import { ErrorsHandler } from './errors.handler';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { ErrorComponent } from './error.component';
import { ErrorsService } from './errors.service';

@NgModule({
  declarations: [
    ErrorComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [
    ErrorsService,
    {
      provide: ErrorHandler,
      useClass: ErrorsHandler,
    },
  ],
  exports: [ErrorComponent]
})
export class ErrorModule { }
