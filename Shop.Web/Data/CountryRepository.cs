

namespace Shop.Web.Data
{
    using Entities;
    using System;
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        public CountryRepository(DataContext context): base(context)
        {

        }
    }
}
