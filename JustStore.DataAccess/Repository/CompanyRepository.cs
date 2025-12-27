using AutoMapper;
using DataAccess.Data;
using DataAccess.Entity;
using DataAccess.Models;
using DataAccess.Repository.IRepository;

namespace DataAccess.Repository;
public class CompanyRepository(AplicationDBContextcs db, IMapper mapper) : Repository<CompanyEntity, Company>(db, mapper), ICompanyRepository;