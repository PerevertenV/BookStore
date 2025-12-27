using AutoMapper;
using DataAccess.Data;
using DataAccess.Entity;
using DataAccess.Models;
using DataAccess.Repository.IRepository;

namespace DataAccess.Repository;
public class CategoryRepository(AplicationDBContextcs db, IMapper mapper) : Repository<CategoryEntity, Category>(db, mapper), ICategoryRepository;
