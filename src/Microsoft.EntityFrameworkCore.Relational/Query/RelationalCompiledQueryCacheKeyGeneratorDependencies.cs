// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.EntityFrameworkCore.Query
{
    /// <summary>
    ///     <para>
    ///         Service dependencies parameter class for <see cref="RelationalCompiledQueryCacheKeyGenerator" />
    ///     </para>
    ///     <para>
    ///         This type is typically used by database providers (and other extensions). It is generally
    ///         not used in application code.
    ///     </para>
    /// </summary>
    public sealed class RelationalCompiledQueryCacheKeyGeneratorDependencies
    {
        /// <summary>
        ///     <para>
        ///         Creates the service dependencies parameter object for a <see cref="RelationalCompiledQueryCacheKeyGenerator" />.
        ///     </para>
        ///     <para>
        ///         This type is typically used by database providers (and other extensions). It is generally
        ///         not used in application code.
        ///     </para>
        ///     <para>
        ///         Do not call this constructor directly from provider or application code as it may change
        ///         as new dependencies are added. Use the 'With...' methods instead.
        ///     </para>
        /// </summary>
        /// <param name="contextOptions"> Options for the current <see cref="DbContext" /> instance. </param>
        public RelationalCompiledQueryCacheKeyGeneratorDependencies([NotNull] IDbContextOptions contextOptions)
        {
            Check.NotNull(contextOptions, nameof(contextOptions));

            ContextOptions = contextOptions;
        }

        /// <summary>
        ///     Options for the current <see cref="DbContext" /> instance.
        /// </summary>
        public IDbContextOptions ContextOptions { get; }
    }
}
