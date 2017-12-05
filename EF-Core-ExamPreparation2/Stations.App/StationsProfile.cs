﻿using System.Linq;
using AutoMapper;

namespace Stations.App
{
    using DataProcessor.Dto.Import;
    using Models;

    public class StationsProfile : Profile
	{
		// Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
		public StationsProfile()
		{
		    CreateMap<StationDto, Station>();
		    CreateMap<SeatingClassDto, SeatingClass>();

        }
    }
}
