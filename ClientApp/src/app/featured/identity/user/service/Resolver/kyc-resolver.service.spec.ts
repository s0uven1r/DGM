/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { KycResolverService } from './kyc-resolver.service';

describe('Service: RoleResolver', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [KycResolverService]
    });
  });

  it('should ...', inject([KycResolverService], (service: KycResolverService) => {
    expect(service).toBeTruthy();
  }));
});
