import {Component, OnInit} from '@angular/core';
import {TodoList, TodoListService} from '../todoApiClient';
import {Observable} from 'rxjs/index';
import {concatAll} from 'rxjs/operators';
import {NewTodoListDialogComponent} from './new-todo-list-dialog/new-todo-list-dialog.component';
import {MatDialog} from '@angular/material';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'app';

  todoLists: Observable<Array<TodoList>>;

  constructor(private todoListService: TodoListService, private dialog: MatDialog) {

  }

  ngOnInit(): void {
    this.todoLists = this.todoListService.apiTodoListGet();
  }

  addNewTodoList(): void {
    console.log('open dialog');

    const dialogRef = this.dialog.open(NewTodoListDialogComponent, {
      width: '500px',
      data: {name: null}
    });

    dialogRef.afterClosed().subscribe(result => {
      this.todoListService.apiTodoListPost({name: result});
    });
  }

}
