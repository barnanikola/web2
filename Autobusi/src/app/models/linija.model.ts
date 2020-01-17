import { Stanica } from './stanica.model';

export class Linija {
    constructor(public Broj: number,
                public Stanice: Stanica[],
                public Boja: string) {}
}
