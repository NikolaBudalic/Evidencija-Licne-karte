using System.Collections.Generic;
using KlasePodataka;

namespace DBUtils.Repozitorijumi
{
    public interface IGradjaninRepozitorijum
    {
        List<Gradjanin> DajSve();

        Gradjanin DajPoJMBG(string jmbg);

        void Dodaj(Gradjanin gradjanin);

        void Izmeni(Gradjanin gradjanin);

        void Obrisi(string jmbg);
    }
}