import { Component, OnInit, PipeTransform } from '@angular/core';
import { NgForm } from '@angular/forms';

import { Karta } from '../models/karta.model';
import { TicketService } from '../services/ticket.service';

@Component({
  selector: 'app-kontroler',
  templateUrl: './kontroler.component.html',
  styleUrls: ['./kontroler.component.css']
})
export class KontrolerComponent implements OnInit {

  karte: Karta[];
  rezultatProvere: string;

  constructor(private ticketService: TicketService) { }

  ngOnInit() {
    this.ticketService.getTickets().subscribe(data => {
      this.karte = data;
    });
  }

  onSubmit(form: NgForm) {
    this.ticketService.chekValidity(form.value.id).subscribe(data => {
      this.rezultatProvere = data;
    });
    form.reset();
  }
}
