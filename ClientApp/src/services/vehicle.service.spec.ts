import { TestBed } from '@angular/core/testing';

import { VehicleService } from './vehicle.service';

describe('MakeService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: VehicleService = TestBed.get(VehicleService);
    expect(service).toBeTruthy();
  });
});
