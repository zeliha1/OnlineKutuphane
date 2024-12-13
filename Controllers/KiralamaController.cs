using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineKutuphane.Models;
using OnlineKutuphane.Utility;

namespace OnlineKutuphane.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class KiralamaController : Controller
    {
		private readonly IKiralamaRepository _kiralamaRepository;
		private readonly IKitapRepository _kitapRepository;
		public readonly IWebHostEnvironment _webHostEnvironment;

		public KiralamaController(IKiralamaRepository kiralamaRepository, IKitapRepository kitapRepository, IWebHostEnvironment webHostEnvironment)
		{
			_kiralamaRepository = kiralamaRepository;
			_kitapRepository = kitapRepository;
			_webHostEnvironment = webHostEnvironment;
		}

		public IActionResult Index()
		{
			List<Kiralama> objKiralamaList = _kiralamaRepository.GetAll(includeProps:"Kitap").ToList();                                                 //controllerın veri tabanından veri çekmesi
			return View(objKiralamaList);
		}


		public IActionResult EkleGuncelle(int? id)
		{
			IEnumerable<SelectListItem> KitapList = _kitapRepository.GetAll()
				.Select(k => new SelectListItem
				{
					Text = k.KitapAdi,
					Value = k.Id.ToString()

				});
			ViewBag.KitapList = KitapList;

			if (id == null || id == 0)
			{
				return View();
			}
			else
			{
				Kiralama? kiralamaVt = _kiralamaRepository.Get(u => u.Id == id);
				if(kiralamaVt == null)
				{
					return NotFound();
				}
				return View(kiralamaVt);
			}


		}


		[HttpPost]
		public IActionResult EkleGuncelle(Kiralama kiralama)
		{
			if (ModelState.IsValid)
			{

				if (kiralama.Id == 0)
				{
					_kiralamaRepository.Ekle(kiralama);
					TempData["basarili"] = "Yeni Kiralama kaydı başarıyla oluşturuldu!";
				}
				else
				{
					_kiralamaRepository.Guncelle(kiralama);
					TempData["basarili"] = "Kiralama kayıt güncelleme başarılı!";
				}

				_kiralamaRepository.Kaydet();                                                                            // SaveChanges() yapmazsanız bilgiler veri tabanına eklenmez!
				return RedirectToAction("Index", "Kiralama");
			}
			return View();
		}



		/*
		public IActionResult Guncelle(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Kitap? kitapVt = _kitapRepository.Get(u => u.Id == id);                     // Expression<Func<T, bool>> filtre
			if (kitapVt == null)
			{
				return NotFound();
			}
			return View(kitapVt);
		}*/
		/*
		[HttpPost]
		public IActionResult Guncelle(Kitap kitap)
		{
			if (ModelState.IsValid)
			{
				_kitapRepository.Guncelle(kitap);
				_kitapRepository.Kaydet();                                             // SaveChanges() yapmazsanız bilgiler veri tabanına eklenmez!
				TempData["basarili"] = "Yeni Kitap başarıyla güncellendi!";
				return RedirectToAction("Index", "Kitap");
			}
			return View();
		}*/


		// GET ACTION
		public IActionResult Sil(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Kiralama? kiralamaVt = _kiralamaRepository.Get(u => u.Id == id);
			if (kiralamaVt == null)
			{
				return NotFound();
			}
			return View(kiralamaVt);
		}


		[HttpPost, ActionName("Sil")]
		public IActionResult SilPOST(int? id)
		{
			Kiralama? kiralama = _kiralamaRepository.Get(u => u.Id == id);
			if (kiralama == null)
			{
				return NotFound();
			}
			_kiralamaRepository.Sil(kiralama);
			_kiralamaRepository.Kaydet();
			TempData["basarili"] = "Kayıt Silme işlemi başarılı!";
			return RedirectToAction("Index", "Kiralama");
		}
	}
}
