﻿//
// Copyright © 2021 Terry Moreland
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), 
// to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
// and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// 

using AutoMapper;
using Vulnerable.Domain.Entities;
using Commands = Vulnerable.Domain.Commands;
using Queries = Vulnerable.Domain.Queries;

namespace Vulnerable.Infrastructure.Profiles
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<City, Queries.Cities.CityViewModel>()
                .ForMember(c =>
                    c.ProvinceName, opt => opt.MapFrom((source, _) => source.Province?.Name))
                .ForMember(c =>
                    c.CountryName, opt => opt.MapFrom((source, _) => source.Country?.Name));

            CreateMap<Province, Queries.Provinces.ProvinceViewModel>()
                .ForMember(c =>
                    c.CountryName, opt => opt.MapFrom((source, _) => source.Country?.Name));

            CreateMap<Country, Queries.Countries.CountryViewModel>()
                .ForMember(c =>
                    c.ContinentName, opt => opt.MapFrom((source, _) => source.Continent?.Name));

            CreateMap<(int Id, string Name), Queries.IdNameViewModel>()
                .ForMember(m => m.Id, opt => opt.MapFrom((source, _) => source.Id))
                .ForMember(m => m.Name, opt => opt.MapFrom((source, _) => source.Name));

            CreateMap<Commands.Cities.CityCreateModel, City>();

            CreateMap<Continent, Queries.Continents.ContinentViewModel>();
        }
    }
}
