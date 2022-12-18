using SqlKata.Extensions;


using skta = SqlKata;

namespace OSRS.Persistance.Extensions.Dapper.Queries
{
    public static class ProductQueryExtensions
    {
        public const string TbProducts = "Products as P";

        public static skta.Query SelectAll(this skta.Query query)
        {
            return query.ForSqlServer(q => q.Select("P.{Id, Name, Price}"));
        }
    }
}
