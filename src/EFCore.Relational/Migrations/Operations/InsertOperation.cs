// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Update;
using System;
using System.Linq;

namespace Microsoft.EntityFrameworkCore.Migrations.Operations
{
    public class InsertOperation : ModificationOperation
    {
        public InsertOperation(
            string table,
            string[] columns,
            object[] values)
        : this(
            null,
            table,
            columns,
            values)
        {
        }

        public InsertOperation(
            string schema,
            string table,
            string[] columns,
            object[] values)
        : base(
            new ModificationCommandBase(
                table,
                schema,
                columns.Zip(values, (k, v) => new ColumnModificationBase(k, null, null, v, false, true, true, false, false)).ToArray()))
        {
            Schema = schema;
            Table = table;
            Columns = columns;
            Values = values;
        }

        public virtual string Schema { get; }
        public virtual string Table { get; }
        public virtual string[] Columns { get; }
        public virtual object[] Values { get; }
    }
}
