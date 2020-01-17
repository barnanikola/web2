import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

import { User } from '../models/user.model';
import { AuthService } from '../services/auth.service';
import { Observable } from 'rxjs';
import { DomSanitizer } from '@angular/platform-browser';




@Component({
  selector: 'app-verifikacija',
  templateUrl: './verifikacija.component.html',
  styleUrls: ['./verifikacija.component.css']
})
export class VerifikacijaComponent implements OnInit {
  korisniciPrikaz: User[];
  korisnici: User[];
  filter = new FormControl('');
  selektovanKorisnik: User;
  statusi = ['zahtev se procesira', 'zahtev prihvacen', 'zahtev odbijen'];
  verifikacijaForm: FormGroup;
  safeUrl: any;

  change: Observable<User[]>;
  constructor(private authService: AuthService, private sanitizer: DomSanitizer) {
  }

  ngOnInit() {
    this.authService.getUsers().subscribe(data => {
      this.korisnici = data;
      this.korisniciPrikaz = this.korisnici;
    });
  }

  search(text: string): User[] {
    return this.korisnici.filter(korisnik => {
      const term = text.toLowerCase();
      return korisnik.Email.toLowerCase().includes(term)
          || korisnik.Ime.toLowerCase().includes(term)
          || korisnik.Prezime.toLowerCase().includes(term)
          || korisnik.TipKorisnika.toLowerCase().includes(term)
          || korisnik.Status.toLowerCase().includes(term);
    });
  }

  onKey(event) {
    this.korisniciPrikaz = this.search(event);
  }

  onSellectedUser(email: string) {
    for ( const user of this.korisnici) {
      if (user.Email === email) {
        this.selektovanKorisnik = user;
      }
    }
    if (this.selektovanKorisnik.Url) {
      this.authService.getImageEmail(this.selektovanKorisnik.Email).subscribe(imageData => {
        const objectURL = URL.createObjectURL(imageData);
        this.safeUrl = this.sanitizer.bypassSecurityTrustUrl(objectURL);
      });
    }
    this.verifikacijaForm = new FormGroup({
      statusZahteva: new FormControl(this.selektovanKorisnik.Status)
    });
  }

  onSubmit() {
    this.authService.changeStatus(this.selektovanKorisnik.Email, this.verifikacijaForm.value.statusZahteva).subscribe(data => {
      for ( const user of this.korisnici) {
        if (user.Email === this.selektovanKorisnik.Email) {
          user.Status = this.verifikacijaForm.value.statusZahteva;
        }
      }
    }, err => {
      console.log(err);
    });
  }
}
