using FluentNHibernate.Mapping;

namespace NHibernateExample.Models.Mappings
{
    public class ToyMap : ClassMap<Toy>
    {
        public ToyMap()
        {
            Table("TOY");

            Id(x => x.Id, "TOY_ID").GeneratedBy.Identity();
        }
    }
}
