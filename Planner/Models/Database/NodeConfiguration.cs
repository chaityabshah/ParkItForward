// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier
// TargetFrameworkVersion = 4.51

#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Entity.ModelConfiguration;
using System.Threading;
using System.Threading.Tasks;
using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption;

namespace Planner.Models.Database
{
    // Nodes
    internal class NodeConfiguration : EntityTypeConfiguration<Node>
    {
        public NodeConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Nodes");
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("id")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.UserId).HasColumnName("user_id").IsRequired();
            Property(x => x.ParentNodeId).HasColumnName("parent_node_id").IsOptional();
            Property(x => x.Name).HasColumnName("name").IsRequired().HasMaxLength(128);
            Property(x => x.Type).HasColumnName("type").IsOptional().HasMaxLength(128);
            Property(x => x.Description)
                .HasColumnName("description")
                .IsOptional()
                .IsUnicode(false)
                .HasMaxLength(2147483647);
            Property(x => x.Startdate).HasColumnName("startdate").IsOptional();
            Property(x => x.Enddate).HasColumnName("enddate").IsOptional();
            Property(x => x.Status).HasColumnName("status").IsRequired().HasMaxLength(16);
            Property(x => x.Created).HasColumnName("created").IsRequired();
            Property(x => x.Updated).HasColumnName("updated").IsOptional();
        }
    }
}