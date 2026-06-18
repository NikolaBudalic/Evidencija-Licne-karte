using System;
using System.ComponentModel.DataAnnotations;

namespace LicnaKarta.ViewModels
{
    public class ZahtevViewModel
    {
        [Required(ErrorMessage = "Ime je obavezno.")]
        [StringLength(50, ErrorMessage = "Ime može imati najviše 50 karaktera.")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Prezime je obavezno.")]
        [StringLength(50, ErrorMessage = "Prezime može imati najviše 50 karaktera.")]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "JMBG je obavezan.")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "JMBG mora imati tačno 13 cifara.")]
        public string JMBG { get; set; }

        [Required(ErrorMessage = "Datum rođenja je obavezan.")]
        public DateTime? DatumRodjenja { get; set; }

        [Required(ErrorMessage = "Pol je obavezan.")]
        public string Pol { get; set; }

        [Required(ErrorMessage = "Državljanstvo je obavezno.")]
        [StringLength(50, ErrorMessage = "Državljanstvo može imati najviše 50 karaktera.")]
        public string Drzavljanstvo { get; set; }

        public string BrojStareLK { get; set; }

        public DateTime? DatumIstekaLK { get; set; }

        [Required(ErrorMessage = "Adresa prebivališta je obavezna.")]
        [StringLength(100, ErrorMessage = "Adresa može imati najviše 100 karaktera.")]
        public string AdresaPrebivalista { get; set; }

        [Required(ErrorMessage = "Kontakt telefon je obavezan.")]
        [RegularExpression(@"^[0-9]{8,15}$", ErrorMessage = "Telefon mora imati od 8 do 15 cifara.")]
        public string KontaktTelefon { get; set; }

        [EmailAddress(ErrorMessage = "Email nije u ispravnom formatu.")]
        public string Email { get; set; }

        public DateTime DatumPodnosenja { get; set; }

        [Required(ErrorMessage = "Razlog izdavanja je obavezan.")]
        public string RazlogIzdavanja { get; set; }

        [Required(ErrorMessage = "Tip zahteva je obavezan.")]
        public string TipZahteva { get; set; }

        [Required(ErrorMessage = "Mesto podnošenja je obavezno.")]
        [StringLength(50, ErrorMessage = "Mesto podnošenja može imati najviše 50 karaktera.")]
        public string MestoPodnosenja { get; set; }

        public string StatusZahteva { get; set; }

        public string RoditeljImePrezime { get; set; }

        [RegularExpression(@"^\d{13}$", ErrorMessage = "JMBG roditelja/staratelja mora imati tačno 13 cifara.")]
        public string RoditeljJMBG { get; set; }

        public string Srodstvo { get; set; }

        [RegularExpression(@"^[0-9]{8,15}$", ErrorMessage = "Telefon roditelja/staratelja mora imati od 8 do 15 cifara.")]
        public string RoditeljTelefon { get; set; }

        [EmailAddress(ErrorMessage = "Email roditelja/staratelja nije u ispravnom formatu.")]
        public string RoditeljEmail { get; set; }

        public bool IzvodIzMaticneKnjigeRodjenih { get; set; }
        public bool UverenjeODrzavljanstvu { get; set; }
        public bool DokazOPrebivalistu { get; set; }
        public bool StaraLicnaKarta { get; set; }
        public bool DokazOUplatiTakse { get; set; }
        public bool Fotografija { get; set; }
        public bool SaglasnostRoditelja { get; set; }

        [StringLength(250, ErrorMessage = "Napomena može imati najviše 250 karaktera.")]
        public string Napomena { get; set; }
    }
}