//
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
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Vulnerable.Domain.Contracts.Queries;
using Vulnerable.Domain.Entities;
using Vulnerable.Domain.Queries.Continents;
using Vulnerable.Shared;
using Vulnerable.Shared.Exceptions;

namespace Vulnerable.Application.Queries.Continents
{
    public sealed class GetContinentByNameQueryHandler : IRequestHandler<GetContinentByNameQuery, ContinentViewModel>
    {
        private readonly IContinentRepository _repository;
        private readonly IMapper _mapper;

        public GetContinentByNameQueryHandler(IContinentRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public Task<ContinentViewModel> Handle(GetContinentByNameQuery request, CancellationToken cancellationToken)
        {
            string requestName = request.Name;
            return _repository.GetContinentByName(requestName)
                .ContinueWith(fetchTask =>
                {
                    GuardAgainst.FaultedOrCancelled(fetchTask);
                    Continent? model = fetchTask.Result;
                    if (model == null)
                        throw new NotFoundException($"{nameof(requestName)} not found");
                    return _mapper.Map<ContinentViewModel>(model);
                }, cancellationToken);
        }
    }
}
