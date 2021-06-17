/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { RoleResolverService } from './role-resolver.service';

describe('Service: RoleResolver', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RoleResolverService]
    });
  });

  it('should ...', inject([RoleResolverService], (service: RoleResolverService) => {
    expect(service).toBeTruthy();
  }));
});
