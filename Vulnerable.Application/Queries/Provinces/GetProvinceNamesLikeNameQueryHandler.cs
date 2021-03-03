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

using System.Linq;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Vulnerable.Domain.Contracts.Query;
using Vulnerable.Domain.Queries;
using Vulnerable.Domain.Queries.Provinces;
using Vulnerable.Shared;

namespace Vulnerable.Application.Queries.Provinces
{
    public sealed class GetProvinceNamesLikeNameQueryHandler : IRequestHandler<GetProvinceNamesLikeNameQuery, PagedNameViewModel>
    {
        private readonly IProvinceRepository _repository;

        /// <summary>
        /// Instantiates a new instance of the <see cref="GetProvinceNamesLikeNameQueryHandler"/> class.
        /// </summary>
        public GetProvinceNamesLikeNameQueryHandler(IProvinceRepository repository)
        {
            _repository = repository ?? throw new System.ArgumentNullException(nameof(repository));
        }

        /// <inheritdoc/>
        public Task<PagedNameViewModel> Handle(GetProvinceNamesLikeNameQuery request, CancellationToken cancellationToken)
        {
            GuardAgainst.NullOrEmpty(request.Name, "name");
            GuardAgainst.LessThanOrEqualToZero(request.PageNumber, "pageNumber");
            GuardAgainst.LessThanOrEqualToZero(request.PageSize, "pageSize");

            var pageNumber = request.PageNumber;
            var pageSize = request.PageSize;
            return _repository.GetProvinceNamesLikeName(request.Name, pageNumber, pageSize)
                .ContinueWith(fetchTask =>
                {
                    GuardAgainst.FaultedOrCancelled(fetchTask);
                    var countTask = _repository.GetTotalCountOfProvinceNamesLikeName(request.Name);
                    countTask.Wait(cancellationToken);

                    return new PagedNameViewModel
                    {
                        Count = countTask.Result,
                        PageNumber = pageNumber,
                        PageSize = pageSize,
                        Items = fetchTask.Result.ToList()
                    };
                }, cancellationToken);

        }
    }
}
