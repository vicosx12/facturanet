using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Transform;

namespace Facturanet.NHUtil
{
    public static class IQueryExtensions
    {
        /*
         * Al parecer no se soporta este tipo de consultas, por lo tanto no 
         * me sirve el AliasToBeanResultTransformer
                select 
                    accountTree.Active as Active, 
                    accountTree.Code as Code, 
                    accountTree.Description as Description, 
                    accountTree.Id as Id, 
                    accountTree.Name as Name 
                from 
                    AccountTree as accountTree
         
        public static IEnumerable<T> ToDTOEnumerable<T>(this IQuery query)
            where T : new()
        {
            return
                query
                .SetResultTransformer(new NHibernate.Transform.AliasToBeanResultTransformer(typeof(T)))
                .SetReadOnly(true)
                .Enumerable<T>();
        }
         
        public static List<T> ToDTOList<T>(this IQuery query)
            where T : new()
        {
            return query.ToDTOEnumerable<T>().ToList<T>();
        }
        */

        public static IEnumerable<T> ToDTOEnumerable<T>(this IQuery query, string[] positionalAliases, params object[] constants)
            where T : new()
        {
            if (IsIEditableUIObject<T>())
            {
                var positionalAliasestmp = new List<string>(positionalAliases);
                var constantstmp = new List<object>(constants);

                if (!positionalAliasestmp.Contains("IsNew"))
                {
                    positionalAliasestmp.Add("IsNew");
                    constantstmp.Add(false);
                }

                positionalAliases = positionalAliasestmp.ToArray();
                constants = constantstmp.ToArray();
            }

            return query
                .SetResultTransformer(new PositionalToBeanResultTransformer(typeof(T), positionalAliases, constants))
                .SetReadOnly(true)
                .Enumerable<T>();
        }

        public static IEnumerable<T> ToDTOEnumerable<T>(this IQuery query, string positionalAliases, params object[] constants)
            where T : new()
        {
            return query.ToDTOEnumerable<T>(
                (from item in positionalAliases.Split(',') select item.Trim())
                .ToArray(), constants);
        }

        private static bool IsIEditableUIObject<T>()
        {
            return (typeof(T).GetInterfaces().Contains(typeof(UI.IEditableUIObject)));
        }

        public static IEnumerable<T> ToDTOEnumerable<T>(this IQuery query, Func<object[], T> transformation)
        {
            Func<object[], T> realtransformation;

            if (IsIEditableUIObject<T>())
            {
                realtransformation = tuple =>
                {
                    UI.IEditableUIObject o = (UI.IEditableUIObject)transformation(tuple);
                    o.IsNew = false;
                    return (T)o;
                };
            }
            else
                realtransformation = transformation;

            return query
                .SetResultTransformer(new GenericResultTransformer<T>(realtransformation))
                .SetReadOnly(true)
                .Enumerable<T>();
        }
    }
}
