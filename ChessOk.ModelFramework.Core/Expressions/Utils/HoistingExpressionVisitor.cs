// -----------------------------------------------------------------------
// <copyright file="HoistingExpressionVisitor.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ChessOk.ModelFramework.Expressions
{
    internal sealed class HoistingExpressionVisitor<TIn, TOut> : ExpressionVisitor
    {
// ReSharper disable StaticFieldInGenericType
        private static readonly ParameterExpression _hoistedConstantsParamExpr = Expression.Parameter(typeof(List<object>), "hoistedConstants");
// ReSharper restore StaticFieldInGenericType
        private int _numConstantsProcessed;

        static HoistingExpressionVisitor()
        {
        }

        private HoistingExpressionVisitor()
        {
        }

        public static Expression<Hoisted<TIn, TOut>> Hoist(Expression<Func<TIn, TOut>> expr)
        {
            return Expression.Lambda<Hoisted<TIn, TOut>>(new HoistingExpressionVisitor<TIn, TOut>().Visit(expr.Body), new []
            {
                expr.Parameters[0],
                _hoistedConstantsParamExpr
            });
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            return Expression.Convert(Expression.Property(_hoistedConstantsParamExpr, "Item", new Expression[]
                {
                    Expression.Constant(_numConstantsProcessed++)
                }), node.Type);
        }
    }
}
