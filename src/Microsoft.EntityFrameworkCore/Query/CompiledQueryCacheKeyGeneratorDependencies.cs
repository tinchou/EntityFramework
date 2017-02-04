// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.EntityFrameworkCore.Query
{
    /// <summary>
    ///     <para>
    ///         Service dependencies parameter class for <see cref="CompiledQueryCacheKeyGenerator" />
    ///     </para>
    ///     <para>
    ///         This type is typically used by database providers (and other extensions). It is generally
    ///         not used in application code.
    ///     </para>
    /// </summary>
    public sealed class CompiledQueryCacheKeyGeneratorDependencies
    {
        /// <summary>
        ///     <para>
        ///         Creates the service dependencies parameter object for a <see cref="CompiledQueryCacheKeyGenerator" />.
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
        /// <param name="model"> The model that queries will be written against. </param>
        /// <param name="currentContext"> The context that queries will be executed for. </param>
        public CompiledQueryCacheKeyGeneratorDependencies([NotNull] IModel model, [NotNull] ICurrentDbContext currentContext)
        {
            Check.NotNull(model, nameof(model));
            Check.NotNull(currentContext, nameof(currentContext));

            Model = model;
            Context = currentContext;
        }

        /// <summary>
        ///     The model that queries will be written against.
        /// </summary>
        public IModel Model { get; }

        /// <summary>
        ///     The context that queries will be executed for.
        /// </summary>
        public ICurrentDbContext Context { get; }
    }
}
