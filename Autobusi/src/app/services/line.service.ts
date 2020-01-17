import { Injectable } from '@angular/core';
import { Linija } from '../models/linija.model';
import { Stanica } from '../models/stanica.model';
import { HttpClient } from '@angular/common/http';
import { LinijaBroj } from '../models/linija-broj.model';
import { AddPolazak } from '../models/add-polazak.model';
import { StanicaBind } from '../models/stanica-bind.model';
import { LinijaBind } from '../models/linija-bind.model';

@Injectable({
  providedIn: 'root'
})
export class LineService {
  baseUrl = `http://localhost:52295`;
  stanice: Stanica[] = [new Stanica('Centar', 'neka adresa', {lat: 45.254512, lng: 19.842476}, []),
                        new Stanica('Vojvode bojovica', 'adresa', {lat: 45.258032, lng: 19.842040}, []),
                        new Stanica('Trg marije trandafil', 'adresa', {lat: 45.260451, lng: 19.843154}, []),
                        new Stanica('Crkvs BJ', 'adresa crkve', {lat: 45.371199, lng: 19.872512}, []),
                        new Stanica('Cenej', 'adresa cenej', {lat: 45.331237, lng: 19.846707}, []),
                        new Stanica('Temerinski', 'temerinska adresa', {lat: 45.300550, lng: 19.831126}, [])];

  linije: Linija[] = [new Linija(1, [new Stanica('Centar', 'neka adresa', {lat: 45.254512, lng: 19.842476}, []),
                                    new Stanica('Vojvode bojovica', 'adresa', {lat: 45.258032, lng: 19.842040}, []),
                                    new Stanica('Trg marije trandafil', 'adresa', {lat: 45.260451, lng: 19.843154}, [])], '#0000FF'),
                      new Linija(1, [new Stanica('Temerinski', 'temerinska adresa', {lat: 45.300550, lng: 19.831126}, []),
                                    new Stanica('Cenej', 'adresa cenej', {lat: 45.331237, lng: 19.846707}, []),
                                    new Stanica('Crkvs BJ', 'adresa crkve', {lat: 45.371199, lng: 19.872512}, [])], '#00FFFF')];

  constructor(private httpClient: HttpClient) { }

  getStanice() {
    return this.httpClient.get<Stanica[]>(this.baseUrl + '/api/Line/GetStanice');
  }

  getLinije() {
    return this.httpClient.get<Linija[]>(this.baseUrl + '/api/Line/GetLinije');
  }

  getLinijeTip(tip: string) {
    return this.httpClient.get<LinijaBroj[]>(this.baseUrl + '/api/Line/GetLinijeTip?tip=' + tip);
  }

  addStanice(stanica: StanicaBind) {
    const httpOptions = {
      headers: {
          'Content-type': 'application/json'
      }
    };
    return this.httpClient.post(this.baseUrl + `/api/Line/AddStanica`, stanica, httpOptions);
  }

  addLinija(linija: LinijaBind) {
    const httpOptions = {
      headers: {
          'Content-type': 'application/json'
      }
    };
    return this.httpClient.post(this.baseUrl + `/api/Line/AddLinija`, linija, httpOptions);
  }

  getRedVoznje(dan: string, linija: number) {
    return this.httpClient.get<string[]>(this.baseUrl + `/api/Line/GetRedVoznje?dan=${dan}&linija=${linija}`);
  }

  addPolazak(polazak: AddPolazak) {
    const httpOptions = {
      headers: {
          'Content-type': 'application/json'
      }
    };
    return this.httpClient.post(this.baseUrl + `/api/Line/AddVoznja`, polazak, httpOptions);
  }

  deletePolazak(id: number) {
    return this.httpClient.delete(this.baseUrl + `/api/Line/DeleteVoznja?id=${id}`);
  }

  deleteLinija(id: number) {
    return this.httpClient.delete(this.baseUrl + `/api/Line/DeleteLinija?id=${id}`);
  }

  deleteStanica(id: number) {
    return this.httpClient.delete(this.baseUrl + `/api/Line/DeleteStanica?id=${id}`);
  }
}
