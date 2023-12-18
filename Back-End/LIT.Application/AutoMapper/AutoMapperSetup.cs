using AutoMapper;
using LIT.Application.ViewModels;
using LIT.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace LIT.Application.AutoMapper
{
    [ExcludeFromCodeCoverage]
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
