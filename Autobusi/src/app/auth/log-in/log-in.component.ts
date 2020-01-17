import { Component, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { User } from 'src/app/models/user.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css']
})
export class LogInComponent implements OnInit {

  user: User;

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {
    this.authService.autoLogIn();
    this.authService.userChanged.subscribe(
      (user: User) => {
        this.user = user;
      }
    );
  }

  onSubmit(form: NgForm) {
    this.authService.logIn(form.value.email, form.value.password).subscribe(temp => {
      if (temp === `uspesno`) {
        console.log(temp);
        this.router.navigate(['/account']);
      } else if (temp === 'neuspesno') {
        console.log(temp);
        this.router.navigate(['/login']);
      }
    });
  }

}
