namespace Net_4._0_Extentions
{
    #region Using

    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    #endregion

    public static class PropertyUtil
    {
        /* public static string GetPropertyName<TObject>(this TObject type,
                                                       Expression<Func<TObject, object>> propertyRefExpr)
        {
            return GetPropertyNameCore(propertyRefExpr.Body);
        }*/

        public static string GetName<T>(Expression<Func<T>> property)
        {
            return ((MemberExpression)property.Body).Member.Name;
        }

        public static string GetName<TObject>(this Expression<Func<TObject, object>> propertyRefExpr)
        {
            return GetPropertyNameCore(propertyRefExpr.Body);
        }

        private static string GetPropertyNameCore(Expression propertyRefExpr)
        {
            if (propertyRefExpr == null)
            {
                throw new ArgumentNullException("propertyRefExpr", @"propertyRefExpr is null.");
            }

            MemberExpression memberExpr = propertyRefExpr as MemberExpression;
            if (memberExpr == null)
            {
                UnaryExpression unaryExpr = propertyRefExpr as UnaryExpression;
                if (unaryExpr != null && unaryExpr.NodeType == ExpressionType.Convert)
                {
                    memberExpr = unaryExpr.Operand as MemberExpression;
                }
            }

            if (memberExpr != null && memberExpr.Member.MemberType == MemberTypes.Property)
            {
                return memberExpr.Member.Name;
            }

            throw new ArgumentException(@"No property reference expression was found.", "propertyRefExpr");
        }
    }
}