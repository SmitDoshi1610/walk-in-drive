import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReviewAndProceedComponent } from './review-and-proceed.component';

describe('ReviewAndProceedComponent', () => {
  let component: ReviewAndProceedComponent;
  let fixture: ComponentFixture<ReviewAndProceedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReviewAndProceedComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ReviewAndProceedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
