using System.ComponentModel.DataAnnotations;

namespace LicnaKarta.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string KorisnickoIme { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Sifra { get; set; }
    }
}