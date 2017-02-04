// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Utilities;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Microsoft.EntityFrameworkCore.Query
{
    /// <summary>
    ///     <para>
    ///         Service dependencies parameter class for <see cref="QueryContext" />
    ///     </para>
    ///     <para>
    ///         This type is typically used by database providers (and other extensions). It is generally
    ///         not used in application code.
    ///     </para>
    /// </summary>
    public sealed class QueryContextDependencies
    {
        /// <summary>
        ///     <para>
        ///         Creates the service dependencies parameter object for a <see cref="QueryContext" />.
        ///     </para>
        ///     <para>
        ///         Do not call this constructor directly from provider or application code as it may change
        ///         as new dependencies are added. Use the 'With...' methods instead.
        ///     </para>
        ///     <para>
        ///         This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///         directly from your code. This API may change or be removed in future releases.
        ///     </para>
        /// </summary>
        public QueryContextDependencies(
            [NotNull] ICurrentDbContext currentContext,
            [NotNull] IConcurrencyDetector concurrencyDetector)
        {
            Check.NotNull(currentContext, nameof(currentContext));
            Check.NotNull(concurrencyDetector, nameof(concurrencyDetector));

            StateManager = new LazyRef<IStateManager>(() => currentContext.Context.GetService<IStateManager>());
            ChangeDetector = new LazyRef<IChangeDetector>(() => currentContext.Context.GetService<IChangeDetector>());

            ConcurrencyDetector = concurrencyDetector;
        }

        /// <summary>
        ///     The cache being used to store value generator instances.
        /// </summary>
        public IValueGeneratorCache Cache { get; }

        /// <summary>
        ///     Gets the change detector.
        /// </summary>
        public LazyRef<IChangeDetector> ChangeDetector { get; }

        /// <summary>
        ///     Gets the state manager.
        /// </summary>
        public LazyRef<IStateManager> StateManager { get; }

        /// <summary>
        ///     Gets the concurrency detector.
        /// </summary>
        public IConcurrencyDetector ConcurrencyDetector { get; }

    }
}
