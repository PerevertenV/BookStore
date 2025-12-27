using AutoMapper;
using DataAccess.Data;
using DataAccess.Entity;
using DataAccess.Repository.IRepository;
using DataAccess.Models;

namespace DataAccess.Repository;
public class ProductRepository(AplicationDBContextcs db, IMapper mapper) : Repository<ProductEntity, Product>(db, mapper), IProductRepository;