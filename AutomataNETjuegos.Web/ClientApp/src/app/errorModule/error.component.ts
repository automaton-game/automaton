import { Component, OnInit } from '@angular/core';
import { ApiErrors } from './apiErrors';
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
    this.errorService.currentMessage.subscribe((error: ApiErrors | Error ) => {
      if (!error) {
        this.errores = null;
        return;
      } 

      if (this.instanceOfApiErrors(error) && error.errores) {
          this.errores = error.errores;
      } else {
        this.errores = [error.message];
      }
    });
  }

  private instanceOfApiErrors(object: any): object is ApiErrors {
    return 'errores' in object;
  }
}
