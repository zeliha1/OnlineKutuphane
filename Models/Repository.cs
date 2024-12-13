using Microsoft.EntityFrameworkCore;
using OnlineKutuphane.Utility;
using System.Linq.Expressions;


namespace OnlineKutuphane.Models
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly KutuphaneDbContext _kutuphaneDbContext;
		internal DbSet<T> dbSet;

		public Repository(KutuphaneDbContext kutuphaneDbContext)
		{
			_kutuphaneDbContext = kutuphaneDbContext;
			this.dbSet = _kutuphaneDbContext.Set<T>();
			
		}

		public void Ekle(T entity)
		{
			dbSet.Add(entity);
		}

		public T Get(Expression<Func<T, bool>> filtre, string? includeProps = null)
		{
			IQueryable<T> sorgu = dbSet;
			sorgu = sorgu.Where(filtre);

			return sorgu.FirstOrDefault();
		}

		public IEnumerable<T> GetAll(string? includeProps = null)
		{
			IQueryable<T> sorgu = dbSet;
			return sorgu.ToList();
		}

		public void Sil(T entity)
		{
			dbSet.Remove(entity);
		}

		public void SilAralik(IEnumerable<T> entities)
		{
			dbSet.RemoveRange(entities);
		}
	}
}
