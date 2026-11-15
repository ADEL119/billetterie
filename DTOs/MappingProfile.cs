using AutoMapper;
using BilletterieAPI.Models;


namespace BilletterieAPI.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Achat, AchatDTO>()
                .ForMember(dest => dest.TypeBilletLibelle, opt => opt.MapFrom(src => src.TypeBillet.Libelle))
                .ForMember(dest => dest.SpectateurNom, opt => opt.MapFrom(src => src.Spectateur.Nom))
                .ForMember(dest => dest.SpectateurPrenom, opt => opt.MapFrom(src => src.Spectateur.Prenom));

            CreateMap<Evenement, EvenementDTO>()
                .ForMember(dest => dest.LieuNom, opt => opt.MapFrom(src => src.Lieu.Nom));

            CreateMap<TypeBillet, TypeBilletDTO>()
                .ForMember(dest => dest.EvenementTitre, opt => opt.MapFrom(src => src.Evenement.Titre));

            
            CreateMap<Lieu, LieuDTO>();
            CreateMap<Spectateur, SpectateurDTO>();
        }
    }
}