using System.Linq;
using System.Linq.Expressions;

namespace DAL.Interfacies.Helper
{
    public class PredicateExpressionVisitor<TSource, TDestination> : ExpressionVisitor
    {
        public ParameterExpression NewParameterExp { get; private set; }

        public PredicateExpressionVisitor(ParameterExpression newParameterExp)
        {
            NewParameterExp = newParameterExp;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return NewParameterExp;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Member.DeclaringType == typeof(TSource))
                return Expression.MakeMemberAccess(this.Visit(node.Expression),
                   typeof(TDestination).GetMember(node.Member.Name).FirstOrDefault());
            return base.VisitMember(node);
        }
    }
}
