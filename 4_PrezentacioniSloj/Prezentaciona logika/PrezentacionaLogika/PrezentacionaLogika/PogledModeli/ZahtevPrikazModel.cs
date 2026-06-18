using System;
using System.ComponentModel.DataAnnotations;

namespace PrezentacionaLogika.PogledModeli
{
    public class ZahtevPrikazModel
    {
        [Display(Name = "Ime")]
        [Required(ErrorMessage = "Ime je obavezno.")]
        [StringLength(50, ErrorMessage = "Ime može imati najviše 50 karaktera.")]
        public string Ime { get; set; }

        [Display(Name = "Prezime")]
        [Required(ErrorMessage = "Prezime je obavezno.")]
        [StringLength(50, ErrorMessage = "Prezime može imati najviše 50 karaktera.")]
        public string Prezime { get; set; }

        [Display(Name = "JMBG")]
        [Required(ErrorMessage = "JMBG je obavezan.")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "JMBG mora imati tačno 13 cifara.")]
        public string JMBG { get; set; }

        [Display(Name = "Datum rođenja")]
        [Required(ErrorMessage = "Datum rođenja je obavezan.")]
        public DateTime? DatumRodjenja { get; set; }

        [Display(Name = "Pol")]
        [Required(ErrorMessage = "Pol je obavezan.")]
        public string Pol { get; set; }

        [Display(Name = "Državljanstvo")]
        [Required(ErrorMessage = "Državljanstvo je obavezno.")]
        [StringLength(50, ErrorMessage = "Državljanstvo može imati najviše 50 karaktera.")]
        public string Drzavljanstvo { get; set; }

        [Display(Name = "Broj stare lične karte")]
        public string BrojStareLK { get; set; }

        [Display(Name = "Datum isteka stare lične karte")]
        public DateTime? DatumIstekaLK { get; set; }

        [Display(Name = "Adresa prebivališta")]
        [Required(ErrorMessage = "Adresa prebivališta je obavezna.")]
        [StringLength(100, ErrorMessage = "Adresa može imati najviše 100 karaktera.")]
        public string AdresaPrebivalista { get; set; }

        [Display(Name = "Kontakt telefon")]
        [Required(ErrorMessage = "Kontakt telefon je obavezan.")]
        [RegularExpression(@"^[0-9]{8,15}$", ErrorMessage = "Telefon mora imati od 8 do 15 cifara.")]
        public string KontaktTelefon { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email nije u ispravnom formatu.")]
        public string Email { get; set; }

        [Display(Name = "Datum podnošenja")]
        public DateTime DatumPodnosenja { get; set; }

        [Display(Name = "Razlog izdavanja")]
        [Required(ErrorMessage = "Razlog izdavanja je obavezan.")]
        public string RazlogIzdavanja { get; set; }

        [Display(Name = "Tip zahteva")]
        [Required(ErrorMessage = "Tip zahteva je obavezan.")]
        public string TipZahteva { get; set; }

        [Display(Name = "Mesto podnošenja")]
        [Required(ErrorMessage = "Mesto podnošenja je obavezno.")]
        [StringLength(50, ErrorMessage = "Mesto podnošenja može imati najviše 50 karaktera.")]
        public string MestoPodnosenja { get; set; }

        [Display(Name = "Status zahteva")]
        public string StatusZahteva { get; set; }

        [Display(Name = "Ime i prezime roditelja/staratelja")]
        public string RoditeljImePrezime { get; set; }

        [Display(Name = "JMBG roditelja/staratelja")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "JMBG roditelja/staratelja mora imati tačno 13 cifara.")]
        public string RoditeljJMBG { get; set; }

        [Display(Name = "Srodstvo")]
        public string Srodstvo { get; set; }

        [Display(Name = "Telefon roditelja/staratelja")]
        [RegularExpression(@"^[0-9]{8,15}$", ErrorMessage = "Telefon roditelja/staratelja mora imati od 8 do 15 cifara.")]
        public string RoditeljTelefon { get; set; }

        [Display(Name = "Email roditelja/staratelja")]
        [EmailAddress(ErrorMessage = "Email roditelja/staratelja nije u ispravnom formatu.")]
        public string RoditeljEmail { get; set; }

        [Display(Name = "Izvod iz matične knjige rođenih")]
        public bool IzvodIzMaticneKnjigeRodjenih { get; set; }

        [Display(Name = "Uverenje o državljanstvu")]
        public bool UverenjeODrzavljanstvu { get; set; }

        [Display(Name = "Dokaz o prebivalištu")]
        public bool DokazOPrebivalistu { get; set; }

        [Display(Name = "Stara lična karta")]
        public bool StaraLicnaKarta { get; set; }

        [Display(Name = "Dokaz o uplati takse")]
        public bool DokazOUplatiTakse { get; set; }

        [Display(Name = "Fotografija")]
        public bool Fotografija { get; set; }

        [Display(Name = "Saglasnost roditelja/staratelja")]
        public bool SaglasnostRoditelja { get; set; }

        [Display(Name = "Napomena")]
        [StringLength(250, ErrorMessage = "Napomena može imati najviše 250 karaktera.")]
        public string Napomena { get; set; }
    }
}