﻿//
// Copyright © 2020 Terry Moreland
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

using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Services;
using MediatR;
using Vulnerable.Domain.Entities;
using Vulnerable.Domain.Queries.Provinces;
using Vulnerable.Shared.Extensions;
using ProvinceCommands = Vulnerable.Domain.Commands.Provinces;
using Commands = Vulnerable.Domain.Commands;
using Queries = Vulnerable.Domain.Queries;

namespace Vulnerable.Net48.Api.Soap
{
    /// <summary>
    /// Summary description for ProvincesService
    /// </summary>
    [WebService(Namespace = "http://vulnerableapp.com/soap/ProvincesService.asmx")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class ProvincesService : WebService
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// instantiates a new instance of the <see cref="ProvincesService"/> class.
        /// </summary>
        public ProvincesService()
        {
            _mediator = DependencyResolver.Current.GetService(typeof(IMediator)) as IMediator ??
                        throw new ConfigurationErrorsException("Unable to load IMediator from IoC container");
        }

        /// <summary>
        /// Adds new province 
        /// </summary>
        /// <param name="province">province to add</param>
        public Commands.CreateResultViewModel<Province> CreateProvince(ProvinceCommands.ProvinceCreateModel province)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get id/name pairs of all provinces
        /// </summary>
        /// <param name="pageNumber">page number used with page size to limit result size</param>
        /// <param name="pageSize">page size used with page number to limit result size</param>
        [WebMethod]
        public Queries.PagedIdNameViewModel GetProvinces(int pageNumber, int pageSize) =>
            _mediator
                .Send(new GetProvincesQuery(pageNumber, pageSize))
                .ResultOrThrow();
        

        /// <summary>
        /// Returns all province names matching <paramref name="name"/> 
        /// </summary>
        /// <param name="name">name to compare against</param>
        /// <param name="pageNumber">page number used with page size to limit result size</param>
        /// <param name="pageSize">page size used with page number to limit result size</param>
        [WebMethod]
        public Queries.PagedNameViewModel GetProvinceNamesLikeName(string name, int pageNumber, int pageSize) =>
            _mediator
                .Send(new GetProvinceNamesLikeNameQuery(name, pageNumber, pageSize))
                .ResultOrThrow();

        /// <summary>
        /// returns province matching <paramref name="id"/>
        /// </summary>
        [WebMethod]
        public ProvinceViewModel GetProvinceById(int id) =>
            _mediator
                .Send(new GetProvinceByIdQuery(id))
                .ResultOrThrow();

        /// <summary>
        /// returns province matching <paramref name="name"/>
        /// </summary>
        [WebMethod]
        public ProvinceViewModel GetProvinceByName(string name) =>
            _mediator
                .Send(new GetProvinceByNameQuery(name))
                .ResultOrThrow();

        /// <summary>
        /// returns all province details for provinces matching country
        /// </summary>
        /// <param name="countryId">unique identifier to match country</param>
        /// <param name="pageNumber">page number used with page size to limit result size</param>
        /// <param name="pageSize">page size used with page number to limit result size</param>
        [WebMethod]
        public Queries.PagedIdNameViewModel GetProvincesByCountryId(int countryId, int pageNumber, int pageSize) =>
            _mediator
                .Send(new GetProvincesByCountryIdQuery(countryId, pageNumber, pageSize))
                .ResultOrThrow();

        /// <summary>
        /// returns all province details for provinces matching country
        /// </summary>
        /// <param name="countryName">unique identifier to match country</param>
        /// <param name="pageNumber">page number used with page size to limit result size</param>
        /// <param name="pageSize">page size used with page number to limit result size</param>
        [WebMethod]
        public Queries.PagedIdNameViewModel GetProvincesByCountryName(string countryName, int pageNumber, int pageSize) =>
            _mediator
                .Send(new GetProvincesByCountryNameQuery(countryName, pageNumber, pageSize))
                .ResultOrThrow();
    }
}
