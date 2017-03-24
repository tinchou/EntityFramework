// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.EntityFrameworkCore.Utilities;
using System.Linq;

namespace Microsoft.EntityFrameworkCore.Migrations.Operations
{
    public class DeleteOperation : ModificationOperation
    {
        public DeleteOperation(
            [NotNull] string table,
            [NotNull] string[] keyColumns,
            [NotNull] object[] keyValues)
        : this(
            null,
            table,
            keyColumns,
            keyValues)
        {
        }

        public DeleteOperation(
            [CanBeNull] string schema,
            [NotNull] string table,
            [NotNull] string[] keyColumns,
            [NotNull] object[] keyValues)
        : base(
            new ModificationCommandBase(
                table,
                schema,
                keyColumns.Zip(keyValues, (k, v) => new ColumnModificationBase(k, null, null, v, false, true, true, true, false)).ToArray()))
        {
            Check.NotNull(table, nameof(table));
            Check.NotNull(keyColumns, nameof(keyColumns));
            Check.NotNull(keyValues, nameof(keyValues));

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
