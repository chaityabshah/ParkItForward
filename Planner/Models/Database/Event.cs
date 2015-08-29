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
    // Events
    public class Event
    {
        public long Id { get; set; } // id (Primary key)
        public long UserId { get; set; } // user_id
        public long? ParentNodeId { get; set; } // parent_node_id
        public string Name { get; set; } // name
        public string Location { get; set; } // location
        public string Description { get; set; } // description
        public string Type { get; set; } // type
        public DateTime Startdate { get; set; } // startdate
        public DateTime? Enddate { get; set; } // enddate
        public string Status { get; set; } // status
        public DateTime Created { get; set; } // created
        public DateTime? Updated { get; set; } // updated
    }
}