import { Linija } from './linija.model';

export class Stanica {
    constructor(public Naziv: string,
                public Adresa: string,
                public Position: { lat: number, lng: number},
                public Linije: Linija[],
                public Id?: number) {}
}
