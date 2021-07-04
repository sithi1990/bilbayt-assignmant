import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { AccountsClient, CreateTokenCommand, ProblemDetails, ResponseMetadata, TokenResponse, UserInfoResult } from 'src/app/api-proxy/assignment-api-client-proxy';

export interface User {
    userId?: string | undefined;
    userName?: string | undefined;
    fullName?: string | undefined;
}

export interface ISignInResult {
  success: boolean;
  errors?: ProblemDetails[];
}
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private readonly accountsClient: AccountsClient, private readonly router: Router) {

  }

  private readonly authSessionKeyName = "authSession";
  private readonly userSessionKeyName = "userSession";

  onUserLoggedIn: BehaviorSubject<User> = new BehaviorSubject<User>(this.user);

  async signInAsync(userName: string, password: string): Promise<ISignInResult> {

    try {
      const loginResult = await this.accountsClient.createAndGetAccessToken({
       userName: userName,
       password: password
     } as CreateTokenCommand).toPromise();

     if(loginResult.success) {
        localStorage.setItem(this.authSessionKeyName, JSON.stringify(loginResult));
        const userInfo = await this.getUserInfo();
        localStorage.setItem(this.userSessionKeyName, JSON.stringify(userInfo));
        this.onUserLoggedIn.next(this.user);
     }
     return { success: loginResult.success, errors: loginResult.errors } as ISignInResult;

    } catch (error) {
      const response = JSON.parse(await error.text()) as ResponseMetadata;
      return { success: false, errors: response.errors } as ISignInResult;
    }

  }

  startAuthentication(): void {

    const currentUrl = this.router.url;
    this.router.navigateByUrl("/login", {
      queryParams: {
        redirectUrl: currentUrl
      }
    });
  }

  isLoggedIn(): boolean {
    if(this.tokenResponse && this.tokenResponse.expiration ) {
      const tokenExpirationTime = this.getUtcMilliseconds(new Date(this.tokenResponse.expiration));
      const currentUtcTime = this.getUtcMilliseconds(new Date());
      return (tokenExpirationTime - currentUtcTime) > 0;
    }
    return false;
  }

  logout(): void {
    localStorage.removeItem(this.authSessionKeyName);
    localStorage.removeItem(this.userSessionKeyName);
    this.startAuthentication();
  }

  private getUtcMilliseconds(date: Date): number {
      return Date.UTC(date.getUTCFullYear(),date.getUTCMonth(), date.getUTCDate() ,date.getUTCHours(), date.getUTCMinutes(), date.getUTCSeconds(), date.getUTCMilliseconds());
  }

  get tokenResponse(): TokenResponse | null {
    if(localStorage.getItem(this.authSessionKeyName)) {
        return JSON.parse(localStorage.getItem(this.authSessionKeyName)) as TokenResponse;
    }
    return null;
  }

  get accessToken(): string | null {
    if(this.isLoggedIn()) {
      return this.tokenResponse.accessToken;
    }
    return null;
  }

  get authHeader(): string {
    if(this.accessToken) {
      return `Bearer ${this.accessToken}`
    }
    return null;
  }

  get user(): User {
    if(this.isLoggedIn()) {
      return JSON.parse(localStorage.getItem(this.userSessionKeyName)) as User;
    }
    return null;
  }

  private async getUserInfo(): Promise<UserInfoResult> {
      try {
         const result = await this.accountsClient.getUserInfo().toPromise();
         return result.userInfo;

      } catch (error) {
        const response = JSON.parse(await error.text()) as ResponseMetadata;
        console.error(response);
      }
  }

}
