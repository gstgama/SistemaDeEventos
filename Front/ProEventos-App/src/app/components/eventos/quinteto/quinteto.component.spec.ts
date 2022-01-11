/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { QuintetoComponent } from './quinteto.component';

describe('QuintetoComponent', () => {
  let component: QuintetoComponent;
  let fixture: ComponentFixture<QuintetoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QuintetoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuintetoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
