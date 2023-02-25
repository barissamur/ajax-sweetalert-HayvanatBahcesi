using System.ComponentModel.DataAnnotations;

namespace Odev27.Models
{
    public class HayvanViewModel
    {
        [Required(ErrorMessage ="Ad alanı zorunlu")]
        public string Ad { get; set; } = null!;

        [Required(ErrorMessage ="Resim alanı zorunlu")]
        public IFormFile Resim { get; set; } = null!;

        public List<Hayvan> Hayvanlar { get; set; } = new();
    }
}
