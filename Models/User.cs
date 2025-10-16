using System.ComponentModel.DataAnnotations;

namespace SimpleApi.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nama pengguna wajib diisi.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email wajib diisi.")]
        [EmailAddress(ErrorMessage = "Format email tidak valid.")]
        public string Email { get; set; }
    }
}