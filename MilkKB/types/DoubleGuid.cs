namespace MilkKB.types
{
    public struct DoubleGuid
    {
        public Guid Id1 { get; set; }
        public Guid Id2 { get; set; }

        public DoubleGuid()
        {
            Id1 = Guid.NewGuid();
            Id2 = Guid.NewGuid();
        }

        public DoubleGuid(string id1, string id2)
        {
            Id1 = new Guid(id1);
            Id2 = new Guid(id2);
        }
    }
}

