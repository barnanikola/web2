import { Linija } from './linija.model';

export class RedVoznje {
    constructor(public Dan: string,
                public Polasci: Date[],
                public LinijaVoznje: Linija) {}
}
