using System;
using JetBrains.Annotations;

namespace MicroFlow
{
    public abstract class FlowNode : IFlowNode
    {
        private string _name;

        public abstract FlowNodeKind Kind { get; }

        public Guid Id { get; } = Guid.NewGuid();

        public string Name
        {
            get { return _name; }
            set
            {
                value.AssertNotNullOrEmpty("Name cannot be null or empty");
                _name.AssertIsNull("Name is already set");

                _name = value;
            }
        }

        public abstract TResult Accept<TResult>(INodeVisitor<TResult> visitor);

        public abstract void RemoveConnections();

        public override string ToString()
        {
            return $"{{Node Kind: '{Kind}' Name: '{_name}'}}";
        }
    }

    public static class FlowNodeExtensions
    {
        public static TFlowNode WithName<TFlowNode>([NotNull] this TFlowNode node, [NotNull] string name)
            where TFlowNode : FlowNode
        {
            node.NotNull().Name = name;
            return node;
        }
    }
}