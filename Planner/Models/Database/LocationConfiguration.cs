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
    // locations
    internal class LocationConfiguration : EntityTypeConfiguration<Location>
    {
        public LocationConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".locations");
            HasKey(x => new { x.Id, x.Vin, x.Lat, x.Lng, x.Type, x.Status, x.Created });

            Property(x => x.Id).HasColumnName("id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Vin).HasColumnName("vin").IsRequired().HasMaxLength(32);
            Property(x => x.Lat).HasColumnName("lat").IsRequired().IsUnicode(false).HasMaxLength(2147483647);
            Property(x => x.Lng).HasColumnName("lng").IsRequired().IsUnicode(false).HasMaxLength(2147483647);
            Property(x => x.Type).HasColumnName("type").IsRequired().HasMaxLength(16);
            Property(x => x.Status).HasColumnName("status").IsRequired().HasMaxLength(16);
            Property(x => x.Created).HasColumnName("created").IsRequired();
            Property(x => x.Updated).HasColumnName("updated").IsOptional();
        }
    }

}
