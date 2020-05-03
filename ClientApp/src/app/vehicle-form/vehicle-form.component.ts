import { VehicleService } from './../../services/vehicle.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit { 
  makes : any;
  features : any;
  models : any;
  vehicle : any = {};
  constructor(private vehicleService : VehicleService) { 

  }

  ngOnInit() {
    this.vehicleService.getMakes().subscribe(makes => this.makes = makes);

    this.vehicleService.getFeatures().subscribe(features => this.features = features);
  }

  onMakeChange() {
    var selectedMake = this.makes.find(x => x.id == this.vehicle.make);
    this.models = selectedMake ? selectedMake.models : [];
    //console.log("VEHICLE", this.vehicle);
    //console.log("Selected Make", selectedMake);
  }

}
