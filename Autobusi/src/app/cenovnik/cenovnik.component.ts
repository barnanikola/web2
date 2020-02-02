import { Component, OnInit } from '@angular/core';
import { Cenovnik } from '../models/cenovnik.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { TicketService } from '../services/ticket.service';
import { Karta } from '../models/karta.model';
import { debug } from 'util';
import { User } from '../models/user.model';
import { AuthService } from '../services/auth.service';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-cenovnik',
  templateUrl: './cenovnik.component.html',
  styleUrls: ['./cenovnik.component.css']
})
export class CenovnikComponent implements OnInit {
  vrste: string[] = ['vremenska', 'dnevna', 'mesecna', 'godisnja'];
  tipovi: string[] = ['obican', 'djak', 'penzioner'];
  cena: number;
  cenovnik: Cenovnik;
  kupovinaForm: FormGroup;
  user: User = null;
  adminEdit = false;
  editOdl = false;
  cenovnikForm: FormGroup;
  error: string;

  constructor(private ticketService: TicketService, private authService: AuthService) { }

  ngOnInit() {
    this.kupovinaForm = new FormGroup({
      vrstaKarte: new FormControl(this.vrste[0], Validators.required),
      tipKorisnika: new FormControl(this.tipovi[0], this.chekUser)
    });
    this.ticketService.getPriceList().subscribe(data => {
      this.cenovnik = data;
      this.calculate();
    }, err => {
      console.log(err);
    });
    this.authService.userChanged.subscribe(
      (user: User) => {
        this.user = user;
        if (this.user) {
          this.kupovinaForm.patchValue({tipKorisnika: user.TipKorisnika});
        }
      }
    );
  }

  calculate() {
    if (this.kupovinaForm.value.vrstaKarte === 'vremenska') {
      this.cena = this.cenovnik.CenaVremenskeKarte;
    } else if (this.kupovinaForm.value.vrstaKarte === 'dnevna') {
      this.cena = this.cenovnik.CenaDnevneKarte;
    } else if (this.kupovinaForm.value.vrstaKarte === 'mesecna') {
      this.cena = this.cenovnik.CenaMesecneKarte;
    } else if (this.kupovinaForm.value.vrstaKarte === 'godisnja') {
      this.cena = this.cenovnik.CenaGodisnjeKarte;
    }
    if (this.kupovinaForm.value.tipKorisnika === 'djak' ||
        this.kupovinaForm.value.tipKorisnika === 'penzioner') {
      this.cena = this.cena * 0.9;
    }
  }

  chekUser(control: FormControl) {
    if (control.value === localStorage.tipKorisnika) {
      return null;
    }
    return { userChekFail: true };
  }

  onSubmit() {
    this.ticketService.buyTicket(this.kupovinaForm.value.vrstaKarte, this.cena, this.kupovinaForm.value.tipKorisnika).subscribe(data => {
      this.error = `${this.kupovinaForm.value.vrstaKarte} karta uspesno kupljena`;
    }, err => {
      this.error = err.error.Message;
    });
  }

  onAdminEdit(mode: boolean) {
    if (mode) {
      this.cenovnikForm = new FormGroup({
        vaziOd: new FormControl(formatDate(this.cenovnik.VaziOd, 'yyyy-MM-dd', 'en'), Validators.required),
        vaziDo: new FormControl(formatDate(this.cenovnik.VaziDo, 'yyyy-MM-dd', 'en'), Validators.required),
        cenaVr: new FormControl(this.cenovnik.CenaVremenskeKarte, Validators.required),
        cenaDnevne: new FormControl(this.cenovnik.CenaDnevneKarte, Validators.required),
        cenaMes: new FormControl(this.cenovnik.CenaMesecneKarte, Validators.required),
        cenaGod: new FormControl(this.cenovnik.CenaGodisnjeKarte, Validators.required)
      });
      this.editOdl = true;
      this.adminEdit = true;
    } else {
      this.cenovnikForm = new FormGroup({
        vaziOd: new FormControl(null, Validators.required),
        vaziDo: new FormControl(null, Validators.required),
        cenaVr: new FormControl(null, Validators.required),
        cenaDnevne: new FormControl(null, Validators.required),
        cenaMes: new FormControl(null, Validators.required),
        cenaGod: new FormControl(null, Validators.required)
      });
      this.adminEdit = true;
    }
  }

  onSubmitCenovnik() {
    // tslint:disable-next-line: max-line-length
    const cenovnik: Cenovnik = new Cenovnik(this.cenovnikForm.value.vaziOd, this.cenovnikForm.value.vaziDo, this.cenovnikForm.value.cenaVr, this.cenovnikForm.value.cenaDnevne, this.cenovnikForm.value.cenaMes, this.cenovnikForm.value.cenaGod);
    if (this.editOdl) {
      cenovnik.Id = this.cenovnik.Id;
      this.ticketService.newCenovnik(cenovnik);
    } else {
      this.ticketService.newCenovnik(cenovnik);
    }
    this.adminEdit = false;
    this.editOdl = false;
  }

  onCancel() {
    this.adminEdit = false;
    this.editOdl = false;
  }
}
