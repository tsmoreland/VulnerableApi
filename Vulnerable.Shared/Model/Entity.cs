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

using System.Collections.Generic;

namespace Moreland.Vulnerable.Shared.Model
{
    public abstract class Entity : IEqualityComparer<Entity>
    {
        protected Entity(int id)
        {
            Id = id;
        }
        protected Entity()
        {
        }

        public int Id { get; private set; }

        /// <inheritdoc />
        public override bool Equals(object? obj) =>
            ReferenceEquals(this, obj) || (obj is Entity entity && Equals(this, entity));

        /// <inheritdoc />
        public override int GetHashCode() => GetHashCode(this);

        /// <inheritdoc />
        public bool Equals(Entity? x, Entity? y)
        {
            if (ReferenceEquals(x, y)) 
                return true;
            if (x is null) 
                return false;
            if (y is null) 
                return false;
            if (x.GetType() != y.GetType()) 
                return false;
            return x.Id == y.Id;
        }

        /// <inheritdoc />
        public int GetHashCode(Entity obj) =>
            obj.Id.GetHashCode();

    }
}
