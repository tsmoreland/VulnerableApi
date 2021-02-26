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
using System.Threading.Tasks;

namespace Vulnerable.Shared
{
    public static class GuardAgainst
    {
        /// <summary>
        /// throws <see cref="ArgumentException"/> if <paramref name="value"/>
        /// is null or empty
        /// </summary>
        public static void NullOrEmpty(string value, string parameterName)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException($"invalid {parameterName}, string cannot be null or empty", parameterName);
        }

        /// <summary>
        /// throws <see cref="ArgumentException"/> if <paramref name="value"/> is less
        /// than or equal to zero
        /// </summary>
        /// <param name="value"></param>
        /// <param name="parameterName"></param>
        public static void LessThanOrEqualToZero(int value, string parameterName)
        {
            if (value <= 0)
                throw new ArgumentException($"{parameterName} has invalid value {value}", parameterName);
        }

        /// <summary>
        /// throws <see cref="ArgumentNullException"/> if <paramref name="value"/> is null
        /// </summary>
        public static void Null<T>(T? value, string parameterName)
        {
            if (value == null)
                throw new ArgumentNullException($"{parameterName} is requried");
        }

        /// <summary>
        /// throws <see cref="ArgumentException"/> or <see cref="AggregateException"/>
        /// if <paramref name="task"/> is cancelled or fault
        /// </summary>
        /// <remarks>
        /// throws <see cref="AggregateException"/> if <see cref="Task.Exception"/> is
        /// non-null
        /// </remarks>
        public static void FaultedOrCancelled(Task task)
        {
            if (task.IsFaulted || task.IsCanceled)
                throw task.Exception ?? (Exception) new ArgumentException($"Operation was cancelled", nameof(task));
        }
    }
}
