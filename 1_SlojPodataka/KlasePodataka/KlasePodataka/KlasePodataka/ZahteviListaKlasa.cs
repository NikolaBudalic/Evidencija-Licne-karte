using System;
using System.Collections.Generic;

namespace KlasePodataka
{
    public class ZahtevListaKlasa
    {
        private List<ZahtevKlasa> _listaZahteva;

        public List<ZahtevKlasa> ListaZahteva
        {
            get { return _listaZahteva; }
            set { _listaZahteva = value; }
        }

        public ZahtevListaKlasa()
        {
            _listaZahteva = new List<ZahtevKlasa>();
        }

        public void DodajElementListe(ZahtevKlasa noviZahtevObjekat)
        {
            _listaZahteva.Add(noviZahtevObjekat);
        }

        public void ObrisiElementListe(ZahtevKlasa zahtevObjekatZaBrisanje)
        {
            _listaZahteva.Remove(zahtevObjekatZaBrisanje);
        }

        public void ObrisiElementNaPoziciji(int pozicija)
        {
            _listaZahteva.RemoveAt(pozicija);
        }

        public void IzmeniElementListe(ZahtevKlasa stariZahtevObjekat, ZahtevKlasa noviZahtevObjekat)
        {
            int indexStarogZahteva = _listaZahteva.IndexOf(stariZahtevObjekat);
            _listaZahteva.RemoveAt(indexStarogZahteva);
            _listaZahteva.Insert(indexStarogZahteva, noviZahtevObjekat);
        }
    }
}