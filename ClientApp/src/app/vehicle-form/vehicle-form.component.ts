import { ToastrModule } from 'ngx-toastr';
import { VehicleService } from './../../services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import { SaveVehicle } from '../models/vehicle';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit { 
  makes: any[]; 
  models: any[];
  features: any[];
  vehicle: SaveVehicle = {
    id: 0,
    makeId: 0,
    modelId: 0,
    isRegistered: false,
    features: [],
    contact: {
      name: '',
      email: '',
      phone: '',
    }
  };

  constructor(private vehicleService: VehicleService,
              private toastyService: ToastrService) { 

  }

  ngOnInit() {
    this.vehicleService.getMakes().subscribe((makes: any[]) => this.makes =  makes);

    this.vehicleService.getFeatures().subscribe((features:any[]) => this.features = features);
  }

  onMakeChange() {
    var selectedMake = this.makes.find(x => x.id == this.vehicle.makeId);
    this.models = selectedMake ? selectedMake.models : [];
    delete this.vehicle.modelId;
    //console.log("VEHICLE", this.vehicle);
    //console.log("Selected Make", selectedMake);
  }

  onFeatureToggle(featureId, $event)
  {
    if ($event.target.checked) {
      this.vehicle.features.push(featureId);
    }
    else { 
      var index = this.vehicle.features.indexOf(featureId);
      this.vehicle.features.splice(index, 1);
    }
  }

  submit() { 
    this.vehicleService.create(this.vehicle)
      .subscribe(
        x => console.log(x),
        err => { 
          this.toastyService.error('An unexpected error happened.', 'Error', {
            timeOut: 3000
          });
        }
      );
  }

}
