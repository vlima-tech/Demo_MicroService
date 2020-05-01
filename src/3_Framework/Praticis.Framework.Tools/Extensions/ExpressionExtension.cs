
using Remotion.Linq.Parsing.ExpressionVisitors;

namespace System.Linq.Expressions
{
    public static class ExpressionExtension
    {
        public static LambdaExpression ConvertFilterExpression<T>(this Expression<Func<T, bool>> filterExpression, Type entityType)
        {
            var parameter = Expression.Parameter(entityType);
            var newBody = ReplacingExpressionVisitor.Replace(filterExpression.Parameters.Single(), parameter, filterExpression.Body);

            return Expression.Lambda(newBody, parameter);
        }

        public static LambdaExpression ToExpression<T>(this T tipo, Expression<Func<T, bool>> filterExpression)
        {
            var parameter = Expression.Parameter(typeof(T));
            var newBody = ReplacingExpressionVisitor.Replace(filterExpression.Parameters.Single(), parameter, filterExpression.Body);

            return Expression.Lambda(newBody, parameter);
        }
    }
}