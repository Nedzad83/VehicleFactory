import { KeyValuePair } from './../models/vehicle';
import { VehicleService } from './../../services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import { Vehicle } from '../models/vehicle';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent implements OnInit {
  queryResult: any = {};
  makes: KeyValuePair[];
  query: any = {
    pageSize: 2
  };
  columns = [
    {title: 'Id'},
    {title: 'Make', key: 'make', isSortable:'true'},
    {title: 'Model', key: 'model', isSortable:'true'},
    {title: 'Contact Name', key: 'contactName', isSortable:'true'}
  ];
  
  constructor(private vehicleService: VehicleService) {
    
   }

  ngOnInit(): void {
    this.vehicleService.getMakes().subscribe((makes: KeyValuePair[]) => this.makes =  makes);
    this.populateVehicles();
  }

  private populateVehicles() {
    this.vehicleService.getVehicles(this.query).subscribe((result: any[]) => { 
      this.queryResult = result;
    });
  }

  onFilterChange() { 
    this.query.page = 1;
    this.query.pageSize = 3;
    this.populateVehicles();
  }

  resetFilter() { 
    this.query = {};
    this.onFilterChange();
  }

  sortBy(columnName) { 
    if (this.query.sortBy === columnName) {
      this.query.IsSortAscending = !this.query.IsSortAscending;
    }
    else { 
      this.query.sortBy = columnName;
      this.query.IsSortAscending = true;
    }
    this.populateVehicles();
  }

  onPageChange(page) { 
    this.query.page = page;
    this.populateVehicles();
  }

}
