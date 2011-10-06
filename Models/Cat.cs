using System.Collections.Generic;

namespace NHibernateExample.Models
{
    public class Cat
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual decimal Weight { get; set; }
        public virtual bool IsSpadeOrNeutered { get; set; }

        public virtual Owner Owner { get; set; }

        public virtual IList<Toy> Toys { get; set; }
    }
}
