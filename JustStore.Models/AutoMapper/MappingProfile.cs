using AutoMapper;
using DataAccess.Entity;
using JustStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustStore.Models.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {

            CreateMap<CategoryEntity, Category>();
            CreateMap<Category, CategoryEntity>();

            CreateMap<CompanyEntity, Company>();
            CreateMap<Company, CompanyEntity>();

            CreateMap<OrderDetailsEntity, OrderDetail>();
            CreateMap<OrderDetail, OrderDetailsEntity>();

            CreateMap<OrderHeaderEntity, OrderHeader>();
            CreateMap<OrderHeader, OrderHeaderEntity>();

            CreateMap<ProductEntity, Product>();
            CreateMap<Product, ProductEntity>();

            CreateMap<ProductImagesEntity, ProductImage>();
            CreateMap<ProductImage, ProductImagesEntity>();

            CreateMap<ShoppingCartEntity, ShoppingCart>();
            CreateMap<ShoppingCart, ShoppingCartEntity>();
        }
    }
}
