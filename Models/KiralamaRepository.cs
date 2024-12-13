using OnlineKutuphane.Utility;
using System.Linq.Expressions;

namespace OnlineKutuphane.Models
{
	public class KiralamaRepository : Repository<Kiralama>, IKiralamaRepository
	{
		private KutuphaneDbContext _kutuphaneDbContext;
		public KiralamaRepository(KutuphaneDbContext kutuphaneDbContext) : base(kutuphaneDbContext)  //dependency injecsin
		{
			_kutuphaneDbContext = kutuphaneDbContext;
		}

		public void Guncelle(Kiralama kiralama)
		{
			_kutuphaneDbContext.Update(kiralama);
		}

		public void Kaydet()
		{
			_kutuphaneDbContext.SaveChanges();
		}
	}
}
