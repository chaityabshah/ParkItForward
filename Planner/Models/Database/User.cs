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
    // Users
    [GeneratedCode("EF.Reverse.POCO.Generator", "2.14.2.0")]
    public class User
    {
        public User()
        {
            Classes = new List<Class>();
            Tasks = new List<Task>();
            UserPasswords = new List<UserPassword>();
            UserRoles = new List<UserRole>();
            UserTokens = new List<UserToken>();
        }

        public long Id { get; set; } // id (Primary key)
        public Guid Uuid { get; set; } // uuid
        public string EmailAddress { get; set; } // email_address
        public string Offset { get; set; } // offset
        public string Status { get; set; } // status
        public DateTime Created { get; set; } // created
        public DateTime? Updated { get; set; } // updated
        // Reverse navigation
        public virtual ICollection<Class> Classes { get; set; } // Classes.FK_Users_Classes
        public virtual ICollection<Task> Tasks { get; set; } // Tasks.FK_Users_Tasks
        public virtual ICollection<UserPassword> UserPasswords { get; set; } // UserPasswords.FK_Users_UserPasswords
        public virtual ICollection<UserRole> UserRoles { get; set; } // UserRoles.FK_User_UserRoles
        public virtual ICollection<UserToken> UserTokens { get; set; } // UserTokens.FK_Users_UserTokens
    }
}