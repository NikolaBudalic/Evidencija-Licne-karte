using System.Data;
using System.Data.SqlClient;

namespace DBUtils
{
    public class TehnoloskaObradaGradjana : TabelaKlasa
    {
        public TehnoloskaObradaGradjana()
        {
            NazivTabele = "Gradjanin";
            StatusObrade = "Tehnološka obrada građana";
        }

        public DataTable DajSveGradjane()
        {
            NazivProcedure = "DajSveGradjane";
            return IzvrsiProceduruSelect();
        }

        public DataTable DajGradjanePoFilteru(string filter)
        {
            NazivProcedure = "DajGradjanePoFilteru";

            SqlParameter[] parametri =
            {
                new SqlParameter("@Filter", filter)
            };

            return IzvrsiProceduruSelect(parametri);
        }

        public DataTable DajGradjaninaPoJMBG(string jmbg)
        {
            NazivProcedure = "DajGradjaninaPoJMBG";

            SqlParameter[] parametri =
            {
                new SqlParameter("@JMBG", jmbg)
            };

            return IzvrsiProceduruSelect(parametri);
        }

        public override string DajOpisObrade()
        {
            return "Tehnološka klasa za obradu tabele Gradjanin preko stored procedura.";
        }
    }
}