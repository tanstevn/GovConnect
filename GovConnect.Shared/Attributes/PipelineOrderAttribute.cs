using System.Diagnostics.CodeAnalysis;

namespace GovConnect.Shared.Attributes {
    [ExcludeFromCodeCoverage(Justification = "")]
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class PipelineOrderAttribute: Attribute {
        public short Order { get; }

        public PipelineOrderAttribute(short order)
        {
            Order = order;
        }
    }
}
