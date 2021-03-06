// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.ChangeTracking.Internal
{
    public class OriginalValuesFactory
    {
        public virtual Sidecar Create([NotNull] InternalEntityEntry entry)
        {
            Check.NotNull(entry, "entry");

            return new OriginalValues(entry);
        }
    }
}
