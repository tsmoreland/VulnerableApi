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

using FluentValidation;
using Vulnerable.Domain.Commands.Provinces;

namespace Vulnerable.Domain.Validators.Commands
{
    public sealed class ProvinceCreateModelValidator : AbstractValidator<ProvinceCreateModel>
    {
        public ProvinceCreateModelValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("Name cannot be empty");

            RuleFor(p => p.Name)
                .Must(p => p is {Length: < 100})
                .WithMessage("Name length must be less than 100");

            RuleFor(p => p.CountryId)
                .Must(value => value > 0)
                .WithMessage("Country is required");
        }
    }
}
