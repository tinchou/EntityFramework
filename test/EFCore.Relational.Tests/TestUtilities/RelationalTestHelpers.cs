// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Relational.Tests.TestUtilities.FakeProvider;
using Microsoft.EntityFrameworkCore.Specification.Tests;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Microsoft.EntityFrameworkCore.Relational.Tests.TestUtilities
{
    public class RelationalTestHelpers : TestHelpers
    {
        protected RelationalTestHelpers()
        {
        }

        public static RelationalTestHelpers Instance { get; } = new RelationalTestHelpers();

        public override IServiceCollection AddProviderServices(IServiceCollection services)
            => FakeRelationalOptionsExtension.AddEntityFrameworkRelationalDatabase(services);

        public virtual ICommandBatchPreparer CreateCommandBatchPreparer(IModificationCommandBatchFactory modificationCommandBatchFactory = null)
            => new TestCommandBatchPreparer(
                modificationCommandBatchFactory ?? CreateModificationCommandBatchFactory());

        public virtual IModificationCommandBatchFactory CreateModificationCommandBatchFactory()
            => new TestModificationCommandBatchFactory(
                Mock.Of<IRelationalCommandBuilderFactory>(),
                Mock.Of<ISqlGenerationHelper>(),
                Mock.Of<IUpdateSqlGenerator>(),
                Mock.Of<IRelationalValueBufferFactoryFactory>());

        protected override void UseProviderOptions(DbContextOptionsBuilder optionsBuilder)
        {
            var extension = optionsBuilder.Options.FindExtension<FakeRelationalOptionsExtension>()
                            ?? new FakeRelationalOptionsExtension();

            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(
                extension.WithConnection(new FakeDbConnection("Database=Fake")));
        }
    }
}