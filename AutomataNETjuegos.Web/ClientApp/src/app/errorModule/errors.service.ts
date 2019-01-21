import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";

@Injectable()
export class ErrorsService {
  private messageSource = new BehaviorSubject<Error>(null);
  public currentMessage = this.messageSource.asObservable();

  setError(error: Error) {
    this.messageSource.next(error);
  }
}
