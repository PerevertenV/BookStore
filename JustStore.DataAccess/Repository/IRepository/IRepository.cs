using System.Linq.Expressions;

namespace DataAccess.Repository.IRepository
{
	public interface IRepository<T, M> where T : class
	{
		IEnumerable<M?> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);

		M? GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null);

		void Add(M obj);

		void Delete(M obj);

		void DeleteRange(IEnumerable<M> objs);

		void Update(M item);

    }
}
