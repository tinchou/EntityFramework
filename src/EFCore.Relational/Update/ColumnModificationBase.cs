// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Microsoft.EntityFrameworkCore.Update
{
    public class ColumnModificationBase
    {
        public ColumnModificationBase(
            string columnName,
            string parameterName,
            object originalValue,
            object value,
            bool isRead,
            bool isWrite,
            bool isKey,
            bool isCondition,
            bool useOriginalValueParameter)
        {
            ColumnName = columnName;
            ParameterName = OriginalParameterName = parameterName;
            OriginalValue = originalValue;
            Value = value;
            IsRead = isRead;
            IsWrite = isWrite;
            IsKey = isKey;
            IsCondition = isCondition;
            UseOriginalValueParameter = useOriginalValueParameter;
        }

        public virtual string ColumnName { get; }

        public virtual string ParameterName { get; }

        public virtual string OriginalParameterName { get; }

        public virtual bool IsRead { get; }

        public virtual bool IsWrite { get; }

        public virtual bool IsKey { get; }

        public virtual bool IsCondition { get; }

        public virtual bool UseOriginalValueParameter { get; }

        public virtual object OriginalValue { get; }

        public virtual object Value { get; }
    }
}
