using BlockChain.DAL.Entities;
using BlockChain.Model.Modelss;
using AutoMapper;
using BlockChain.Model.Models;
namespace BlockChain.BAL
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<UserCreateDto, User>()
          .ForMember(dest => dest.PublicKey, opt => opt.Ignore())
          .ForMember(dest => dest.PrivateKey, opt => opt.Ignore());

            CreateMap<User, UserReadDto>();
        }

    }
}
