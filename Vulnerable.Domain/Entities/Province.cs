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

using System;
using System.Collections.Generic;

namespace Vulnerable.Domain.Entities
{
    public class Province : Entity
    {
        public Province(int id, string name, Country country)
            : base(id)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name cannot be empty", nameof(name));
            if (name.Length > 100)
                throw new ArgumentException("name must be shorter than 100 characters", nameof(name));
            Name = name;
            CountryId = country.Id;
            Country = country;

        }

        // ReSharper disable once UnusedMember.Local - required by entity framework
        private Province()
        {
            Name = string.Empty;
        }

        public string Name { get; private set; }

        public int? CountryId { get; private set; }
        public Country? Country { get; private set; }

        public List<City> Cities { get; private set; } = new ();

        public void SetCountry(Country country)
        {
            Country = country;
            CountryId = country.Id;
        }
    }
}
