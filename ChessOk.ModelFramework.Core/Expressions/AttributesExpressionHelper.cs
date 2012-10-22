using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ChessOk.ModelFramework.Expressions
{
    public static class AttributesExpressionHelper
    {
        public static string MemberName<T, V>(this Expression<Func<T, V>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
                throw new InvalidOperationException("Expression must be a member expression");

            return memberExpression.Member.Name;
        }

        public static T GetAttribute<T>(this ICustomAttributeProvider provider)
                where T : Attribute
        {
            var attributes = provider.GetCustomAttributes(typeof(T), true);
            return attributes.Length > 0 ? attributes[0] as T : null;
        }

        public static TAttribute GetMethodAttribute<T, V, TAttribute>(this Expression<Func<T, V>> expression)
            where TAttribute : Attribute
        {
            var memberExpression = expression.Body as MethodCallExpression;
            if (memberExpression == null)
                throw new InvalidOperationException("Expression must be a member expression");

            return memberExpression.Method.GetAttribute<TAttribute>();
        }

        public static IEnumerable<TAttributes> GetMethodAttributes<T, V, TAttributes>(this Expression<Func<T, V>> expression)
            where TAttributes : Attribute
        {
            var memberExpression = expression.Body as MethodCallExpression;
            if (memberExpression == null)
                throw new InvalidOperationException("Expression must be a member expression");

            return memberExpression.Method.GetCustomAttributes(typeof(TAttributes), true).Cast<TAttributes>();
        }
    }
}
