import { SaveVehicle } from './../app/models/vehicle';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {

  constructor(private http: HttpClient) {

   }

  getMakes() {
    return this.http.get('/api/makes');
  }
 
  getFeatures() {
    return this.http.get('/api/features');
  }

  create(vehicle: SaveVehicle) { 
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Accept', 'application/json');

    vehicle.isRegistered = JSON.parse(vehicle.isRegistered.toString());
    vehicle.modelId = JSON.parse(vehicle.modelId.toString());
    vehicle.makeId = JSON.parse(vehicle.makeId.toString())
    
    return this.http.post('/api/vehicles', vehicle, { headers : headers });
  }

  update(vehicle: SaveVehicle) { 
    const headers = new HttpHeaders()
    .set('Content-Type', 'application/json')
    .set('Accept', 'application/json');
    vehicle.isRegistered = JSON.parse(vehicle.isRegistered.toString());
    vehicle.modelId = JSON.parse(vehicle.modelId.toString());
    vehicle.makeId = JSON.parse(vehicle.makeId.toString())
    return this.http.put('/api/vehicles/' + vehicle.id, vehicle,  { headers : headers });
  }

  getVehicle(id) { 
    return this.http.get('/api/vehicles/' + id);
  }

  delete(id) { 
    return this.http.delete('/api/vehicles/' + id);
  }
} 
