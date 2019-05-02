import { Injectable, EventEmitter } from '@angular/core';

@Injectable()
export class SpinnerService {

  public visibility: EventEmitter<boolean>;

  constructor() {
    this.visibility = new EventEmitter();
  }
}
