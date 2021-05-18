//
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
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vulnerable.Domain.Commands.Cities;
using Vulnerable.Domain.Contracts.Commands;

namespace Vulnerable.Application.Commands.Cities
{
    public sealed class DeleteCityCommandHandler : IRequestHandler<DeleteCityComamnd, bool>
    {
        private readonly ICityUnitOfWorkFactory _unitOfWorkFactory;

        public DeleteCityCommandHandler(ICityUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory ?? throw new ArgumentNullException(nameof(unitOfWorkFactory));
        }

        public async Task<bool> Handle(DeleteCityComamnd request, CancellationToken cancellationToken)
        {
            #if NET5_0_OR_GREATER
            await using var unitOfWork = _unitOfWorkFactory.Create();
            #else
            using var unitOfWork = _unitOfWorkFactory.Create();
            #endif

            await unitOfWork.Delete(request.Id, cancellationToken);
            await unitOfWork.Commit(cancellationToken);

            return true;
        }
    }
}
