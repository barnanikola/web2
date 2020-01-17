import { Injectable } from '@angular/core';
import { Cenovnik } from '../models/cenovnik.model';
import { Karta } from '../models/karta.model';
import { AuthService } from './auth.service';
import { HttpClient } from '@angular/common/http';
import { KupiKartu } from '../models/kupi-kartu.model';

@Injectable({
  providedIn: 'root'
})
export class TicketService {

  kartaId = 4;
  baseUrl = `http://localhost:52295`;
  constructor(private authService: AuthService, private httpClient: HttpClient) { }

  getPriceList() {
    return this.httpClient.get<Cenovnik>(this.baseUrl + '/api/Ticket/GetPriceList');
  }

  buyTicket(tipKarte: string, cena: number, tipKorisnika: string) {
    const httpOptions = {
      headers: {
          'Content-type': 'application/json'
      }
    };
    const karta: KupiKartu = new KupiKartu(tipKarte, cena, localStorage.getItem(`loggedUser`));
    return  this.httpClient.post<any>(this.baseUrl + '/api/Ticket/BuyTicket', karta, httpOptions);
  }

  getTickets() {
    return this.httpClient.get<Karta[]>(this.baseUrl + '/api/Ticket/GetTickets');
  }

  chekValidity(id: number) {
    return this.httpClient.get<string>(this.baseUrl + `/api/Ticket/ChekValidity?id=${id}`);
  }

  newCenovnik(cenovnik: Cenovnik) {
    const httpOptions = {
      headers: {
        'Content-Type': 'application/json'
      }
    };
    this.httpClient.post(this.baseUrl + '/api/Ticket/AddCenovnik', cenovnik, httpOptions).subscribe(data => {
    }, err => {
      console.log(err);
    });
  }
}
