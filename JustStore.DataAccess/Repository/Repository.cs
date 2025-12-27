using AutoMapper;
using DataAccess.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using DataAccess.Repository.IRepository;

namespace DataAccess.Repository
{
	public class Repository<T, M> : IRepository<T, M> where T : class
	{
		private readonly AplicationDBContextcs _db;
		private readonly IMapper _mapper;
		internal DbSet<T> dbSet;

		public Repository(AplicationDBContextcs db, IMapper mapper) 
		{
			_db = db;
			_mapper = mapper;
			this.dbSet = _db.Set<T>();
			_db.Products.Include(u => u.Category).Include(u=>u.CategoryId);
		}

		public void Add(M entity)
		{
			dbSet.Add(_mapper.Map<T>(entity));
            _db.SaveChanges();
        }

		public void Delete(M entity)
		{
			dbSet.Remove(_mapper.Map<T>(entity));
            _db.SaveChanges();
        }

        public void DeleteRange(IEnumerable<M> entities)
		{
			dbSet.RemoveRange(_mapper.Map<IEnumerable<T>>(entities));
            _db.SaveChanges();
        }

        public IEnumerable<M?> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = this.dbSet;

            query = query.AsNoTracking();

            query = filter != null ? query.Where(filter) : query;

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            var list = query.ToList();

            return _mapper.Map<IEnumerable<M>>(list);
        }

        public M? GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = this.dbSet;

            query = filter != null ? query.Where(filter) : query;

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            var obj = query.FirstOrDefault();

            return _mapper.Map<M?>(obj);
        }

        public void Update(M item)
        {
            var keyValue = typeof(M).GetProperty("Id")?.GetValue(item); 
            
            if (keyValue == null) throw new InvalidOperationException($"Entity was not found");

            var existingEntity = this.dbSet.Find(keyValue);

            if (existingEntity != null) { _mapper.Map(item, existingEntity);  _db.SaveChanges(); }
        }
    }
}

