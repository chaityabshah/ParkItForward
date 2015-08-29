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
    public class UserPassword
    {
        public long Id { get; set; } // id (Primary key)
        public long UserId { get; set; } // user_id
        public string PasswordSalt { get; set; } // password_salt
        public string PasswordHash { get; set; } // password_hash
        public string Status { get; set; } // status
        public DateTime Created { get; set; } // created
        public DateTime? Updated { get; set; } // updated
        // Foreign keys
        public virtual User User { get; set; } // FK_Users_UserPasswords
    }
}