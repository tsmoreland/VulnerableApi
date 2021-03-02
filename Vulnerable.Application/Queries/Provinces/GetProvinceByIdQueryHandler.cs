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

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Vulnerable.Domain.Contracts.Data;
using Vulnerable.Domain.Queries.Provinces;
using Vulnerable.Shared;
using Vulnerable.Shared.Exceptions;

namespace Vulnerable.Application.Queries.Provinces
{
    public sealed class GetProvinceByIdQueryHandler : IRequestHandler<GetProvinceByIdQuery, ProvinceViewModel>
    {
        private readonly IProvinceRepository _repository;
        private readonly IMapper _mapper;

        public GetProvinceByIdQueryHandler(IProvinceRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new System.ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
        }

        /// <inheritdoc/>
        public Task<ProvinceViewModel> Handle(GetProvinceByIdQuery request, CancellationToken cancellationToken)
        {
            var requestId = request.Id;
            return _repository.GetProvinceById(requestId)
                .ContinueWith(t =>
                {
                    GuardAgainst.FaultedOrCancelled(t);
                    var model = t.Result;
                    if (model == null)
                        throw new NotFoundException($"{nameof(requestId)} not found");
                    return _mapper.Map<ProvinceViewModel>(model);
                }, cancellationToken);
        }
    }
}
