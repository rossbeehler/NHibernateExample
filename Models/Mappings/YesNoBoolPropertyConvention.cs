using System;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace NHibernateExample.Models.Mappings
{
    public class YesNoBoolPropertyConvention : IPropertyConvention, IConventionAcceptance<IPropertyInspector>
    {
        public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        {
            criteria.Either(
                sub => sub.Expect(x => x.Property.PropertyType == typeof(bool)),
                sub => sub.Expect(x => x.Property.PropertyType == typeof(Nullable<bool>)));
        }

        public void Apply(IPropertyInstance instance)
        {
            instance.CustomType("YesNo");
        }
    }
}
