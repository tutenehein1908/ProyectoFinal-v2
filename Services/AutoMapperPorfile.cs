namespace Licencias___PF.Services
{
    using AutoMapper;
    using Licencia___PF.Model;
    using Licencias___PF.DTO;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<LicenciaCreateDto, Licencia>();

            CreateMap<LicenciaCreateDto, Licencia>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); // evita sobreescribir con null
        }
    }

}
