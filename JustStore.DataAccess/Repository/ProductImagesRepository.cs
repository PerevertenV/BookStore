using AutoMapper;
using DataAccess.Data;
using DataAccess.Entity;
using DataAccess.Repository.IRepository;
using DataAccess.Models;

namespace DataAccess.Repository;
public class ProductImagesRepository(AplicationDBContextcs db, IMapper mapper) : Repository<ProductImagesEntity, ProductImage>(db, mapper), IProductImagesRepository;
