export class Cenovnik {
    constructor(public VaziOd: Date,
                public VaziDo: Date,
                public CenaVremenskeKarte: number,
                public CenaDnevneKarte: number,
                public CenaMesecneKarte: number,
                public CenaGodisnjeKarte: number,
                public Id?: number) {}
}
