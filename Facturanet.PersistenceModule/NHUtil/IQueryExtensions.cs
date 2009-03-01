using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Facturanet.NHUtil
{
    public static class IQueryExtensions
    {
        public static List<T> DTOList<T>(this IQuery query, string[] positionalAliases)
            where T : new()
        {
            return
                (List<T>) query
                .SetResultTransformer(new PositionalToBeanResultTransformerGeneric<T>(positionalAliases))
                .SetReadOnly(true)
                .List<T>();
        }

        public static List<T> DTOList<T>(this IQuery query, string positionalAliases)
            where T : new()
        {
            return query.DTOList<T>(positionalAliases.Split(','));
        }
    }
}
