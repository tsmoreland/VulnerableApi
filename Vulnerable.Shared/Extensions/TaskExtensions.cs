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

using System;
using System.Threading;
using System.Threading.Tasks;
using Vulnerable.Shared.Exceptions;

namespace Vulnerable.Shared.Extensions
{
    public static class TaskExtensions
    {
        public static TResult ResultOrThrow<TResult>(this Task<TResult> task)
        {
            task.Wait();

            if (task.IsFaulted)
                throw task.Exception ?? (Exception) new InternalServerErrorException("Unknown error occurred");
            if (task.IsCanceled)
                throw new OperationCanceledException("Operation was cancelled");

            return task.Result;
        }

        public static int ResultIfGreaterThanZero(this Task<int> task, CancellationToken cancellationToken)
        {
            task.Wait(cancellationToken);

            GuardAgainst.FaultedOrCancelled(task);
            if (task.Result <= 0)
                throw new NotFoundException("no matches found");

            return task.Result;
        }

        public static Task ContinueOnSuccessOrThrow(this Task task, Action<Task> continuationAction)
        {
            return ContinueOnSuccessOrThrow(task, continuationAction, CancellationToken.None);
        }
        public static Task ContinueOnSuccessOrThrow(this Task task, Action<Task> continuationAction, CancellationToken cancellationToken)
        {
            return task.ContinueWith(t => 
            { 
                GuardAgainst.FaultedOrCancelled(t);
                continuationAction.Invoke(t);
            }, cancellationToken);
        }
        public static Task<TDestination> ContinueOnSuccessOrThrow<TSource, TDestination>(this Task<TSource> task, Func<Task<TSource>, TDestination> continuationAction)
        {
            return ContinueOnSuccessOrThrow(task, continuationAction, CancellationToken.None);
        }
        public static Task<TDestination> ContinueOnSuccessOrThrow<TSource, TDestination>(this Task<TSource> task, Func<Task<TSource>, TDestination> continuationAction, CancellationToken cancellationToken)
        {
            return task.ContinueWith(t => 
            { 
                GuardAgainst.FaultedOrCancelled(t);
                return continuationAction.Invoke(t);
            }, cancellationToken);
        }
        public static Task ContinueOnSuccessOrThrow<TSource>(this Task<TSource> task, Action<Task<TSource>> continuationAction)
        {
            return ContinueOnSuccessOrThrow(task, continuationAction, CancellationToken.None);
        }
        public static Task ContinueOnSuccessOrThrow<TSource>(this Task<TSource> task, Action<Task<TSource>> continuationAction, CancellationToken cancellationToken)
        {
            return task.ContinueWith(t => 
            { 
                GuardAgainst.FaultedOrCancelled(t);
                continuationAction.Invoke(t);
            }, cancellationToken);
        }
    }
}
