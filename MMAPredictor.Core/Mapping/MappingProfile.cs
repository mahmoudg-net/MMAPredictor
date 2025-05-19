using AutoMapper;
using MMAPredictor.Core.DTO;
using MMAPredictor.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMAPredictor.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FighterDTO, Fighter>().ReverseMap();
        }
    }
}
