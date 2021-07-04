import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProblemDetails } from 'src/app/api-proxy/assignment-api-client-proxy';
import { AuthService } from 'src/app/core/auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginFormGroup: FormGroup;
  commonErrors?: ProblemDetails[];
  private readonly redirectUrl: string;

  constructor(private readonly authService: AuthService,  private readonly router: Router, private readonly route: ActivatedRoute) {
    this.redirectUrl = this.route.snapshot.paramMap.get('redirectUrl');
  }

  ngOnInit(): void {

    this.loginFormGroup = new FormGroup({
        userName: new FormControl('', [
          Validators.required,
          Validators.email
        ]),
        password: new FormControl('', [ Validators.required ])
    });
  }

  get userName(): AbstractControl { return this.loginFormGroup.get('userName'); }

  get password(): AbstractControl { return this.loginFormGroup.get('password'); }

  async login(): Promise<void> {

      var signinResult = await this.authService.signInAsync(this.userName.value, this.password.value);
      if(!signinResult.success) {
        this.commonErrors = signinResult.errors;
      } else {
        this.router.navigateByUrl(this.redirectUrl);
      }
  }

  async register(): Promise<void> {
    await this.router.navigateByUrl("/register");
  }
}
