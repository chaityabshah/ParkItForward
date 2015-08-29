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
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Entity.ModelConfiguration;
using System.Threading;
using System.Threading.Tasks;
using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption;

namespace Planner.Models.Database
{
    public class PlannerDbContext : DbContext, IPlannerDbContext
    {
        static PlannerDbContext()
        {
            System.Data.Entity.Database.SetInitializer<PlannerDbContext>(null);
        }

        public PlannerDbContext()
            : base("Name=Planner")
        {
        }

        public PlannerDbContext(string connectionString) : base(connectionString)
        {
        }

        public PlannerDbContext(string connectionString, DbCompiledModel model) : base(connectionString, model)
        {
        }

        public IDbSet<Class> Classes { get; set; } // Classes
        public IDbSet<Event> Events { get; set; } // Events
        public IDbSet<Node> Nodes { get; set; } // Nodes
        public IDbSet<Role> Roles { get; set; } // Roles
        public IDbSet<Task> Tasks { get; set; } // Tasks
        public IDbSet<User> Users { get; set; } // Users
        public IDbSet<UserPassword> UserPasswords { get; set; } // UserPasswords
        public IDbSet<UserRole> UserRoles { get; set; } // UserRoles
        public IDbSet<UserToken> UserTokens { get; set; } // UserTokens

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new ClassConfiguration());
            modelBuilder.Configurations.Add(new EventConfiguration());
            modelBuilder.Configurations.Add(new NodeConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new TaskConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new UserPasswordConfiguration());
            modelBuilder.Configurations.Add(new UserRoleConfiguration());
            modelBuilder.Configurations.Add(new UserTokenConfiguration());
        }

        public static DbModelBuilder CreateModel(DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new ClassConfiguration(schema));
            modelBuilder.Configurations.Add(new EventConfiguration(schema));
            modelBuilder.Configurations.Add(new NodeConfiguration(schema));
            modelBuilder.Configurations.Add(new RoleConfiguration(schema));
            modelBuilder.Configurations.Add(new TaskConfiguration(schema));
            modelBuilder.Configurations.Add(new UserConfiguration(schema));
            modelBuilder.Configurations.Add(new UserPasswordConfiguration(schema));
            modelBuilder.Configurations.Add(new UserRoleConfiguration(schema));
            modelBuilder.Configurations.Add(new UserTokenConfiguration(schema));
            return modelBuilder;
        }
    }
}