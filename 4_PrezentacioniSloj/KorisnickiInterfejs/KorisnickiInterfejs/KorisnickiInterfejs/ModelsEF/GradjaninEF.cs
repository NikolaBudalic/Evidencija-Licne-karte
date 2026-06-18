using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KorisnickiInterfejs.ModelsEF
{
    [Table("Gradjanin")]
    public class GradjaninEF
    {
        [Key]
        public string JMBG { get; set; }

        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Pol { get; set; }
        public string Drzavljanstvo { get; set; }
        public string AdresaPrebivalista { get; set; }
        public string KontaktTelefon { get; set; }
        public string Email { get; set; }
        public string BrojStareLK { get; set; }
        public DateTime? DatumIstekaLK { get; set; }
    }
}