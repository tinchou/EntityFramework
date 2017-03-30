// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.EntityFrameworkCore.Migrations.Operations
{
    public abstract class ModificationOperation : MigrationOperation
    {
        public ModificationOperation([NotNull] ModificationCommandBase modificationCommandBase)
        {
            Check.NotNull(modificationCommandBase, nameof(modificationCommandBase));

            ModificationCommand = modificationCommandBase;
        }

        public virtual ModificationCommandBase ModificationCommand { get; }
    }
}
