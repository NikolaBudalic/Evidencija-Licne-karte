namespace DBUtils
{
    public class TehnoloskaObradaZahteva : OsnovnaTehnoloskaKlasa
    {
        public int IDZahteva { get; set; }

        public string TipZahteva { get; set; }

        public override string DajOpisObrade()
        {
            return "Obrada zahteva za izdavanje lične karte.";
        }
    }
}