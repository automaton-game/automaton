import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';

@Component({
  selector: 'spinnerModal',
  templateUrl: 'spinnerModal.component.html',
  styleUrls: ['spinnerModal.component.css']
})
export class SpinnerModalComponent implements OnInit {
  public visibility: boolean = false;

  constructor(public bsModalRef: BsModalRef) { }

  ngOnInit() {
  }
}
