// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal
{
    /// <summary>
    ///     <para>
    ///         Service dependencies parameter class for <see cref="RelationalConventionSetBuilder" />
    ///     </para>
    ///     <para>
    ///         This type is typically used by database providers (and other extensions). It is generally
    ///         not used in application code.
    ///     </para>
    ///     <para>
    ///         This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///         directly from your code. This API may change or be removed in future releases.
    ///     </para>
    /// </summary>
    public sealed class RelationalConventionSetBuilderDependencies
    {
        /// <summary>
        ///     <para>
        ///         Creates the service dependencies parameter object for a <see cref="RelationalConventionSetBuilder" />.
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
        public RelationalConventionSetBuilderDependencies(
            [NotNull] IRelationalTypeMapper typeMapper,
            [CanBeNull] ICurrentDbContext currentContext,
            [CanBeNull] IDbSetFinder setFinder)
        {
            Check.NotNull(typeMapper, nameof(typeMapper));

            TypeMapper = typeMapper;
            Context = currentContext;
            SetFinder = setFinder;
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IRelationalTypeMapper TypeMapper { get; }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public ICurrentDbContext Context { get; }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IDbSetFinder SetFinder { get; }
    }
}
