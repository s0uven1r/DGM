import { HttpClient } from '@angular/common/http';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UserComponent implements OnInit, OnDestroy {
  dtOptions: DataTables.Settings = {};
  persons: Person[] = [];
  dtTrigger: Subject<any> = new Subject<any>();
  constructor(private httpClient: HttpClient, private changeDetectorRef: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10
    };

    this.httpClient.get<Person[]>('assets/user.json')
    .subscribe(data => {
      this.persons = data;
      // Calling the DT trigger to manually render the table
      this.dtTrigger.next();
      this.changeDetectorRef.markForCheck();
    });
  }
  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }

}
export interface Person{
  id: string;
  title:string;
}
