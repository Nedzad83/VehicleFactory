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
  vehicles: Vehicle[];
  makes: KeyValuePair[];
  filter: any = { };
  
  constructor(private vehicleService: VehicleService) {
    
   }

  ngOnInit(): void {
    this.vehicleService.getMakes().subscribe((makes: KeyValuePair[]) => this.makes =  makes);
    this.populateVehicles();
  }

  private populateVehicles() {
    this.vehicleService.getVehicles(this.filter).subscribe((vehicles: Vehicle[]) => this.vehicles = vehicles);
  }

  onFilterChange() { 
    this.populateVehicles();
  }

  resetFilter() { 
    this.filter = {};
    this.onFilterChange();
  }

}
