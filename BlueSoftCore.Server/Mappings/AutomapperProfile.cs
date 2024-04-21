using AutoMapper;
using BlueSoftCore.Server.DTO;
using BlueSoftCore.Server.Models;

namespace BlueSoftCore.Server.Mappings
{
    public class AutomapperProfile: Profile
    {
        public AutomapperProfile() {

            CreateMap<ClientDTO, Client>()
            .ForMember(dest => dest.ClientId, opt => opt.Ignore());

            CreateMap<AccountDTO, Account>()
            .ForMember(dest => dest.AccountId, opt => opt.Ignore());
        }
    }
}
