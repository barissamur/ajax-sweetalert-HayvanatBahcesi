using Microsoft.AspNetCore.Mvc;
using Odev27.Models;

namespace Odev27.ViewComponents
{
    public class ListeleViewComponent : ViewComponent
    {
        private readonly UygulamaDbContext _db;

        public ListeleViewComponent(UygulamaDbContext db)
        {
            _db=db;
        }

        public IViewComponentResult Invoke()
        {
            var vm = new HayvanViewModel()
            {
                Hayvanlar = _db.Hayvanlar.ToList()
            };
            return View(vm);
        }
    }
}
