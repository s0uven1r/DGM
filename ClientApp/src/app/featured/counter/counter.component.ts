import { Component, OnInit } from '@angular/core';
import { UserModel } from 'src/app/infrastructure/model/UserManagement/user-model';
import { CounterService } from './service/counter.service';

@Component({
  selector: 'app-counter',
  templateUrl: './counter.component.html',
  styleUrls: ['./counter.component.css']
})
export class CounterComponent implements OnInit {

  userDetail: UserModel [] = [];
  constructor(private counter: CounterService) { }

  ngOnInit(): void {
   this.counter.validateUserData().subscribe((res: UserModel[]) => {this.userDetail = res;});
  }
  public currentCount = 0;

  public incrementCounter() {
    this.currentCount++;
  }
}
