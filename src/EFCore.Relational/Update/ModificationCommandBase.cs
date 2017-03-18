// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Microsoft.EntityFrameworkCore.Update
{
    public class ModificationCommandBase
    {
        public ModificationCommandBase(
            string name,
            string schema,
            IReadOnlyList<ColumnModificationBase> columnModificationsBase)
        {
            TableName = name;
            Schema = schema;
            ColumnModificationsBase = columnModificationsBase;
        }

        public virtual string TableName { get; }

        public virtual string Schema { get; }

        public virtual IReadOnlyList<ColumnModificationBase> ColumnModificationsBase { get; }
    }
}
