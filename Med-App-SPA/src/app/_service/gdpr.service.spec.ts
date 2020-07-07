import { TestBed } from '@angular/core/testing';

import { GdprServiceService } from './gdpr-service.service';

describe('GdprServiceService', () => {
  let service: GdprServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GdprServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
