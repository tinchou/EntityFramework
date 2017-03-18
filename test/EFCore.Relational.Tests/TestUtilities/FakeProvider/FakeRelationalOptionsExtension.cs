// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;
using Microsoft.EntityFrameworkCore.Query.Sql;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;

namespace Microsoft.EntityFrameworkCore.Relational.Tests.TestUtilities.FakeProvider
{
    public class FakeRelationalOptionsExtension : RelationalOptionsExtension
    {
        public FakeRelationalOptionsExtension()
        {
        }

        protected FakeRelationalOptionsExtension(FakeRelationalOptionsExtension copyFrom)
            : base(copyFrom)
        {
        }

        protected override RelationalOptionsExtension Clone()
            => new FakeRelationalOptionsExtension(this);

        public override bool ApplyServices(IServiceCollection services)
        {
            AddEntityFrameworkRelationalDatabase(services);

            return true;
        }

        public static IServiceCollection AddEntityFrameworkRelationalDatabase(IServiceCollection serviceCollection)
        {
            var builder = new EntityFrameworkRelationalServicesBuilder(serviceCollection)
                .TryAdd<IDatabaseProvider, DatabaseProvider<FakeRelationalOptionsExtension>>()
                .TryAdd<ISqlGenerationHelper, RelationalSqlGenerationHelper>()
                .TryAdd<IRelationalTypeMapper, TestRelationalTypeMapper>()
                .TryAdd<IRelationalAnnotationProvider, TestAnnotationProvider>()
                .TryAdd<IMigrationsSqlGenerator, TestRelationalMigrationSqlGenerator>()
                .TryAdd<IConventionSetBuilder, TestRelationalConventionSetBuilder>()
                .TryAdd<IMemberTranslator, TestRelationalCompositeMemberTranslator>()
                .TryAdd<IMethodCallTranslator, TestRelationalCompositeMethodCallTranslator>()
                .TryAdd<IQuerySqlGeneratorFactory, TestQuerySqlGeneratorFactory>()
                .TryAdd<IRelationalConnection, FakeRelationalConnection>()
                .TryAdd<IHistoryRepository>(_ => null)
                .TryAdd<IUpdateSqlGenerator, FakeSqlGenerator>()
                .TryAdd<IModificationCommandBatchFactory, TestModificationCommandBatchFactory>()
                .TryAdd<IRelationalDatabaseCreator, FakeRelationalDatabaseCreator>();

            builder.TryAddCoreServices();

            return serviceCollection;
        }

        //
        //
        // TODO avoid copy-pasting from CommandBatchPreparerTest
        //
        //

        private class TestModificationCommandBatchFactory : IModificationCommandBatchFactory
        {
            private readonly IRelationalCommandBuilderFactory _commandBuilderFactory;
            private readonly ISqlGenerationHelper _sqlGenerationHelper;
            private readonly IUpdateSqlGenerator _updateSqlGenerator;
            private readonly IRelationalValueBufferFactoryFactory _valueBufferFactoryFactory;

            public TestModificationCommandBatchFactory(
                IRelationalCommandBuilderFactory commandBuilderfactory,
                ISqlGenerationHelper sqlGenerationHelper,
                IUpdateSqlGenerator updateSqlGenerator,
                IRelationalValueBufferFactoryFactory valueBufferFactoryFactory)
            {
                _commandBuilderFactory = commandBuilderfactory;
                _sqlGenerationHelper = sqlGenerationHelper;
                _updateSqlGenerator = updateSqlGenerator;
                _valueBufferFactoryFactory = valueBufferFactoryFactory;
            }

            public ModificationCommandBatch Create()
                => new SingularModificationCommandBatch(
                    _commandBuilderFactory,
                    _sqlGenerationHelper,
                    _updateSqlGenerator,
                    _valueBufferFactoryFactory);
        }

        //
        //
        // TODO avoid copy-pasting from UpdateSqlGeneratorTest
        //
        //

        private class ConcreteSqlGenerator : UpdateSqlGenerator
        {
            public ConcreteSqlGenerator()
                : base(
                    new UpdateSqlGeneratorDependencies(
                        new RelationalSqlGenerationHelper(
                            new RelationalSqlGenerationHelperDependencies())))
            {
            }

            protected override void AppendIdentityWhereCondition(StringBuilder commandStringBuilder, ColumnModificationBase columnModification)
                => commandStringBuilder
                    .Append(SqlGenerationHelper.DelimitIdentifier(columnModification.ColumnName))
                    .Append(" = ")
                    .Append("provider_specific_identity()");

            protected override ResultSetMapping AppendSelectAffectedCountCommand(StringBuilder commandStringBuilder, string name, string schema, int commandPosition)
            {
                commandStringBuilder
                    .Append("SELECT provider_specific_rowcount();" + Environment.NewLine + Environment.NewLine);

                return ResultSetMapping.LastInResultSet;
            }

            protected override void AppendRowsAffectedWhereCondition(StringBuilder commandStringBuilder, int expectedRowsAffected)
                => commandStringBuilder
                    .Append("provider_specific_rowcount() = " + expectedRowsAffected);
        }
    }
}
