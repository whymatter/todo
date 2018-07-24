import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';


import {AppComponent} from './app.component';
import {ApiModule, Configuration} from '../todoApiClient';
import {GrantService} from './grant.service';
import {ConfigurationFactory} from './configuration-factory';
import {MyOwnCustomMaterialModule} from './my-own-custom-material/my-own-custom-material.module';
import {NewTodoListDialogComponent} from './new-todo-list-dialog/new-todo-list-dialog.component';
import {FormsModule} from '@angular/forms';


@NgModule({
  declarations: [
    AppComponent,
    NewTodoListDialogComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    BrowserAnimationsModule,
    MyOwnCustomMaterialModule,
    ApiModule
  ],
  providers: [
    GrantService,
    {provide: Configuration, useFactory: ConfigurationFactory, deps: [GrantService]}
  ],
  bootstrap: [AppComponent],
  entryComponents: [NewTodoListDialogComponent]
})
export class AppModule {
}
