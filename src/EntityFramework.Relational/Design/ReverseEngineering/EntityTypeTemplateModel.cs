﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Data.Entity.Metadata;

namespace Microsoft.Data.Entity.Relational.Design.ReverseEngineering
{
    public class EntityTypeTemplateModel : BaseTemplateModel
    {
        public IEntityType EntityType { get; set; }
    }
}