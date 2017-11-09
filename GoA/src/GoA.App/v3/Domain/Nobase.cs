
namespace GoA.App.v3.Domain
{
    using System;
    public abstract class BaseNode
    {
        public Guid Id { get; private set; }
        public string Information { get; protected set; }
        public BaseNode PositiveQuestion { get; protected set; }
        public BaseNode NegativeQuestion { get; protected set; }
        public BaseNode()
        {
            Id = Guid.NewGuid();
        }

        public BaseNode WithPositiveQuestion(BaseNode sim)
        {
            PositiveQuestion = sim;
            return this;
        }

        public BaseNode WithNegativeQuestion(BaseNode nao)
        {
            NegativeQuestion = nao;
            return this;
        }
        public override bool Equals(object obj)
        {
            if(obj == null)
                return false;

            var no = (BaseNode)obj;
            return this.Id.Equals(no.Id);
        }
    }
}
