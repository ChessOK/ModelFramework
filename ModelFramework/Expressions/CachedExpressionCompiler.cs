using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace ChessOk.ModelFramework.Expressions
{
    public static class CachedExpressionCompiler
    {
        public static Func<TModel, TValue> CompileGetter<TModel, TValue>(Expression<Func<TModel, TValue>> lambdaExpression)
        {
            return GetCompiler<TModel, TValue>.Compile(lambdaExpression);
        }

        public static Action<TModel, TValue> CompileSetter<TModel, TValue> (Expression<Func<TModel, TValue>> lambdaExpression)
        {
            return SetCompiler<TModel, TValue>.Compile(lambdaExpression);
        }

        private static class SetCompiler<TIn, TOut>
        {
            private static readonly ConcurrentDictionary<PropertyInfo, Action<TIn, TOut>> SimplePropertyAccessDict =
                new ConcurrentDictionary<PropertyInfo, Action<TIn, TOut>>();

            static SetCompiler()
            {
            }

            public static Action<TIn, TOut> Compile(Expression<Func<TIn, TOut>> expr)
            {
                var member = (MemberExpression)expr.Body;
                if (member == null)
                {
                    throw new ArgumentException();
                }

                var propertyInfo = member.Member as PropertyInfo;
                if (propertyInfo == null)
                {
                    throw new ArgumentException();
                }

                return SimplePropertyAccessDict.GetOrAdd(propertyInfo, _ => CompileFromPropertyInfo(propertyInfo));
            }

            private static Action<TIn, TOut> CompileFromPropertyInfo(PropertyInfo propertyInfo)
            {
                if (propertyInfo == null)
                {
                    throw new ArgumentNullException("propertyInfo");
                }

                if (!propertyInfo.DeclaringType.IsAssignableFrom(typeof(TIn)))
                {
                    throw new ArgumentException();
                }

                var instance = Expression.Parameter(propertyInfo.DeclaringType, "i");
                var argument = Expression.Parameter(typeof(TOut), "a");
                var setterCall = Expression.Call(
                    instance,
                    propertyInfo.GetSetMethod(),
                    Expression.Convert(argument, propertyInfo.PropertyType));

                return (Action<TIn, TOut>)Expression.Lambda(setterCall, instance, argument)
                                                    .Compile();
            }
        }

        private static class GetCompiler<TIn, TOut>
        {
            private static readonly ConcurrentDictionary<MemberInfo, Func<TIn, TOut>> SimpleMemberAccessDict =
                new ConcurrentDictionary<MemberInfo, Func<TIn, TOut>>();

            private static readonly ConcurrentDictionary<MemberInfo, Func<object, TOut>> ConstMemberAccessDict =
                new ConcurrentDictionary<MemberInfo, Func<object, TOut>>();

            private static readonly ConcurrentDictionary<ExpressionFingerprintChain, Hoisted<TIn, TOut>>
                FingerprintedCache = new ConcurrentDictionary<ExpressionFingerprintChain, Hoisted<TIn, TOut>>();

            private static Func<TIn, TOut> _identityFunc;

            static GetCompiler()
            {
            }

            public static Func<TIn, TOut> Compile(Expression<Func<TIn, TOut>> expr)
            {
                return CompileFromIdentityFunc(expr)
                       ??
                       CompileFromConstLookup(expr)
                       ?? CompileFromMemberAccess(expr) ?? CompileFromFingerprint(expr) ?? CompileSlow(expr);
            }

            private static Func<TIn, TOut> CompileFromConstLookup(Expression<Func<TIn, TOut>> expr)
            {
                var constantExpression = expr.Body as ConstantExpression;
                if (constantExpression == null) return null;
                var constantValue = (TOut)constantExpression.Value;
                return _ => constantValue;
            }

            private static Func<TIn, TOut> CompileFromIdentityFunc(Expression<Func<TIn, TOut>> expr)
            {
                if (expr.Body != expr.Parameters[0]) return null;
                return _identityFunc ?? (_identityFunc = expr.Compile());
            }

            private static Func<TIn, TOut> CompileFromFingerprint(Expression<Func<TIn, TOut>> expr)
            {
                List<object> capturedConstants;
                ExpressionFingerprintChain fingerprintChain =
                    FingerprintingExpressionVisitor.GetFingerprintChain(expr, out capturedConstants);
                if (fingerprintChain == null) return null;
                Hoisted<TIn, TOut> del =
                    FingerprintedCache.GetOrAdd(
                        fingerprintChain,
                        _ => HoistingExpressionVisitor<TIn, TOut>.Hoist(expr).Compile());
                return model => del(model, capturedConstants);
            }

            private static Func<TIn, TOut> CompileFromMemberAccess(Expression<Func<TIn, TOut>> expr)
            {
                var memberExpr = expr.Body as MemberExpression;
                if (memberExpr != null)
                {
                    if (memberExpr.Expression == expr.Parameters[0] || memberExpr.Expression == null) return SimpleMemberAccessDict.GetOrAdd(memberExpr.Member, _ => expr.Compile());
                    var constantExpression = memberExpr.Expression as ConstantExpression;
                    if (constantExpression != null)
                    {
                        var del = ConstMemberAccessDict.GetOrAdd(
                            memberExpr.Member,
                            _ =>
                                {
                                    var local_0 = Expression.Parameter(typeof(object), "capturedLocal");
                                    return
                                        Expression.Lambda<Func<object, TOut>>(
                                            memberExpr.Update(
                                                Expression.Convert(local_0, memberExpr.Member.DeclaringType)),
                                            new[] { local_0 }).Compile();
                                });
                        object capturedLocal = constantExpression.Value;
                        return _ => del(capturedLocal);
                    }
                }
                return null;
            }

            private static Func<TIn, TOut> CompileSlow(Expression<Func<TIn, TOut>> expr)
            {
                return expr.Compile();
            }
        }
    }

}
