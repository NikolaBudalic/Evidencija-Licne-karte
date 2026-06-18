using System.Collections.Generic;
using KlasePodataka;

namespace DBUtils.Repozitorijumi
{
    public interface IZahtevRepozitorijum
    {
        List<Zahtev> DajSve();

        Zahtev DajPoId(int id);

        void Dodaj(Zahtev zahtev);

        void Izmeni(Zahtev zahtev);

        void Obrisi(int id);
    }
}