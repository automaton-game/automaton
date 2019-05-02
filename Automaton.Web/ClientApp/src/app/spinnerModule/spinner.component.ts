import { Component } from '@angular/core';
import { SpinnerService } from './spinner.service';

@Component({
  selector: 'spinner',
  templateUrl: 'spinner.component.html',
  styleUrls: ['spinner.component.css']
})
export class SpinnerComponent {
  public visibility: boolean = false;

  constructor(private spinnerService: SpinnerService) { }

  ngOnInit() {
    this.spinnerService.visibility.subscribe(state => {
      this.visibility = state;
    });
  }
}
