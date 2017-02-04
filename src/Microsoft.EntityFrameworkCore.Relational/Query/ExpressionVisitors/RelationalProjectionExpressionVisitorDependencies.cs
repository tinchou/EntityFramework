// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.EntityFrameworkCore.Query.ExpressionVisitors
{
    /// <summary>
    ///     <para>
    ///         Service dependencies parameter class for <see cref="RelationalProjectionExpressionVisitor" />
    ///     </para>
    ///     <para>
    ///         This type is typically used by database providers (and other extensions). It is generally
    ///         not used in application code.
    ///     </para>
    /// </summary>
    public sealed class RelationalProjectionExpressionVisitorDependencies
    {
        /// <summary>
        ///     <para>
        ///         Creates the service dependencies parameter object for a <see cref="RelationalProjectionExpressionVisitor" />.
        ///     </para>
        ///     <para>
        ///         This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///         directly from your code. This API may change or be removed in future releases.
        ///     </para>
        ///     <para>
        ///         Do not call this constructor directly from provider or application code as it may change
        ///         as new dependencies are added. Use the 'With...' methods instead.
        ///     </para>
        /// </summary>
        /// <param name="sqlTranslatingExpressionVisitorFactory"> The SQL translating expression visitor factory. </param>
        /// <param name="entityMaterializerSource"> The entity materializer source. </param>
        public RelationalProjectionExpressionVisitorDependencies(
            [NotNull] ISqlTranslatingExpressionVisitorFactory sqlTranslatingExpressionVisitorFactory,
            [NotNull] IEntityMaterializerSource entityMaterializerSource)
        {
            Check.NotNull(sqlTranslatingExpressionVisitorFactory, nameof(sqlTranslatingExpressionVisitorFactory));
            Check.NotNull(entityMaterializerSource, nameof(entityMaterializerSource));

            SqlTranslatingExpressionVisitorFactory = sqlTranslatingExpressionVisitorFactory;
            EntityMaterializerSource = entityMaterializerSource;
        }

        /// <summary>
        ///     The SQL translating expression visitor factory.
        /// </summary>
        public ISqlTranslatingExpressionVisitorFactory SqlTranslatingExpressionVisitorFactory { get; }

        /// <summary>
        ///     The entity materializer source.
        /// </summary>
        public IEntityMaterializerSource EntityMaterializerSource { get; }
    }
}
