using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using DBUtils;
using System.Data;

namespace KlasePodataka
{
    public class NastavnikDBKlasa: TabelaKlasa
    {
        

        public NastavnikDBKlasa(KonekcijaKlasa novaKonekcija, string noviNazivTabele):base(novaKonekcija, noviNazivTabele)
        {
            // nesto drugo u vezi specificno ove klase
        }
        
        
        public DataSet DajSveNastavnike()
        {
            return this.DajPodatke("select * from Nastavnik");
        }

        public bool DodajNovogNastavnika(NastavnikKlasa noviNastavnikObjekat)
        {
            string upit="insert into Nastavnik values('" + noviNastavnikObjekat.JMBG + "', '" + noviNastavnikObjekat.Prezime + "','" + noviNastavnikObjekat.Ime + "','" + noviNastavnikObjekat.Zvanje.Sifra + "')";
            return this.IzvrsiAzuriranje(upit);

        }
    }
}
