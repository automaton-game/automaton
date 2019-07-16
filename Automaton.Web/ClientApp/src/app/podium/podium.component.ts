import { Component, Input, OnInit } from '@angular/core';
import { PodiumModel } from './podium.model';

declare let $: any;

@Component({
  selector: 'podium-component',
  templateUrl: './podium.component.html',
  styleUrls: ['./podium.component.css']
})
export class PodiumComponent implements OnInit {
  
  @Input() model: PodiumModel;

  ngOnInit(): void {
    $(document).ready(function () {
      function podiumAnimate() {
        $('.bronze .podium').animate({
          "height": "62px"
        }, 1500);
        $('.gold .podium').animate({
          "height": "165px"
        }, 1500);
        $('.silver .podium').animate({
          "height": "106px"
        }, 1500);
        $('.competition-container .name').delay(1000).animate({
          "opacity": "1"
        }, 500);
      }
      podiumAnimate();
    });
  }
}
