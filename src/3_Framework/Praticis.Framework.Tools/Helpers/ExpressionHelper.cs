
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Praticis.Framework.Tools.Helpers
{
    public static class ExpressionHelper
    {
        public static Expression<Func<T, TReturn>> Append<T,TReturn>(Expression<Func<T, TReturn>> currentExpression, Expression<Func<T, TReturn>> expressionToAppend, ExpressionType type)
        {
            var visitor = new ParameterUpdateVisitor(expressionToAppend.Parameters.First(), currentExpression.Parameters.First());

            var newExp = visitor.Visit(expressionToAppend) as Expression<Func<T, TReturn>>;
            var binExp = Expression.And(currentExpression.Body, expressionToAppend.Body);

            return Expression.Lambda<Func<T, TReturn>>(binExp, newExp.Parameters);
        }
        /*
        private Expression<Func<T, bool>> AddGlobalFilters<T>(Expression<Func<T, bool>> exp)
        {
            // get the global filter
            Expression<Func<Case, bool>> newExp = c => c.CaseStatusId != (int)CaseStatus.Finished;

            // get the visitor
            var visitor = new ParameterUpdateVisitor(newExp.Parameters.First(), exp.Parameters.First());
            // replace the parameter in the expression just created
            newExp = visitor.Visit(newExp) as Expression<Func<Case, bool>>;

            // now you can and together the two expressions
            var binExp = Expression.And(exp.Body, newExp.Body);
            // and return a new lambda, that will do what you want. NOTE that the binExp has reference only to te newExp.Parameters[0] (there is only 1) parameter, and no other
            return Expression.Lambda<Func<Case, bool>>(binExp, newExp.Parameters);
        }
        */
    }

    internal class ParameterUpdateVisitor : ExpressionVisitor
    {
        private ParameterExpression _oldParameter;
        private ParameterExpression _newParameter;

        public ParameterUpdateVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
        {
            this._oldParameter = oldParameter;
            this._newParameter = newParameter;
        }
        
        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (object.ReferenceEquals(node, _oldParameter))
                return _newParameter;

            return base.VisitParameter(node);
        }
    }
}