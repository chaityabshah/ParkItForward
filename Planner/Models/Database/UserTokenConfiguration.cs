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
    // UserTokens
    internal class UserTokenConfiguration : EntityTypeConfiguration<UserToken>
    {
        public UserTokenConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".UserTokens");
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("id")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.UserId).HasColumnName("user_id").IsRequired();
            Property(x => x.Token).HasColumnName("token").IsRequired();
            Property(x => x.Status).HasColumnName("status").IsRequired().HasMaxLength(16);
            Property(x => x.Expires).HasColumnName("expires").IsRequired();
            Property(x => x.Created).HasColumnName("created").IsRequired();
            Property(x => x.Updated).HasColumnName("updated").IsOptional();

            // Foreign keys
            HasRequired(a => a.User).WithMany(b => b.UserTokens).HasForeignKey(c => c.UserId); // FK_Users_UserTokens
        }
    }
}