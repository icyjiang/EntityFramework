// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.Data.Entity.Identity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Relational;
using Microsoft.Data.Entity.SqlServer;
using Microsoft.Data.Entity.SqlServer.Migrations;
using Microsoft.Data.Entity.SqlServer.Update;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Utilities;

// ReSharper disable once CheckNamespace

namespace Microsoft.Framework.DependencyInjection
{
    public static class SqlServerEntityServicesBuilderExtensions
    {
        public static EntityFrameworkServicesBuilder AddSqlServer([NotNull] this EntityFrameworkServicesBuilder builder)
        {
            Check.NotNull(builder, "builder");

            builder.AddRelational().ServiceCollection
                .AddScoped<DataStoreSource, SqlServerDataStoreSource>()
                .TryAdd(new ServiceCollection()
                    .AddSingleton<SqlServerModelBuilderFactory>()
                    .AddSingleton<SqlServerValueGeneratorCache>()
                    .AddSingleton<SqlServerValueGeneratorSelector>()
                    .AddSingleton<SimpleValueGeneratorFactory<SequentialGuidValueGenerator>>()
                    .AddSingleton<SqlServerSequenceValueGeneratorFactory>()
                    .AddSingleton<SqlServerSqlGenerator>()
                    .AddSingleton<SqlStatementExecutor>()
                    .AddSingleton<SqlServerTypeMapper>()
                    .AddSingleton<SqlServerModificationCommandBatchFactory>()
                    .AddSingleton<SqlServerCommandBatchPreparer>()
                    .AddSingleton<SqlServerModelSource>()
                    .AddScoped<SqlServerBatchExecutor>()
                    .AddScoped<SqlServerDataStoreServices>()
                    .AddScoped<SqlServerDataStore>()
                    .AddScoped<SqlServerConnection>()
                    .AddScoped<SqlServerModelDiffer>()
                    .AddScoped<SqlServerDatabase>()
                    .AddScoped<SqlServerMigrationSqlGenerator>()
                    .AddScoped<SqlServerDataStoreCreator>()
                    .AddScoped<SqlServerHistoryRepository>());

            return builder;
        }
    }
}
