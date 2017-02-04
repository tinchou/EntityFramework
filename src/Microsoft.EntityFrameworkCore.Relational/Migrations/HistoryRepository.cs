// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.EntityFrameworkCore.Migrations
{
    /// <summary>
    ///     <para>
    ///         Service dependencies parameter class for <see cref="HistoryRepository" />
    ///     </para>
    ///     <para>
    ///         This type is typically used by database providers (and other extensions). It is generally
    ///         not used in application code.
    ///     </para>
    /// </summary>
    public sealed class HistoryRepositoryDependencies
    {
        /// <summary>
        ///     <para>
        ///         Creates the service dependencies parameter object for a <see cref="HistoryRepository" />.
        ///     </para>
        ///     <para>
        ///         Do not call this constructor directly from provider or application code as it may change
        ///         as new dependencies are added. Use the 'With...' methods instead.
        ///     </para>
        ///     <para>
        ///         This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///         directly from your code. This API may change or be removed in future releases.
        ///     </para>
        /// </summary>
        /// <param name="databaseCreator"> The database creator. </param>
        /// <param name="rawSqlCommandBuilder"> A command builder for building raw SQL commands. </param>
        /// <param name="connection"> The connection to the database. </param>
        /// <param name="options"> Options for the current context instance. </param>
        /// <param name="modelDiffer"> The model differ. </param>
        /// <param name="migrationsSqlGenerator"> The SQL generator for Migrations operations. </param>
        /// <param name="annotations"> Access to relational metadata for the model. </param>
        /// <param name="sqlGenerationHelper"> Helpers for generating update SQL. </param>
        public HistoryRepositoryDependencies(
            [NotNull] IDatabaseCreator databaseCreator,
            [NotNull] IRawSqlCommandBuilder rawSqlCommandBuilder,
            [NotNull] IRelationalConnection connection,
            [NotNull] IDbContextOptions options,
            [NotNull] IMigrationsModelDiffer modelDiffer,
            [NotNull] IMigrationsSqlGenerator migrationsSqlGenerator,
            [NotNull] IRelationalAnnotationProvider annotations,
            [NotNull] ISqlGenerationHelper sqlGenerationHelper)
        {
            Check.NotNull(databaseCreator, nameof(databaseCreator));
            Check.NotNull(rawSqlCommandBuilder, nameof(rawSqlCommandBuilder));
            Check.NotNull(connection, nameof(connection));
            Check.NotNull(options, nameof(options));
            Check.NotNull(modelDiffer, nameof(modelDiffer));
            Check.NotNull(migrationsSqlGenerator, nameof(migrationsSqlGenerator));
            Check.NotNull(annotations, nameof(annotations));
            Check.NotNull(sqlGenerationHelper, nameof(sqlGenerationHelper));

            DatabaseCreator = (IRelationalDatabaseCreator)databaseCreator;
            RawSqlCommandBuilder = rawSqlCommandBuilder;
            Connection = connection;
            Options = options;
            ModelDiffer = modelDiffer;
            MigrationsSqlGenerator = migrationsSqlGenerator;
            Annotations = annotations;
            SqlGenerationHelper = sqlGenerationHelper;
        }

        /// <summary>
        ///     The database creator.
        /// </summary>
        public IRelationalDatabaseCreator DatabaseCreator { get; }

        /// <summary>
        ///     A command builder for building raw SQL commands.
        /// </summary>
        public IRawSqlCommandBuilder RawSqlCommandBuilder { get; }

        /// <summary>
        ///     The connection to the database.
        /// </summary>
        public IRelationalConnection Connection { get; }

        /// <summary>
        ///     Options for the current context instance.
        /// </summary>
        public IDbContextOptions Options { get; }

        /// <summary>
        ///     The model differ.
        /// </summary>
        public IMigrationsModelDiffer ModelDiffer { get; }

        /// <summary>
        ///     The SQL generator for Migrations operations.
        /// </summary>
        public IMigrationsSqlGenerator MigrationsSqlGenerator { get; }

        /// <summary>
        ///     Access to relational metadata for the model.
        /// </summary>
        public IRelationalAnnotationProvider Annotations { get; }

        /// <summary>
        ///     Helpers for generating update SQL.
        /// </summary>
        public ISqlGenerationHelper SqlGenerationHelper { get; }
    }

    // TODO: Leverage query pipeline for GetAppliedMigrations
    // TODO: Leverage update pipeline for GetInsertScript & GetDeleteScript
    public abstract class HistoryRepository : IHistoryRepository
    {
        public const string DefaultTableName = "__EFMigrationsHistory";

        private readonly LazyRef<IModel> _model;
        private readonly LazyRef<string> _migrationIdColumnName;
        private readonly LazyRef<string> _productVersionColumnName;

        /// <summary>
        ///     Initializes a new instance of this class.
        /// </summary>
        /// <param name="dependencies"> Parameter object containing dependencies for this service. </param>
        protected HistoryRepository([NotNull] HistoryRepositoryDependencies dependencies)
        {
            Check.NotNull(dependencies, nameof(dependencies));

            Dependencies = dependencies;

            var relationalOptions = RelationalOptionsExtension.Extract(dependencies.Options);
            TableName = relationalOptions?.MigrationsHistoryTableName ?? DefaultTableName;
            TableSchema = relationalOptions?.MigrationsHistoryTableSchema;
            _model = new LazyRef<IModel>(
                () =>
                    {
                        var modelBuilder = new ModelBuilder(new ConventionSet());
                        modelBuilder.Entity<HistoryRow>(
                            x =>
                                {
                                    ConfigureTable(x);
                                    x.ToTable(TableName, TableSchema);
                                });

                        return modelBuilder.Model;
                    });
            var entityType = new LazyRef<IEntityType>(() => _model.Value.FindEntityType(typeof(HistoryRow)));
            _migrationIdColumnName = new LazyRef<string>(
                () => dependencies.Annotations.For(entityType.Value.FindProperty(nameof(HistoryRow.MigrationId))).ColumnName);
            _productVersionColumnName = new LazyRef<string>(
                () => dependencies.Annotations.For(entityType.Value.FindProperty(nameof(HistoryRow.ProductVersion))).ColumnName);
        }

        /// <summary>
        ///     Parameter object containing service dependencies.
        /// </summary>
        protected virtual HistoryRepositoryDependencies Dependencies { get; }

        protected virtual ISqlGenerationHelper SqlGenerationHelper => Dependencies.SqlGenerationHelper;

        protected virtual string TableName { get; }
        protected virtual string TableSchema { get; }
        protected virtual string MigrationIdColumnName => _migrationIdColumnName.Value;
        protected virtual string ProductVersionColumnName => _productVersionColumnName.Value;

        protected abstract string ExistsSql { get; }

        public virtual bool Exists()
            => Dependencies.DatabaseCreator.Exists()
               && InterpretExistsResult(
                   Dependencies.RawSqlCommandBuilder.Build(ExistsSql).ExecuteScalar(Dependencies.Connection));

        public virtual async Task<bool> ExistsAsync(CancellationToken cancellationToken = default(CancellationToken))
            => await Dependencies.DatabaseCreator.ExistsAsync(cancellationToken)
               && InterpretExistsResult(
                   await Dependencies.RawSqlCommandBuilder.Build(ExistsSql).ExecuteScalarAsync(
                       Dependencies.Connection, cancellationToken: cancellationToken));

        /// <returns>true if the table exists; otherwise, false.</returns>
        protected abstract bool InterpretExistsResult([NotNull] object value);

        public abstract string GetCreateIfNotExistsScript();

        public virtual string GetCreateScript()
        {
            var operations = Dependencies.ModelDiffer.GetDifferences(null, _model.Value);
            var commandList = Dependencies.MigrationsSqlGenerator.Generate(operations, _model.Value);

            return string.Concat(commandList.Select(c => c.CommandText));
        }

        protected virtual void ConfigureTable([NotNull] EntityTypeBuilder<HistoryRow> history)
        {
            history.ToTable(DefaultTableName);
            history.HasKey(h => h.MigrationId);
            history.Property(h => h.MigrationId).HasMaxLength(150);
            history.Property(h => h.ProductVersion).HasMaxLength(32).IsRequired();
        }

        public virtual IReadOnlyList<HistoryRow> GetAppliedMigrations()
        {
            var rows = new List<HistoryRow>();

            if (Exists())
            {
                var command = Dependencies.RawSqlCommandBuilder.Build(GetAppliedMigrationsSql);

                using (var reader = command.ExecuteReader(Dependencies.Connection))
                {
                    while (reader.DbDataReader.Read())
                    {
                        rows.Add(new HistoryRow(reader.DbDataReader.GetString(0), reader.DbDataReader.GetString(1)));
                    }
                }
            }

            return rows;
        }

        public virtual async Task<IReadOnlyList<HistoryRow>> GetAppliedMigrationsAsync(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var rows = new List<HistoryRow>();

            if (await ExistsAsync(cancellationToken))
            {
                var command = Dependencies.RawSqlCommandBuilder.Build(GetAppliedMigrationsSql);

                using (var reader = await command.ExecuteReaderAsync(Dependencies.Connection, cancellationToken: cancellationToken))
                {
                    while (await reader.DbDataReader.ReadAsync(cancellationToken))
                    {
                        rows.Add(new HistoryRow(reader.DbDataReader.GetString(0), reader.DbDataReader.GetString(1)));
                    }
                }
            }

            return rows;
        }

        protected virtual string GetAppliedMigrationsSql
            => new StringBuilder()
                .Append("SELECT ")
                .Append(SqlGenerationHelper.DelimitIdentifier(MigrationIdColumnName))
                .Append(", ")
                .AppendLine(SqlGenerationHelper.DelimitIdentifier(ProductVersionColumnName))
                .Append("FROM ")
                .AppendLine(SqlGenerationHelper.DelimitIdentifier(TableName, TableSchema))
                .Append("ORDER BY ")
                .Append(SqlGenerationHelper.DelimitIdentifier(MigrationIdColumnName))
                .Append(SqlGenerationHelper.StatementTerminator)
                .ToString();

        public virtual string GetInsertScript(HistoryRow row)
        {
            Check.NotNull(row, nameof(row));

            return new StringBuilder().Append("INSERT INTO ")
                .Append(SqlGenerationHelper.DelimitIdentifier(TableName, TableSchema))
                .Append(" (")
                .Append(SqlGenerationHelper.DelimitIdentifier(MigrationIdColumnName))
                .Append(", ")
                .Append(SqlGenerationHelper.DelimitIdentifier(ProductVersionColumnName))
                .AppendLine(")")
                .Append("VALUES ('")
                .Append(SqlGenerationHelper.EscapeLiteral(row.MigrationId))
                .Append("', '")
                .Append(SqlGenerationHelper.EscapeLiteral(row.ProductVersion))
                .Append("')")
                .AppendLine(SqlGenerationHelper.StatementTerminator)
                .ToString();
        }

        public virtual string GetDeleteScript(string migrationId)
        {
            Check.NotEmpty(migrationId, nameof(migrationId));

            return new StringBuilder().Append("DELETE FROM ")
                .AppendLine(SqlGenerationHelper.DelimitIdentifier(TableName, TableSchema))
                .Append("WHERE ")
                .Append(SqlGenerationHelper.DelimitIdentifier(MigrationIdColumnName))
                .Append(" = '")
                .Append(SqlGenerationHelper.EscapeLiteral(migrationId))
                .Append("'")
                .AppendLine(SqlGenerationHelper.StatementTerminator)
                .ToString();
        }

        public abstract string GetBeginIfNotExistsScript(string migrationId);
        public abstract string GetBeginIfExistsScript(string migrationId);
        public abstract string GetEndIfScript();
    }
}
