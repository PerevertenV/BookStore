using AutoMapper;
using DataAccess.Data;
using DataAccess.Entity;
using DataAccess.Repository.IRepository;
using DataAccess.Models;

namespace DataAccess.Repository;
public class OrderDetailRepository(AplicationDBContextcs db, IMapper mapper) : Repository<OrderDetailsEntity, OrderDetail>(db, mapper), IOrderDetailRepository;
