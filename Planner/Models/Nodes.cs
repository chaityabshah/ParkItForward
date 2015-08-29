using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Planner.Models.Database;

namespace Planner.Models
{
    public class Nodes
    {
        //private static long? userId;

        public static IEnumerable<Node> GetAllNodes(long userId)
        {
            var dbContext = new PlannerDbContext();

            dbContext.Configuration.ProxyCreationEnabled = false;
            var nodes = dbContext.Set<Database.Node>().Where(_ => _.UserId == userId && _.Status == "ACTIVE").ToList();

            return CreateTree(nodes, null);
        }

        public static IEnumerable<Node> CreateTree(IEnumerable<Database.Node> nodes, long? parentNodeId)
        {
            var nodeTree = new List<Node>();

            var childNodes = nodes.Where(o => o.ParentNodeId == parentNodeId).ToList();

            foreach (var childNode in childNodes)
            {
                var n = new Node();
                var children = CreateTree(nodes, childNode.Id);
                n.Name = childNode.Name;
                n.Type = childNode.Type;
                n.Id = childNode.Id;
                n.ChildNodes = children;
                nodeTree.Add(n);
            }

            return nodeTree;
        }

        [JsonObject(MemberSerialization.OptIn)]
        public class Node
        {
            public long? ParentNodeId;
            public long? UserId { get; set; }
            public long Id { get; set; }

            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }

            [JsonProperty(PropertyName = "type")]
            public string Type { get; set; }

            [JsonProperty(PropertyName = "children")]
            public IEnumerable<Node> ChildNodes { get; set; }

            public string Status { get; set; }
        }
    }
}