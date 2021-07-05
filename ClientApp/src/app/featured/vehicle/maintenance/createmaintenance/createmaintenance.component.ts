import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-createmaintenance',
  templateUrl: './createmaintenance.component.html',
  styleUrls: ['./createmaintenance.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CreatemaintenanceComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
