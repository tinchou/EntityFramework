// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Update;
using System;
using System.Linq;

namespace Microsoft.EntityFrameworkCore.Migrations.Operations
{
    public class DeleteOperation : ModificationOperation
    {
        public DeleteOperation(
            string table,
            string[] keyColumns,
            object[] keyValues)
        : this(
            null,
            table,
            keyColumns,
            keyValues)
        {
        }

        public DeleteOperation(
            string schema,
            string table,
            string[] keyColumns,
            object[] keyValues)
        : base(
            new ModificationCommandBase(
                table,
                schema,
                keyColumns.Zip(keyValues, (k, v) => new ColumnModificationBase(k, null, null, v, false, true, true, true, false)).ToArray()))
        {
            Schema = schema;
            Table = table;
            KeyColumns = keyColumns;
            KeyValues = keyValues;
        }

        public virtual string Schema { get; }
        public virtual string Table { get; }
        public virtual string[] KeyColumns { get; }
        public virtual object[] KeyValues { get; }
    }
}
