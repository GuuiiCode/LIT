using AutoMapper;
using LIT.Application.ViewModels;
using LIT.Domain.Entities;

namespace LIT.Application.AutoMapper
{
    public class AutoMapperSetup : Profile
    {
        public AutoMapperSetup()
        {
            CreateMap<BaseProductViewModel, Product>().ReverseMap();
            CreateMap<ProductViewModel, Product>().ReverseMap();

            CreateMap<BaseCategoryViewModel, Category>().ReverseMap();
            CreateMap<CategoryViewModel, Category>().ReverseMap();
        }
    }
}
