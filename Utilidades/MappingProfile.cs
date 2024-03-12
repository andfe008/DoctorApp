using AutoMapper;
using Model.DTOs;
using Model.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades
{
    public class MappingProfile : Profile
    {

        public MappingProfile() {

            CreateMap<Especialidad, EspecialidadDto>()
               .ForMember(d => d.Estado, m => m.MapFrom(o => o.Estado == true ? 1 : 0));
        
        
        }
    }
}
