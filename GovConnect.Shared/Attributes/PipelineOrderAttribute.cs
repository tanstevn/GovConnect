namespace GovConnect.Shared.Attributes {
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class PipelineOrderAttribute: Attribute {
        public short Order { get; }

        public PipelineOrderAttribute(short order)
        {
            Order = order;
        }
    }
}
