import { Component, OnInit } from '@angular/core';
import { AccountsClient, ResponseMetadata, UserInfoResponse, UserInfoResult } from 'src/app/api-proxy/assignment-api-client-proxy';
import { AuthService, User } from 'src/app/core/auth/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  user: User;

  constructor(private readonly authService: AuthService) { }

  ngOnInit(): void {
    this.user = this.authService.user;
  }
}
