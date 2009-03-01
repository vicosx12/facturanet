using System;
using System.Collections;
using NHibernate;
using NHibernate.Properties;
using NHibernate.Transform;

namespace Facturanet.NHUtil
{
    /// <summary>
    /// Based on uNhAddIns.Transform.PositionalToBeanResultTransformer (http://code.google.com/p/unhaddins/)
    /// </summary>
    [Serializable]
    public class PositionalToBeanResultTransformerGeneric<T> : IResultTransformer
        where T : new()
    {
        private readonly Type resultClass;
        private readonly string[] positionalAliases;
        private ISetter[] setters;
        private readonly IPropertyAccessor propertyAccessor;

        /// <summary>
        /// Initializes a new instance of the transformer class.
        /// </summary>
        public PositionalToBeanResultTransformerGeneric(string[] positionalAliases)
        {
            resultClass = typeof(T);
            if (positionalAliases == null || positionalAliases.Length == 0)
            {
                throw new ArgumentNullException("positionalAliases");
            }
            this.positionalAliases = positionalAliases;

            propertyAccessor =
                new ChainedPropertyAccessor(new[]
                	{
                		PropertyAccessorFactory.GetPropertyAccessor("field"),
                		PropertyAccessorFactory.GetPropertyAccessor(null)
                	});
            AssignSetters();
        }

        private void AssignSetters()
        {
            setters = new ISetter[positionalAliases.Length];
            for (int i = 0; i < positionalAliases.Length; i++)
            {
                string alias = positionalAliases[i].Trim();
                if (!string.IsNullOrEmpty(alias))
                {
                    setters[i] = propertyAccessor.GetSetter(resultClass, alias);
                }
            }
        }

        #region IResultTransformer Members

        public IList TransformList(IList collection)
        {
            return collection;
        }

        public object TransformTuple(object[] tuple, string[] aliases)
        {
            T result = new T();
            try
            {
                for (int i = 0; i < setters.Length; i++)
                    if (setters[i] != null)
                        setters[i].Set(result, tuple[i]);
            }
            catch (IndexOutOfRangeException)
            {
                throw new HibernateException("Tuple have less scalars then trasformer class: " + resultClass.FullName);
            }

            return result;
        }
        #endregion
    }
}