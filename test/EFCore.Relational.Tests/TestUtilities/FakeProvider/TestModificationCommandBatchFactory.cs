﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update;

namespace Microsoft.EntityFrameworkCore.Relational.Tests.TestUtilities.FakeProvider
{
    public class TestModificationCommandBatchFactory : IModificationCommandBatchFactory
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
}
