using FluentNHibernate.Mapping;

namespace NHibernateExample.Models.Mappings
{
    public class OwnerMap : ClassMap<Owner>
    {
        public OwnerMap()
        {
            Table("OWNER");

            Id(x => x.Id, "OWNER_ID").GeneratedBy.Identity();

            Map(x => x.Name, "NAME");
        }
    }
}
