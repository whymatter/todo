import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewTodoListDialogComponent } from './new-todo-list-dialog.component';

describe('NewTodoListDialogComponent', () => {
  let component: NewTodoListDialogComponent;
  let fixture: ComponentFixture<NewTodoListDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewTodoListDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewTodoListDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
