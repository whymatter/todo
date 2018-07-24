import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';
import {TodoList} from '../../todoApiClient';

@Component({
  selector: 'app-new-todo-list-dialog',
  templateUrl: './new-todo-list-dialog.component.html',
  styleUrls: ['./new-todo-list-dialog.component.css']
})
export class NewTodoListDialogComponent {

  test = 'Hi';

  constructor(
    public dialogRef: MatDialogRef<NewTodoListDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public todoList: TodoList) {
  }

  cancel(): void {
    this.dialogRef.close();
  }

  create(): void {
    this.dialogRef.close(this.todoList);
  }

}
