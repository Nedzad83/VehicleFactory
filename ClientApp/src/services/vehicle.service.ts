import { SaveVehicle } from './../app/models/vehicle';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {

  private readonly vehiclesEndpoint = "/api/vehicles";
  constructor(private http: HttpClient) {

   }

  getMakes() {
    return this.http.get('/api/makes');
  }
 
  getFeatures() {
    return this.http.get('/api/features');
  }

  getVehicles(filter) { 
    return this.http.get(this.vehiclesEndpoint + '?' + this.toQueryString(filter));
  }

  toQueryString(obj) { 
    var parts = [];
    for (var prop in obj) { 
      var value = obj[prop];
      if (value != null && value != undefined)
        parts.push(encodeURIComponent(prop) + '=' + encodeURIComponent(value));  // property equals value..
    }
    return parts.join('&');
  }

  create(vehicle: SaveVehicle) { 
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Accept', 'application/json');

    vehicle.isRegistered = JSON.parse(vehicle.isRegistered.toString());
    vehicle.modelId = JSON.parse(vehicle.modelId.toString());
    vehicle.makeId = JSON.parse(vehicle.makeId.toString())
    
    return this.http.post(this.vehiclesEndpoint, vehicle, { headers : headers });
  }

  update(vehicle: SaveVehicle) { 
    const headers = new HttpHeaders()
    .set('Content-Type', 'application/json')
    .set('Accept', 'application/json');
    vehicle.isRegistered = JSON.parse(vehicle.isRegistered.toString());
    vehicle.modelId = JSON.parse(vehicle.modelId.toString());
    vehicle.makeId = JSON.parse(vehicle.makeId.toString())
    return this.http.put(this.vehiclesEndpoint + '/' + vehicle.id, vehicle,  { headers : headers });
  }

  getVehicle(id) { 
    return this.http.get(this.vehiclesEndpoint + '/'+ id);
  }

  delete(id) { 
    return this.http.delete(this.vehiclesEndpoint + '/'+ id);
  }
} 
