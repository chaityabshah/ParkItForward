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
    [GeneratedCode("EF.Reverse.POCO.Generator", "2.14.2.0")]
    public class FakePlannerDbContext : IPlannerDbContext
    {
        public FakePlannerDbContext()
        {
            Classes = new FakeDbSet<Class>();
            Events = new FakeDbSet<Event>();
            Nodes = new FakeDbSet<Node>();
            Roles = new FakeDbSet<Role>();
            Tasks = new FakeDbSet<Task>();
            Users = new FakeDbSet<User>();
            UserPasswords = new FakeDbSet<UserPassword>();
            UserRoles = new FakeDbSet<UserRole>();
            UserTokens = new FakeDbSet<UserToken>();
        }

        public IDbSet<Class> Classes { get; set; }
        public IDbSet<Event> Events { get; set; }
        public IDbSet<Node> Nodes { get; set; }
        public IDbSet<Role> Roles { get; set; }
        public IDbSet<Task> Tasks { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<UserPassword> UserPasswords { get; set; }
        public IDbSet<UserRole> UserRoles { get; set; }
        public IDbSet<UserToken> UserTokens { get; set; }

        public int SaveChanges()
        {
            return 0;
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}