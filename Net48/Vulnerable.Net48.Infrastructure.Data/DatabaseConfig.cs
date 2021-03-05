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
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Threading;

namespace Vulnerable.Net48.Infrastructure.Data
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class DatabaseConfig
    {
        public static void RegisterDatabaseReset(Func<Type, object> dependencyResolver)
        {
            if (dependencyResolver(typeof(IDbContextFactory<AddressDbContext>)) is not IDbContextFactory<AddressDbContext> contextFactory)
                throw new KeyNotFoundException("Unable to get IDbContextFactory<AddressDbContext> from DependencyResolver");

            // setup for retry, not in place yet
            var startTime = DateTime.UtcNow;
            var maxTime = TimeSpan.FromMinutes(5); // time for SQL Server to startup

            while ((DateTime.UtcNow - startTime) < maxTime)
            {
                try
                {
                    using var context = contextFactory.Create();

                    bool exists = context.Database.Exists();
                    if (exists)
                        context.Database.Delete();
                    context.Database.CreateIfNotExists();

                    if (!exists)
                        context.Database.Initialize(true);

                    var initializer = new AddressDbInitializer();
                    initializer.ResetDatabaseToSeeded(context);
                    return;
                }
                catch (Exception)
                {
                    // TODO: get logging working
                    Thread.Sleep(5000);
                }
            }
        }
    }
}