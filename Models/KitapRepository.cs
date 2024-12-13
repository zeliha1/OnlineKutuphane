using OnlineKutuphane.Utility;
using System.Linq.Expressions;

namespace OnlineKutuphane.Models
{
	public class KitapRepository : Repository<Kitap>, IKitapRepository
	{
		private KutuphaneDbContext _kutuphaneDbContext;
		public KitapRepository(KutuphaneDbContext kutuphaneDbContext) : base(kutuphaneDbContext)  //dependency injecsin
		{
			_kutuphaneDbContext = kutuphaneDbContext;
		}

		public void Guncelle(Kitap kitap)
		{
			_kutuphaneDbContext.Update(kitap);
		}

		public void Kaydet()
		{
			_kutuphaneDbContext.SaveChanges();
		}
	}
}
