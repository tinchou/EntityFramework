// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace Microsoft.EntityFrameworkCore.Relational.Tests.TestUtilities.FakeProvider
{
    public class TestCommandBatchPreparer : CommandBatchPreparer
    {
        public TestCommandBatchPreparer(IModificationCommandBatchFactory modificationCommandBatchFactory)
        : base(
            modificationCommandBatchFactory,
            new ParameterNameGeneratorFactory(new ParameterNameGeneratorDependencies()),
            new ModificationCommandComparer(),
            new TestAnnotationProvider(),
            new KeyValueIndexFactorySource())
        {
        }
    }
}
