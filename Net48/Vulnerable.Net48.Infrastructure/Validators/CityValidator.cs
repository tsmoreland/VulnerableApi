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

using FluentValidation;
using Vulnerable.Domain.Entities;

namespace Vulnerable.Net48.Infrastructure.Validators
{
    public sealed class CityValidator : AbstractValidator<City>
    {
        public CityValidator()
        {
            RuleFor(c => c.Name)
                .Must(name => name is {Length: > 0})
                .WithMessage("Name cannot be empty");
            RuleFor(c => c.Name)
                .Must(name => name.Length < 100)
                .WithMessage("Name must be less than 100 characters");
            RuleFor(c => c.Province)
                .Must(e => e != null)
                .WithMessage("City requires a province");
            RuleFor(c => c.Country)
                .Must(e => e != null)
                .WithMessage("City requires a country");

            RuleFor(c => c.ProvinceId)
                .Must(e => e != null)
                .WithMessage("City requires a province");
            RuleFor(c => c.CountryId)
                .Must(e => e != null)
                .WithMessage("City requires a country");


        }
    }
}
