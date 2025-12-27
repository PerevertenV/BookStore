using AutoMapper;
using DataAccess.Data;
using DataAccess.Entity;
using DataAccess.Repository.IRepository;
using DataAccess.Models;

namespace DataAccess.Repository;
public class ShoppingCartRepository(AplicationDBContextcs db, IMapper mapper) : Repository<ShoppingCartEntity, ShoppingCart>(db, mapper), IShoppingCartRepository;