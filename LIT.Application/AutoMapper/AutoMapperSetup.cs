using AutoMapper;
using LIT.Application.ViewModels;
using LIT.Domain.Entities;

namespace LIT.Application.AutoMapper
{
    public class AutoMapperSetup : Profile
    {
        public AutoMapperSetup()
        {
            CreateMap<ProductViewModel, Product>().ReverseMap();
            CreateMap<Category, CategoryViewModel>().ReverseMap();
        }
    }
}
