import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Linija } from '../models/linija.model';
import { LineService } from '../services/line.service';
import { LinijaBroj } from '../models/linija-broj.model';
import { AddPolazak } from '../models/add-polazak.model';

@Component({
  selector: 'app-red-voznje',
  templateUrl: './red-voznje.component.html',
  styleUrls: ['./red-voznje.component.css']
})
export class RedVoznjeComponent implements OnInit {

  dani = ['Radni', 'Subota', 'Nedelja'];
  tipovi = ['Gradska', 'Prigradska'];
  linije: LinijaBroj[] = [{Id: 0, Broj: 0}];
  redVoznjeForm: FormGroup;
  showTable = false;
  polasci: string[] = [];
  polazakForm: FormGroup;
  obrisiPolazakForm: FormGroup;
  admin = false;
  constructor(private lineService: LineService) { }

  ngOnInit() {
    this.initForm();
    if (localStorage.role === 'Admin') {
      this.admin = true;
    } else {
      this.admin = false;
    }
    this.lineService.getLinijeTip(this.tipovi[0]).subscribe(data => {
      this.linije = data;
      this.redVoznjeForm.patchValue({linija: this.linije[0].Broj});
    });
  }

  initForm() {
    this.redVoznjeForm = new FormGroup({
      dan: new FormControl(this.dani[0], Validators.required),
      tipLinije: new FormControl(this.tipovi[0], Validators.required),
      linija: new FormControl(this.linije[0].Broj, Validators.required)
    });

    this.polazakForm = new FormGroup({
      id: new FormControl(null, Validators.required),
      danPolazak: new FormControl(null, Validators.required),
      polazak: new FormControl(null, Validators.required)
    });

    this.obrisiPolazakForm = new FormGroup({
      idPol: new FormControl(null, Validators.required)
    });
  }

  onTipChange(event) {
    this.lineService.getLinijeTip(this.redVoznjeForm.value.tipLinije).subscribe(data => {
      this.linije = data;
      this.redVoznjeForm.patchValue({linija: this.linije[0].Broj});
    });
  }

  onSubmit() {
    this.lineService.getRedVoznje(this.redVoznjeForm.value.dan, this.redVoznjeForm.value.linija.Id).subscribe(data => {
      this.polasci = data;
      this.showTable = true;
    }, err => {
      console.log(err);
    });
  }

  addPolazak() {
    const polazak = new AddPolazak(this.polazakForm.value.id, this.polazakForm.value.danPolazak, this.polazakForm.value.polazak);
    this.lineService.addPolazak(polazak).subscribe(data => {
      console.log(data);
    }, err => {
      console.log(err);
    });
  }

  obisiPolazak() {
    this.lineService.deletePolazak(this.obrisiPolazakForm.value.idPol).subscribe(data => {
      console.log(data);
    }, err => {
      console.log(err);
    });
  }
}
