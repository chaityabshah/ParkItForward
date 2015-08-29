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
    // UserPasswords
    internal class UserPasswordConfiguration : EntityTypeConfiguration<UserPassword>
    {
        public UserPasswordConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".UserPasswords");
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("id")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.UserId).HasColumnName("user_id").IsRequired();
            Property(x => x.PasswordSalt).HasColumnName("password_salt").IsRequired().HasMaxLength(16);
            Property(x => x.PasswordHash).HasColumnName("password_hash").IsRequired().HasMaxLength(128);
            Property(x => x.Status).HasColumnName("status").IsRequired().HasMaxLength(16);
            Property(x => x.Created).HasColumnName("created").IsRequired();
            Property(x => x.Updated).HasColumnName("updated").IsOptional();

            // Foreign keys
            HasRequired(a => a.User).WithMany(b => b.UserPasswords).HasForeignKey(c => c.UserId);
                // FK_Users_UserPasswords
        }
    }
}