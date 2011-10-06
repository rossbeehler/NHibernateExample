
namespace NHibernateExample.Models.Mappings
{
    public class CatMap : FluentNHibernate.Mapping.ClassMap<Cat>
    {
        public CatMap()
        {
            Table("CAT");

            Id(x => x.Id, "CAT_ID").GeneratedBy.Identity();

            Map(x => x.Name, "NAME");
            Map(x => x.Gender, "SEX").CustomType<Gender>();
            Map(x => x.Weight, "WEIGHT");
            Map(x => x.IsSpadeOrNeutered, "SPADE_NEUTERED");

            References(x => x.Owner, "OWNER_ID")
                .NotFound.Ignore();

            HasMany(x => x.Toys)
                .KeyColumn("CAT_ID")
                .LazyLoad();
        }
    }
}
