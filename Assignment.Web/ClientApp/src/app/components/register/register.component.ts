import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountsClient, ProblemDetails, ResponseMetadata } from 'src/app/api-proxy/assignment-api-client-proxy';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  registrationFormGroup: FormGroup;
  commonErrors?: ProblemDetails[];
  isSaving: boolean = false;

  constructor(private readonly accountsClient: AccountsClient, private readonly router: Router) {
  }
  ngOnInit(): void {

    this.registrationFormGroup = new FormGroup({
        userName: new FormControl('', [
          Validators.required,
          Validators.email
        ]),
        password: new FormControl('', [ Validators.required ]),
        fullName: new FormControl('', [ Validators.required ])
    });
  }

  get userName(): AbstractControl { return this.registrationFormGroup.get('userName'); }

  get password(): AbstractControl { return this.registrationFormGroup.get('password'); }

  get fullName(): AbstractControl { return this.registrationFormGroup.get('fullName'); }

  async register(): Promise<void> {

      this.isSaving = true;
      const registrationData = this.registrationFormGroup.getRawValue();

      try {
        const registrationResult = await this.accountsClient.register(registrationData).toPromise();
        if(registrationResult.success) {
            this.router.navigateByUrl("/");
        }
      } catch (error) {
          const response = JSON.parse(await error.text()) as ResponseMetadata;
          this.commonErrors = response.errors;
      }

      this.isSaving = false;
  }

}
