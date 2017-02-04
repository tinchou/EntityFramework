// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.EntityFrameworkCore.Query.Sql
{
    /// <summary>
    ///     <para>
    ///         Service dependencies parameter class for <see cref="QuerySqlGeneratorFactoryBase" />
    ///     </para>
    ///     <para>
    ///         This type is typically used by database providers (and other extensions). It is generally
    ///         not used in application code.
    ///     </para>
    /// </summary>
    public sealed class QuerySqlGeneratorDependencies
    {
        /// <summary>
        ///     <para>
        ///         Creates the service dependencies parameter object for a <see cref="QuerySqlGeneratorFactoryBase" />.
        ///     </para>
        ///     <para>
        ///         Do not call this constructor directly from provider or application code as it may change
        ///         as new dependencies are added. Use the 'With...' methods instead.
        ///     </para>
        /// </summary>
        /// <param name="commandBuilderFactory"> The command builder factory. </param>
        /// <param name="sqlGenerationHelper"> The SQL generation helper. </param>
        /// <param name="parameterNameGeneratorFactory"> The parameter name generator factory. </param>
        /// <param name="relationalTypeMapper"> The relational type mapper. </param>
        public QuerySqlGeneratorDependencies(
            [NotNull] IRelationalCommandBuilderFactory commandBuilderFactory,
            [NotNull] ISqlGenerationHelper sqlGenerationHelper,
            [NotNull] IParameterNameGeneratorFactory parameterNameGeneratorFactory,
            [NotNull] IRelationalTypeMapper relationalTypeMapper)
        {
            Check.NotNull(commandBuilderFactory, nameof(commandBuilderFactory));
            Check.NotNull(sqlGenerationHelper, nameof(sqlGenerationHelper));
            Check.NotNull(parameterNameGeneratorFactory, nameof(parameterNameGeneratorFactory));
            Check.NotNull(relationalTypeMapper, nameof(relationalTypeMapper));

            CommandBuilderFactory = commandBuilderFactory;
            SqlGenerationHelper = sqlGenerationHelper;
            ParameterNameGeneratorFactory = parameterNameGeneratorFactory;
            RelationalTypeMapper = relationalTypeMapper;
        }

        /// <summary>
        ///     Gets the command builder factory.
        /// </summary>
        public IRelationalCommandBuilderFactory CommandBuilderFactory { get; }

        /// <summary>
        ///     Gets the SQL generation helper.
        /// </summary>
        public ISqlGenerationHelper SqlGenerationHelper { get; }

        /// <summary>
        ///     Gets the parameter name generator factory.
        /// </summary>
        public IParameterNameGeneratorFactory ParameterNameGeneratorFactory { get; }

        /// <summary>
        ///     Gets the relational type mapper.
        /// </summary>
        public IRelationalTypeMapper RelationalTypeMapper { get; }
    }
}
