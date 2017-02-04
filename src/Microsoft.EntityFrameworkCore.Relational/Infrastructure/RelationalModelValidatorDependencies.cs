// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.EntityFrameworkCore.Infrastructure
{
    /// <summary>
    ///     <para>
    ///         Service dependencies parameter class for <see cref="RelationalModelValidator" />
    ///     </para>
    ///     <para>
    ///         This type is typically used by database providers (and other extensions). It is generally
    ///         not used in application code.
    ///     </para>
    /// </summary>
    public sealed class RelationalModelValidatorDependencies
    {
        /// <summary>
        ///     <para>
        ///         Creates the service dependencies parameter object for a <see cref="RelationalModelValidator" />.
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
        /// <param name="relationalExtensions"> The relational annotation provider. </param>
        /// <param name="typeMapper"> The type mapper. </param>
        public RelationalModelValidatorDependencies(
            [NotNull] IRelationalAnnotationProvider relationalExtensions,
            [NotNull] IRelationalTypeMapper typeMapper)
        {
            Check.NotNull(relationalExtensions, nameof(relationalExtensions));
            Check.NotNull(typeMapper, nameof(typeMapper));

            RelationalExtensions = relationalExtensions;
            TypeMapper = typeMapper;
        }

        /// <summary>
        ///     Gets the relational annotation provider.
        /// </summary>
        public IRelationalAnnotationProvider RelationalExtensions { get; }

        /// <summary>
        ///     Gets the type mapper.
        /// </summary>
        public IRelationalTypeMapper TypeMapper { get; }
    }
}
