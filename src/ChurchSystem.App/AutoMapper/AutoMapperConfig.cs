using AutoMapper;
using Dotnet.Portal.App.ViewsModels;
using Dotnet.Portal.Domain.Models;

namespace Dotnet.Portal.App.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Member, MemberViewModel>().ReverseMap();
            CreateMap<Donation, DonationViewModel>().ReverseMap();
            CreateMap<Group, GroupViewModel>().ReverseMap();
            CreateMap<Role, RoleViewModel>().ReverseMap();
        }
    }
}
