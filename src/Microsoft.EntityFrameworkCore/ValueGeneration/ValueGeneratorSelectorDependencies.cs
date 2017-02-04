// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.EntityFrameworkCore.ValueGeneration
{
    /// <summary>
    ///     <para>
    ///         Service dependencies parameter class for <see cref="ValueGeneratorSelector" />
    ///     </para>
    ///     <para>
    ///         This type is typically used by database providers (and other extensions). It is generally
    ///         not used in application code.
    ///     </para>
    /// </summary>
    public sealed class ValueGeneratorSelectorDependencies
    {
        /// <summary>
        ///     <para>
        ///         Creates the service dependencies parameter object for a <see cref="ValueGeneratorSelector" />.
        ///     </para>
        ///     <para>
        ///         Do not call this constructor directly from provider or application code as it may change
        ///         as new dependencies are added. Use the 'With...' methods instead.
        ///     </para>
        /// </summary>
        /// <param name="cache"> The cache to be used to store value generator instances. </param>
        public ValueGeneratorSelectorDependencies([NotNull] IValueGeneratorCache cache)
        {
            Check.NotNull(cache, nameof(cache));

            Cache = cache;
        }

        /// <summary>
        ///     The cache being used to store value generator instances.
        /// </summary>
        public IValueGeneratorCache Cache { get; }
    }
}
