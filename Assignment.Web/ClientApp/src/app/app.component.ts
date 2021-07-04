import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { AuthService, User } from './core/auth/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'ClientApp112';
  message = '';
  user: User;

  onUserLoggedInSubscription: Subscription;

  constructor(private readonly authService: AuthService) {

  }

  logout(): void {
    this.authService.logout();
  }
  ngOnDestroy(): void {
    this.onUserLoggedInSubscription.unsubscribe();
  }
  ngOnInit(): void {
    this.onUserLoggedInSubscription  = this.authService.onUserLoggedIn.subscribe(user => {
      this.user = user;
    });
  }
}
