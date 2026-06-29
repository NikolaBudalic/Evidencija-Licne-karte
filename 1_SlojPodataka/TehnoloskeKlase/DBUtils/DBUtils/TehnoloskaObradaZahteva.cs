using System.Data;
using System.Data.SqlClient;

namespace DBUtils
{
    public class TehnoloskaObradaZahteva : TabelaKlasa
    {
        public int IDZahteva { get; set; }

        public string TipZahteva { get; set; }

        public TehnoloskaObradaZahteva()
        {
            NazivTabele = "Zahtev";
            StatusObrade = "Tehnološka obrada zahteva";
        }

        public DataTable DajSveZahteve()
        {
            NazivProcedure = "DajSveZahteve";
            return IzvrsiProceduruSelect();
        }

        public DataTable DajZahtevePoFilteru(string filter)
        {
            NazivProcedure = "DajZahtevePoFilteru";

            SqlParameter[] parametri =
            {
                new SqlParameter("@Filter", filter)
            };

            return IzvrsiProceduruSelect(parametri);
        }

        public DataTable DajZahtevPoId(int idZahteva)
        {
            NazivProcedure = "DajDetaljeZahtevaPoID";

            SqlParameter[] parametri =
            {
                new SqlParameter("@IDZahteva", idZahteva)
            };

            return IzvrsiProceduruSelect(parametri);
        }

        public DataTable DajZahtevZaStampu(int idZahteva)
        {
            NazivProcedure = "DajZahtevZaStampu";

            SqlParameter[] parametri =
            {
                new SqlParameter("@IDZahteva", idZahteva)
            };

            return IzvrsiProceduruSelect(parametri);
        }

        public DataTable DajZahteveZaStampuPoStatusu(string statusZahteva)
        {
            NazivProcedure = "DajZahteveZaStampuPoStatusu";

            SqlParameter[] parametri =
            {
                new SqlParameter("@StatusZahteva", statusZahteva)
            };

            return IzvrsiProceduruSelect(parametri);
        }

        public override string DajOpisObrade()
        {
            return "Tehnološka klasa za obradu tabele Zahtev preko stored procedura.";
        }
    }
}