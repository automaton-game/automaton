import { Component, OnInit } from '@angular/core';
import { ApiErrors } from './apiErrors';
import { ApiError } from './apiError';
import { ErrorsService } from './errors.service';

@Component({
  selector: 'app-error-component',
  templateUrl: './error.component.html',
})
export class ErrorComponent implements OnInit {

  public errores: string[] = null;

  constructor(private errorService: ErrorsService) {
  }

  ngOnInit(): void {
    this.errorService.currentMessage.subscribe((error: Error) => {
      if (!error) {
        this.errores = null;
        return;
      }

      if (error instanceof ApiErrors) {
        this.errores = error.ApiError.map(m => m.message);
      } else if (error instanceof ApiError) {
        this.errores = [error.message];
      } else {
        this.errores = [error.message];
      }
    });
  }
}
