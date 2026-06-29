using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DBUtils
{
    public class TabelaKlasa : OsnovnaTehnoloskaKlasa
    {
        protected string NazivTabele { get; set; }
        protected string NazivProcedure { get; set; }

        private readonly string stringKonekcije;

        public TabelaKlasa()
        {
            stringKonekcije = ConfigurationManager
                .ConnectionStrings["NasaKonekcija"]
                .ConnectionString;

            StatusObrade = "Rad sa SQL Server bazom podataka";
        }

        protected DataTable IzvrsiProceduruSelect(SqlParameter[] parametri = null)
        {
            using (SqlConnection konekcija = new SqlConnection(stringKonekcije))
            {
                using (SqlCommand komanda = new SqlCommand(NazivProcedure, konekcija))
                {
                    komanda.CommandType = CommandType.StoredProcedure;

                    if (parametri != null)
                    {
                        komanda.Parameters.AddRange(parametri);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(komanda))
                    {
                        DataTable tabela = new DataTable();
                        adapter.Fill(tabela);
                        return tabela;
                    }
                }
            }
        }

        protected int IzvrsiProceduruKomanda(SqlParameter[] parametri = null)
        {
            using (SqlConnection konekcija = new SqlConnection(stringKonekcije))
            {
                using (SqlCommand komanda = new SqlCommand(NazivProcedure, konekcija))
                {
                    komanda.CommandType = CommandType.StoredProcedure;

                    if (parametri != null)
                    {
                        komanda.Parameters.AddRange(parametri);
                    }

                    konekcija.Open();
                    return komanda.ExecuteNonQuery();
                }
            }
        }

        public override string DajOpisObrade()
        {
            return "Tehnološka klasa za rad sa SQL Server bazom preko stored procedura.";
        }
    }
}