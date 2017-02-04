// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Utilities;
using Microsoft.Extensions.Logging;

namespace Microsoft.EntityFrameworkCore.Infrastructure
{
    /// <summary>
    ///     <para>
    ///         Service dependencies parameter class for <see cref="ModelValidator" />
    ///     </para>
    ///     <para>
    ///         This type is typically used by database providers (and other extensions). It is generally
    ///         not used in application code.
    ///     </para>
    /// </summary>
    public sealed class ModelValidatorDependencies
    {
        /// <summary>
        ///     <para>
        ///         Creates the service dependencies parameter object for a <see cref="ModelValidator" />.
        ///     </para>
        ///     <para>
        ///         This type is typically used by database providers (and other extensions). It is generally
        ///         not used in application code.
        ///     </para>
        ///     <para>
        ///         Do not call this constructor directly from provider or application code as it may change
        ///         as new dependencies are added. Use the 'With...' methods instead.
        ///     </para>
        /// </summary>
        /// <param name="logger"> The logger. </param>
        public ModelValidatorDependencies([NotNull] ILogger<IModelValidator> logger)
        {
            Check.NotNull(logger, nameof(logger));

            Logger = logger;
        }

        /// <summary>
        ///     The logger.
        /// </summary>
        public ILogger<IModelValidator> Logger { get; }
    }
}
