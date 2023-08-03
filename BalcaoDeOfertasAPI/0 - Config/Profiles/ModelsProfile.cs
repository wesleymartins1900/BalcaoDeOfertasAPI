using AutoMapper;
using DtosBalcaoDeOfertas.InputDTO;
using BalcaoDeOfertasAPI._1___Models;

namespace BalcaoDeOfertasAPI._0___Config.Profiles
{
    public class ModelsProfile : Profile
    {
        public ModelsProfile()
        {
            CreateMap<OfertaInputDTO, Oferta>();
        }
    }
}