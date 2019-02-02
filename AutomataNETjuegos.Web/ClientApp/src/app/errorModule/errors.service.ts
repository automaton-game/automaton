import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { ApiErrors } from "./apiErrors";

@Injectable()
export class ErrorsService {
  private messageSource = new BehaviorSubject<Error | ApiErrors>(null);
  public currentMessage = this.messageSource.asObservable();

  setError(error: Error | ApiErrors) {
    this.messageSource.next(error);
  }
}
