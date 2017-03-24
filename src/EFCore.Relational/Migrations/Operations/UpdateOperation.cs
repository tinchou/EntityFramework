// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.EntityFrameworkCore.Utilities;
using System.Linq;

namespace Microsoft.EntityFrameworkCore.Migrations.Operations
{
    public class UpdateOperation : ModificationOperation
    {
        public UpdateOperation(
            [NotNull] string table,
            [NotNull] string[] keyColumns,
            [NotNull] object[] keyValues,
            [NotNull] string[] columns,
            [NotNull] object[] values)
        : this(
            null,
            table,
            keyColumns,
            keyValues,
            columns,
            values)
        {
        }

        public UpdateOperation(
            [CanBeNull] string schema,
            [NotNull] string table,
            [NotNull] string[] keyColumns,
            [NotNull] object[] keyValues,
            [NotNull] string[] columns,
            [NotNull] object[] values)
        : base(
            new ModificationCommandBase(
                table,
                schema,
                keyColumns.Zip(keyValues,
                    (k, v) => new ColumnModificationBase(k, null, null, v, false, false, true, true, false)).Concat(
                columns.Zip(values,
                    (k, v) => new ColumnModificationBase(k, null, null, v, false, true, true, false, false))).ToArray()))
        {
            Check.NotNull(table, nameof(table));
            Check.NotNull(keyColumns, nameof(keyColumns));
            Check.NotNull(keyValues, nameof(keyValues));
            Check.NotNull(columns, nameof(columns));
            Check.NotNull(values, nameof(values));

            Schema = schema;
            Table = table;
            KeyColumns = keyColumns;
            KeyValues = keyValues;
            Columns = columns;
            Values = values;
        }

        public virtual string Schema { get; }
        public virtual string Table { get; }
        public virtual string[] KeyColumns { get; }
        public virtual object[] KeyValues { get; }
        public virtual string[] Columns { get; }
        public virtual object[] Values { get; }
    }
}
