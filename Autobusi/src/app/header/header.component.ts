import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { User } from '../models/user.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  colapsed = true;
  loggedIn = false;
  kontroler = false;
  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {
    this.authService.userChanged.subscribe(
      (user: User) => {
        if (user === null) {
          this.loggedIn = false;
          this.kontroler = false;
        } else {
          this.loggedIn = true;
          if (localStorage.role === 'Controller') {
            this.kontroler = true;
          } else {
            this.kontroler = false;
          }
        }
      }
    );
  }

  onLogout() {
    this.authService.logOut().subscribe();
    this.router.navigate(['/login']);
  }
}
