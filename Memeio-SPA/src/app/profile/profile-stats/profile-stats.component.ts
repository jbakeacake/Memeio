import { Component, OnInit } from '@angular/core';
import { multi } from './data';

@Component({
  selector: 'app-profile-stats',
  templateUrl: './profile-stats.component.html',
  styleUrls: ['./profile-stats.component.css'],
})
export class ProfileStatsComponent implements OnInit {
  multi: any[];
  baseWidth: number = 500;
  view: any[] = [this.baseWidth, 300];

  // options
  legend: boolean = true;
  showLabels: boolean = true;
  animations: boolean = true;
  xAxis: boolean = true;
  yAxis: boolean = true;
  showYAxisLabel: boolean = true;
  showXAxisLabel: boolean = true;
  xAxisLabel: string = 'Month';
  yAxisLabel: string = 'Like:Dislike';
  timeline: boolean = true;

  colorScheme = {
    domain: ['#5AA454', '#E44D25', '#CFC0BB', '#7aa3e5', '#a8385d', '#aae3f5'],
  };

  constructor() {
    Object.assign(this, { multi });
  }
  ngOnInit(): void {}

  onSelect(data): void {
    console.log('Item clicked', JSON.parse(JSON.stringify(data)));
  }

  onActivate(data): void {
    console.log('Activate', JSON.parse(JSON.stringify(data)));
  }

  onDeactivate(data): void {
    console.log('Deactivate', JSON.parse(JSON.stringify(data)));
  }

  onResize(event: any): void {
    console.log(innerWidth);
    if (innerWidth > this.baseWidth) {
      this.view = [innerWidth / (innerWidth / this.baseWidth), 300];
    } else {
      this.view = [innerWidth / (this.baseWidth / innerWidth), 300];
    }
  }
}
