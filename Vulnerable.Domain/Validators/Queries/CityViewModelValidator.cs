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

using FluentValidation;
using Vulnerable.Domain.Queries.Cities;

namespace Vulnerable.Domain.Validators.Queries
{
    public sealed class CityViewModelValidator : AbstractValidator<CityViewModel>
    {
        public CityViewModelValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .WithMessage("Id must be greater than zero");

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Name cannot be empty");

            RuleFor(c => c.Name)
                .Must(p => p is {Length: < 100})
                .WithMessage("Name length must be less than 100");

            RuleFor(c => c.ProvinceName)
                .NotEmpty()
                .WithMessage("Province name cannot be empty");
            RuleFor(c => c.CountryName)
                .NotEmpty()
                .WithMessage("Country name cannot be empty");
        }
    }
}
