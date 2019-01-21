// errors-handler.ts
import { ErrorHandler, Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { ErrorsService } from './errors.service';

@Injectable()
export class ErrorsHandler extends ErrorHandler {

  

  constructor(private errorService: ErrorsService) {
    super();
  }

  handleError(error: Error) {
    // Do whatever you like with the error (send it to the server?)
    // And log it to the console
    try {
      this.errorService.setError(error);
    } catch (e) {
      console.error(e);
      console.error(error);
    }
  }
}
