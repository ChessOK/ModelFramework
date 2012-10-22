﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ChessOk.ModelFramework.Expressions
{
    public static class ExpressionTextHelper
    {
        public static string GetExpressionText(string expression)
        {
            return !string.Equals(expression, "model", StringComparison.OrdinalIgnoreCase) ? expression : string.Empty;
        }

        public static string GetExpressionText(LambdaExpression expression)
        {
            var stack = new Stack<string>();
            var expression1 = expression.Body;
            while (expression1 != null)
            {
                if (expression1.NodeType == ExpressionType.Call)
                {
                    var methodCallExpression = (MethodCallExpression)expression1;
                    if (IsSingleArgumentIndexer(methodCallExpression))
                    {
                        stack.Push(GetIndexerInvocation(methodCallExpression.Arguments.Single(), expression.Parameters.ToArray()));
                        expression1 = methodCallExpression.Object;
                    }
                    else
                        break;
                }
                else if (expression1.NodeType == ExpressionType.ArrayIndex)
                {
                    var binaryExpression = (BinaryExpression)expression1;
                    stack.Push(GetIndexerInvocation(binaryExpression.Right, expression.Parameters.ToArray()));
                    expression1 = binaryExpression.Left;
                }
                else if (expression1.NodeType == ExpressionType.MemberAccess)
                {
                    var memberExpression = (MemberExpression)expression1;
                    stack.Push("." + memberExpression.Member.Name);
                    expression1 = memberExpression.Expression;
                }
                else if (expression1.NodeType == ExpressionType.Parameter)
                {
                    stack.Push(string.Empty);
                    expression1 = null;
                }
                else
                    break;
            }
            if (stack.Count > 0 && string.Equals(stack.Peek(), ".model", StringComparison.OrdinalIgnoreCase))
                stack.Pop();
            if (stack.Count <= 0)
                return string.Empty;
            return stack.Aggregate((left, right) => left + right).TrimStart(new[] { '.' });
        }

        private static string GetIndexerInvocation(Expression expression, IList<ParameterExpression> parameters)
        {
            var lambdaExpression = Expression.Lambda<Func<object, object>>(Expression.Convert(expression, typeof(object)), new[]
      {
        Expression.Parameter(typeof (object), null)
      });
            Func<object, object> func;
            try
            {
                func = CachedExpressionCompiler.CompileGetter(lambdaExpression);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Invalid indexer expression: {0} {1}", new object[]
        {
          expression,
          parameters[0].Name
        }), ex);
            }
            return "[" + Convert.ToString(func(null), CultureInfo.InvariantCulture) + "]";
        }

        internal static bool IsSingleArgumentIndexer(Expression expression)
        {
            var methodExpression = expression as MethodCallExpression;
            if (methodExpression == null || methodExpression.Arguments.Count != 1)
                return false;

            return methodExpression.Method.DeclaringType.GetDefaultMembers().OfType<PropertyInfo>().Any(p => p.GetGetMethod() == methodExpression.Method);
        }
    }
}
