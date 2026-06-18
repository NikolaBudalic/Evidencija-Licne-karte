using System;
using System.Collections.Generic;

namespace KlasePodataka
{
    public class GradjaninTabelaKlasa
    {
        private List<GradjaninKlasa> _listaGradjana;

        public List<GradjaninKlasa> ListaGradjana
        {
            get { return _listaGradjana; }
            set { _listaGradjana = value; }
        }

        public GradjaninTabelaKlasa()
        {
            _listaGradjana = new List<GradjaninKlasa>();
        }

        public void DodajElementListe(GradjaninKlasa noviGradjaninObjekat)
        {
            _listaGradjana.Add(noviGradjaninObjekat);
        }

        public void ObrisiElementListe(GradjaninKlasa gradjaninObjekatZaBrisanje)
        {
            _listaGradjana.Remove(gradjaninObjekatZaBrisanje);
        }

        public void ObrisiElementNaPoziciji(int pozicija)
        {
            _listaGradjana.RemoveAt(pozicija);
        }

        public void IzmeniElementListe(GradjaninKlasa stariGradjaninObjekat, GradjaninKlasa noviGradjaninObjekat)
        {
            int indexStarogGradjanina = _listaGradjana.IndexOf(stariGradjaninObjekat);
            _listaGradjana.RemoveAt(indexStarogGradjanina);
            _listaGradjana.Insert(indexStarogGradjanina, noviGradjaninObjekat);
        }
    }
}