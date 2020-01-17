export class StanicaBind {
    constructor(public Naziv: string,
                public Adresa: string,
                public Position: { lat: number, lng: number},
                public Linije: number[],
                public IdStanice?: number) {}
}
