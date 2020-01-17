import { Component, OnInit, ViewChild } from '@angular/core';
import {} from 'googlemaps';

import { Stanica } from '../models/stanica.model';
import { LineService } from '../services/line.service';
import { Linija } from '../models/linija.model';
import { FormGroup, FormControl, Validators, NgForm } from '@angular/forms';
import { StanicaBind } from '../models/stanica-bind.model';
import { LinijaBind } from '../models/linija-bind.model';


@Component({
  selector: 'app-linije',
  templateUrl: './linije.component.html',
  styleUrls: ['./linije.component.css']
})
export class LinijeComponent implements OnInit {
  @ViewChild('map', {static: false}) mapElement: any;
  map: google.maps.Map;
  mapOptions = {
    center: new google.maps.LatLng(45.267136, 19.833549),
    mapTypeId: google.maps.MapTypeId.ROADMAP,
    zoom: 13
  };
  stanice: Stanica[];
  linije: Linija[];
  stanicaForm: FormGroup;
  linijaForm: FormGroup;
  tipNaloga: string;

  dodateStanice: number[] = [];
  dodateLinije: number[] = [];

  admin = false;
  tipovi = ['Gradska', 'Prigradska'];
  constructor(private lineService: LineService) { }

  ngOnInit() {
    if (localStorage.role === 'Admin') {
      this.admin = true;
    } else {
      this.admin = false;
    }
    this.tipNaloga = localStorage.tipNaloga;
    this.lineService.getLinije().subscribe(linije => {
      this.linije = linije;
      this.lineService.getStanice().subscribe(stanice => {
        this.stanice = stanice;
        this.initMap();
      }, errStanice => {
        console.log(errStanice);
      });
    }, errLinije => {
      console.log(errLinije);
    });
    this.initLinijaForm();
    this.initStanicaForm();
  }

  initMap() {
    this.map = new google.maps.Map(document.getElementById('googleMap'), this.mapOptions);
    for (const stanica of this.stanice) {
      let linijeString = '';
      for (const lin of stanica.Linije) {
        linijeString = linijeString + '<li>' + lin.Broj + '</li>';
      }
      // tslint:disable-next-line: max-line-length
      const contentString = `<h3>${stanica.Naziv}</h3><p>${stanica.Adresa}</p><ul>${linijeString}</ul>`;
      const marker = new google.maps.Marker({position: stanica.Position, map: this.map});
      const infowindow = new google.maps.InfoWindow({
        content: contentString
      });
      marker.addListener('click', m => {
        infowindow.open(this.map, marker);
      });
    }

    for (const linija of this.linije) {
      const coordinates: {lat: number, lng: number} [] = [];
      for (const stanica of linija.Stanice) {
        coordinates.push(stanica.Position);
      }
      const busLine = new google.maps.Polyline({
        path: coordinates,
        strokeColor: linija.Boja,
        strokeOpacity: 0.8,
        strokeWeight: 4,
        map: this.map
      });
    }
  }

  initStanicaForm() {
    this.stanicaForm = new FormGroup({
      naziv: new FormControl(null, Validators.required),
      adresa: new FormControl(null, Validators.required),
      lat: new FormControl(null, Validators.required),
      lng: new FormControl(null, Validators.required),
      brojLinije: new FormControl(null),
      idStaniceEdit: new FormControl(null)
    });
  }

  initLinijaForm() {
    this.linijaForm = new FormGroup({
      broj: new FormControl(null, Validators.required),
      boja: new FormControl(null, Validators.required),
      idStanice: new FormControl(null),
      tipLinije: new FormControl(null, Validators.required),
      idLinije: new FormControl(null)
    });
  }

  onAddStanica() {
    this.dodateStanice.push(this.linijaForm.value.idStanice);
    this.linijaForm.patchValue({idStanice: ''});
  }

  onAddLinija() {
    this.dodateLinije.push(this.stanicaForm.value.brojLinije);
    this.stanicaForm.patchValue({brojLinije: ''});
  }

  stanicaSubmit() {
    // tslint:disable-next-line: max-line-length
    const stanica = new StanicaBind(this.stanicaForm.value.naziv, this.stanicaForm.value.adresa, {lat: this.stanicaForm.value.lat, lng: this.stanicaForm.value.lng}, this.dodateLinije, this.stanicaForm.value.idStaniceEdit);
    this.lineService.addStanice(stanica).subscribe(data => {
      console.log(data);
    }, err => {
      console.log(err);
    });
  }

  linijaSubmit() {
    // tslint:disable-next-line: max-line-length
    const linija = new LinijaBind(this.linijaForm.value.broj, this.dodateStanice, this.linijaForm.value.boja, this.linijaForm.value.tipLinije, this.linijaForm.value.idLinije);
    this.lineService.addLinija(linija).subscribe(data => {
      console.log(data);
    }, err => {
      console.log(err);
    });
  }

  deleteStanica(form: NgForm) {
    this.lineService.deleteStanica(form.value.idStaniceDelete).subscribe(data => {
      form.reset();
      console.log(data);
    }, err => {
      form.reset();
      console.log(err);
    });
  }

  deleteLinija(form: NgForm) {
    this.lineService.deleteLinija(form.value.idLinijeDelete).subscribe(data => {
      form.reset();
      console.log(data);
    }, err => {
      form.reset();
      console.log(err);
    });
  }
}
