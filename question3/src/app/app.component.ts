import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, ValidationErrors, Validators, ReactiveFormsModule, FormGroup } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';


@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
    standalone: true,
    imports: [MatToolbarModule,
       MatIconModule,
        MatCardModule,
         ReactiveFormsModule,
          MatFormFieldModule,
           MatInputModule]
})
export class AppComponent {
 formGroup: FormGroup;
  constructor(private formBuilder: FormBuilder) {
        this.formGroup = this.formBuilder.group(
      {
        nom: ['', [Validators.required]],
        roadNumber : ['', [Validators.required],
                          Validators.min(1000), // Minimum allowed value is 10
                         Validators.max(9999) ],
        postalCode: ['', [ Validators.pattern('"^[A-Z][0-9][A-Z][ ]?[0-9][A-Z][0-9]$')]],
        comment: ['', [Validators.required]]
      },
    );
  }
}


