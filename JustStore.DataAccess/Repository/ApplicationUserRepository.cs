using AutoMapper;
using DataAccess.Data;
using DataAccess.Entity;
using DataAccess.Repository.IRepository;

namespace DataAccess.Repository;
public class ApplicationUserRepository(AplicationDBContextcs db, IMapper mapper) : Repository<ApplicationUser, ApplicationUser>(db, mapper), IApplicationUserRepository;
