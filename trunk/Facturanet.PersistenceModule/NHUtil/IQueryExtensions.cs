using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Facturanet.NHUtil
{
    public static class IQueryExtensions
    {
        public static IEnumerable<T> ToDTOEnumerable<T>(this IQuery query, string[] positionalAliases)
            where T : new()
        {
            return
                query
                .SetResultTransformer(new PositionalToBeanResultTransformerGeneric<T>(positionalAliases))
                .SetReadOnly(true)
                .Enumerable<T>();
        }

        public static IEnumerable<T> ToDTOEnumerable<T>(this IQuery query, string positionalAliases)
            where T : new()
        {
            return query.ToDTOEnumerable<T>(positionalAliases.Split(','));
        }
    }
}
