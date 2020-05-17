import * as _ from 'underscore';
import { Vehicle, SaveVehicle } from './../models/vehicle';
import { VehicleService } from './../../services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { forkJoin } from 'rxjs';
import { map, timeout } from 'rxjs/operators';


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

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private vehicleService: VehicleService,
    private toastyService: ToastrService) { 
    route.params.subscribe(p => { 
      this.vehicle.id = +p['id'];
    });
  }

  ngOnInit() {
    this.vehicleService.getMakes().subscribe((makes: any[]) => this.makes =  makes);
    this.vehicleService.getFeatures().subscribe((features:any[]) => this.features = features);

    forkJoin([
      //this.vehicleService.getMakes(),
      //this.vehicleService.getFeatures(),
      this.vehicle.id ? this.vehicleService.getVehicle(this.vehicle.id) : this.vehicle
    ])
      .subscribe(responseList => {
      //this.makes = responseList[0] as any[];
      //this.features = responseList[1] as any[];
        if (this.vehicle.id)
        {
          this.setVehicle(responseList[0] as Vehicle);
          this.populateModels();
        }
      }, err => { 
        if (err.status == 404)
             this.router.navigate(['']);
      });
  }

  private setVehicle(v: Vehicle) { 
    this.vehicle.id = v.id;
    this.vehicle.makeId = v.make.id;
    this.vehicle.modelId = v.model.id;
    this.vehicle.isRegistered = v.isRegistered;
    this.vehicle.contact = v.contact;
    this.vehicle.features = _.pluck(v.features, 'id');
  }

  onMakeChange() {
    this.populateModels();
    delete this.vehicle.modelId;
  }

  private populateModels() {
    var selectedMake = this.makes.find(x => x.id == this.vehicle.makeId);
    this.models = selectedMake ? selectedMake.models : [];
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
    if (this.vehicle.id) {
      this.vehicleService.update(this.vehicle)
        .subscribe(x => {
          this.toastyService.success("Success", "The vehicle is successfully updated !", { timeOut: 5000 });
        });
    }
    else { 
      this.vehicleService.create(this.vehicle)
      .subscribe(
        x => console.log(x)
      );
    }
  }

  delete() { 
    if (confirm("Are you sure ?")) { 
      this.vehicleService.delete(this.vehicle.id)
        .subscribe(x => { 
          this.router.navigate(['']);
          this.toastyService.success("Success", "The vehicle is successfully deleted !", { timeOut: 5000 });
        });
    }
  }

}
