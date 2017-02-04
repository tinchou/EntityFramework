// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.EntityFrameworkCore.Update
{
    /// <summary>
    ///     <para>
    ///         Service dependencies parameter class for <see cref="UpdateSqlGenerator" />
    ///     </para>
    ///     <para>
    ///         This type is typically used by database providers (and other extensions). It is generally
    ///         not used in application code.
    ///     </para>
    /// </summary>
    public sealed class UpdateSqlGeneratorDependencies
    {
        /// <summary>
        ///     <para>
        ///         Creates the service dependencies parameter object for a <see cref="UpdateSqlGenerator" />.
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
        /// <param name="sqlGenerationHelper"> Helpers for generating update SQL. </param>
        public UpdateSqlGeneratorDependencies([NotNull] ISqlGenerationHelper sqlGenerationHelper)
        {
            Check.NotNull(sqlGenerationHelper, nameof(sqlGenerationHelper));

            SqlGenerationHelper = sqlGenerationHelper;
        }

        /// <summary>
        ///     Helpers for generating update SQL.
        /// </summary>
        public ISqlGenerationHelper SqlGenerationHelper { get; }
    }
}
