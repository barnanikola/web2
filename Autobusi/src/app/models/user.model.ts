export class User {
    constructor(public Email: string,
                public Ime: string,
                public Prezime: string,
                public DatRodj: Date,
                public Adresa: string,
                public TipKorisnika: string,
                public Status?: string,
                public VrstaNaloga?: string,
                public Url?: string,
                public Password?: string,
                public ConfirmPassword?: string) {}
}
