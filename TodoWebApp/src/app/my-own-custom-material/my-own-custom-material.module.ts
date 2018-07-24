import {
  MatButtonModule,
  MatCheckboxModule,
  MatChipsModule,
  MatDialog,
  MatDialogModule, MatFormFieldModule, MatInputModule,
  MatToolbar,
  MatToolbarModule
} from '@angular/material';
import {NgModule} from '@angular/core';

@NgModule({
  imports: [MatButtonModule, MatCheckboxModule, MatToolbarModule, MatChipsModule, MatDialogModule, MatFormFieldModule, MatInputModule],
  exports: [MatButtonModule, MatCheckboxModule, MatToolbarModule, MatChipsModule, MatDialogModule, MatFormFieldModule, MatInputModule],
})
export class MyOwnCustomMaterialModule {
}
