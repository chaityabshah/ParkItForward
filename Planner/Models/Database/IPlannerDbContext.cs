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
    public interface IPlannerDbContext : IDisposable
    {
        IDbSet<Class> Classes { get; set; } // Classes
        IDbSet<Event> Events { get; set; } // Events
        IDbSet<Node> Nodes { get; set; } // Nodes
        IDbSet<Role> Roles { get; set; } // Roles
        IDbSet<Task> Tasks { get; set; } // Tasks
        IDbSet<User> Users { get; set; } // Users
        IDbSet<UserPassword> UserPasswords { get; set; } // UserPasswords
        IDbSet<UserRole> UserRoles { get; set; } // UserRoles
        IDbSet<UserToken> UserTokens { get; set; } // UserTokens
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}