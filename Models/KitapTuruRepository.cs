using OnlineKutuphane.Utility;
using System.Linq.Expressions;

namespace OnlineKutuphane.Models
{
	public class KitapTuruRepository : Repository<KitapTuru>, IKitapTuruRepository
	{
		private KutuphaneDbContext _kutuphaneDbContext;
		public KitapTuruRepository(KutuphaneDbContext kutuphaneDbContext) : base(kutuphaneDbContext)  //dependency injecsin
		{
			_kutuphaneDbContext = kutuphaneDbContext;
		}

		public void Guncelle(KitapTuru kitapTuru)
		{
			_kutuphaneDbContext.Update(kitapTuru);
		}

		public void Kaydet()
		{
			_kutuphaneDbContext.SaveChanges();
		}
	}
}
