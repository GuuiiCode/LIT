using AutoMapper;
using LIT.Application.ViewModels;
using LIT.Domain.Entities;
using LIT.Domain.ValueObjects;
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

            CreateMap<UserViewModel, User>()
                .ConstructUsing(src => new User(
                    new UserName(src.Name ?? ""),
                    new Password(src.Password)));

            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName.Value))
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<User, LoginResultViewModel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName.Value));

        }
    }
}
