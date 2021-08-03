import { formatDate } from '@angular/common';
import { Directive, ElementRef, HostListener, Input } from '@angular/core';
import NepaliDate from 'nepali-date-converter';

@Directive({
  selector: '[appNgNepaliDate]'
})
export class NgNepaliDateDirective  {
  @Input() appNgNepaliDate = '';
  constructor(el: ElementRef) {
   }

   @HostListener('focusout', ["$event.target"])
   onKeyup(val: any) {
    this.DateConverter(val.value);
   }
   private  DateConverter(val: any){
    var dateValue = val;
    if(dateValue){
      var date = dateValue.split("/", 3);
      var y: number = +date[2];
      var m: number = +date[1] ;
      var d: number = +date[0];
      var actualDate = new NepaliDate(y,m,d) ;
      var npDate = actualDate.format('DD/MM/YYYY', 'np').toString();
      val = npDate;
      if(this.appNgNepaliDate){
        require('nepali-date-converter');
        var dateAd = formatDate(actualDate.toJsDate(),"dd/MM/yyyy","en-US");
        (<HTMLInputElement>document.getElementById(this.appNgNepaliDate)).value = dateAd;
      }
    }

   }
}
