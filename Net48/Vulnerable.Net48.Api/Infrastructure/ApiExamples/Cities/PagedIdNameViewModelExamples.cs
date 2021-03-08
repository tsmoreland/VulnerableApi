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

using System.Collections.Generic;
using Swashbuckle.Examples;
using Vulnerable.Domain.Queries;

namespace Vulnerable.Net48.Api.Infrastructure.ApiExamples.Cities
{
    /// <summary>
    /// Example response of <see cref="PagedIdNameViewModel"/>
    /// </summary>
    public sealed class PagedIdNameViewModelExamples : IExamplesProvider
    {
        /// <inheritdoc/>
        public object GetExamples() =>
            new PagedIdNameViewModel {
                Count = 50,
                PageNumber = 1,
                PageSize = 10,
                Items = new List<IdNameViewModel> 
                {
                    new IdNameViewModel { Id = 34, Name = "Albany" },
                    new IdNameViewModel { Id = 3, Name = "Banff" },
                    new IdNameViewModel { Id = 36, Name = "Boston" },
                    new IdNameViewModel { Id = 35, Name = "Buffalo" },
                    new IdNameViewModel { Id = 4, Name = "Calgary" },
                    new IdNameViewModel { Id = 37, Name = "Camberidge" },
                    new IdNameViewModel { Id = 18, Name = "Camberidge" },
                    new IdNameViewModel { Id = 29, Name = "Charlottetown" },
                    new IdNameViewModel { Id = 32, Name = "Destruction Bay" },
                    new IdNameViewModel { Id = 3, Name = "Edmonton" },
                }
            };
    }
}