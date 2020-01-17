import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';

import { User } from '../models/user.model';
import { ChangePasswordModel } from '../models/password.model';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl = `http://localhost:52295`;
  userChanged = new BehaviorSubject<User>(null);
  // tslint:disable-next-line: max-line-length
  users: User[] = [new User('nikola93b@yahoo.com', 'Nikola', 'Barna', new Date(1993, 11, 18), 'Novosadska 120', 'djak', 'zahtev se procesira', 'Korisnik'),
                  // tslint:disable-next-line: max-line-length
                  new User('nikola.barna@protonmail.com', 'Marko', 'Markovic', new Date(1993, 11, 18), 'Novosadska 120', 'obican', 'zahtev prihvacen', 'Kontroler'),
                // tslint:disable-next-line: max-line-length
                new User('admin@yahoo.com', 'Admin', 'Adminovic', new Date(1993, 11, 18), 'Novosadska 120', 'obican', 'zahtev prihvacen', 'Admin')];
  constructor(private httpClient: HttpClient, private router: Router) { }

  addUser(user: User): Observable<any> {
    const data = user;
    const httpOptions = {
      headers: {
          'Content-type': 'application/json'
      }
    };
    return this.httpClient.post<any>(this.baseUrl + '/api/Account/Register', data, httpOptions);
  }

  logIn(email: string, password: string): Observable<any> {

    const data = `username=${email}&password=${password}&grant_type=password`;
    const httpOptions = {'Content-Type': 'application/x-www-form-urlencoded', 'Access-Control-Allow-Origin': '*'};

    // tslint:disable-next-line: deprecation
    return Observable.create((observer) => {
      // tslint:disable-next-line: no-shadowed-variable
      this.httpClient.post<any>(`http://localhost:52295/oauth/token`, data, {headers : httpOptions}).subscribe(data => {
        localStorage.jwt = data.access_token;
        const jwtData = localStorage.jwt.split('.')[1];
        const decodedJwtJsonData = window.atob(jwtData);
        const decodedJwtData = JSON.parse(decodedJwtJsonData);
        const role = decodedJwtData.role;
        localStorage.setItem(`role`, role);
        localStorage.setItem(`loggedUser`, email);
        localStorage.loggedIn = true;
        observer.next(`uspesno`);
        observer.complete();
      },
      err => {
        console.log(err);
        observer.next(`neuspesno`);
        observer.complete();
      });
    });
  }

  logOut(): Observable<any> {
    // tslint:disable-next-line: deprecation
    return Observable.create((observer) => {
        localStorage.setItem('loggedUser', undefined);
        localStorage.jwt = undefined;
        localStorage.role = undefined;
        localStorage.loggedIn = false;
        localStorage.tipKorisnika = 'obican';
        this.userChanged.next(null);
    });
}

  autoLogIn() {
    if (localStorage.jwt !== 'undefined') {
      this.router.navigate(['/account']);
    }
  }

  // tslint:disable-next-line: max-line-length
  changeUserData(email: string, ime: string, prezime: string, datRodj: Date, adresa: string, tipKorisnika: string) {
    const user: User = new User(email, ime, prezime, datRodj, adresa, tipKorisnika);
    const httpOptions = {
      headers: {
          'Content-type': 'application/json'
      }
    };
    return this.httpClient.post<User>(this.baseUrl + '/api/Account/UpdateUser', user, httpOptions);
  }

  getUsers() {
    return this.httpClient.get<User[]>(this.baseUrl + '/api/Account/GetUsers');
  }

  getUser() {
    return this.httpClient.get<any>(this.baseUrl + '/api/Account/GetUser');
  }

  changeStatus(email: string, status: string) {
    const data = {Email: email, Status: status};
    const httpOptions = {
      headers: {
          'Content-type': 'application/json'
      }
    };
    return this.httpClient.post(this.baseUrl + `/api/Account/ChangeStatus`, data, httpOptions);
  }

  uploadImage(image: File) {
    const formData: FormData = new FormData();
    formData.append('Image', image, image.name);
    formData.append('Imagecaption', 'dokument');
    return this.httpClient.post(this.baseUrl + '/api/Account/UploadImage', formData);
  }

  getImage() {
    return this.httpClient.get(this.baseUrl + '/api/Account/GetImage', {responseType: 'blob'});
  }

  getImageEmail(email: string) {
    return this.httpClient.get(this.baseUrl + `/api/Account/GetImageEmail?email=${email}`, {responseType: 'blob'});
  }

  changePassword(passwords: ChangePasswordModel) {
    return this.httpClient.post(this.baseUrl + '/api/Account/ChangePassword', passwords);
  }
}
