// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Utilities;
using Microsoft.Extensions.Logging;

namespace Microsoft.EntityFrameworkCore.Storage
{
    /// <summary>
    ///     <para>
    ///         Service dependencies parameter class for <see cref="ExecutionStrategyContext" />
    ///     </para>
    ///     <para>
    ///         This type is typically used by database providers (and other extensions). It is generally
    ///         not used in application code.
    ///     </para>
    /// </summary>
    public sealed class ExecutionStrategyContextDependencies
    {
        /// <summary>
        ///     <para>
        ///         Creates the service dependencies parameter object for a <see cref="ExecutionStrategyContext" />.
        ///     </para>
        ///     <para>
        ///         Do not call this constructor directly from provider or application code as it may change
        ///         as new dependencies are added. Use the 'With...' methods instead.
        ///     </para>
        /// </summary>
        /// <param name="currentDbContext"> Indirection to the current <see cref="DbContext" /> instance. </param>
        /// <param name="options"> The options for the current <see cref="DbContext" /> instance. </param>
        /// <param name="logger"> A logger.</param>
        public ExecutionStrategyContextDependencies(
            [NotNull] ICurrentDbContext currentDbContext,
            [CanBeNull] IDbContextOptions options,
            [CanBeNull] ILogger<IExecutionStrategy> logger)
        {
            Check.NotNull(currentDbContext, nameof(currentDbContext));

            Options = options;
            CurrentDbContext = currentDbContext;
            Logger = logger;
        }

        /// <summary>
        ///     The options for the current <see cref="DbContext" /> instance.
        /// </summary>
        public IDbContextOptions Options { get; }

        /// <summary>
        ///     Indirection to the current <see cref="DbContext" /> instance.
        /// </summary>
        public ICurrentDbContext CurrentDbContext { get; }

        /// <summary>
        ///     The logger.
        /// </summary>
        public ILogger<IExecutionStrategy> Logger { get; }
    }
}
