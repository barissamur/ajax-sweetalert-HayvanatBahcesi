namespace Odev27.Models
{
    public class HayvanViewModel
    {
        public string Ad { get; set; } = null!;
        public IFormFile Resim { get; set; } = null!;

        public List<Hayvan> Hayvanlar { get; set; } = new();
    }
}
