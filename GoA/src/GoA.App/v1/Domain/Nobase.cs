
namespace GoA.App.v1.Domain
{
    using System;
    public abstract class Nobase
    {
        public Guid Id { get; private set; }
        public string Informacao { get; protected set; }
        public Nobase RespostaPositiva { get; protected set; }
        public Nobase RespostaNegativa { get; protected set; }
        public Nobase()
        {
            Id = Guid.NewGuid();
        }

        public Nobase AtribuirRespostaPositiva(Nobase sim)
        {
            RespostaPositiva = sim;
            return this;
        }

        public Nobase AtribuirRespostaNegativa(Nobase nao)
        {
            RespostaNegativa = nao;
            return this;
        }
        public override bool Equals(object obj)
        {
            if(obj == null)
                return false;

            var no = (Nobase)obj;
            return this.Id.Equals(no.Id);
        }
    }
}
