using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ResourceService
{
    public class RegionSearchExpressionProvider : DefaultSearchExpressionProvider
    {

        private const string GreaterThanOperator = "gt";
        private const string GreaterThanEqualToOperator = "gte";
        private const string LessThanOperator = "lt";
        private const string LessThanEqualToOperator = "lte";
        private const string NotEqualTo = "neq";

        public override ConstantExpression GetValue( string input )
        {
            if ( !int.TryParse( input, out var i ) )
                throw new ArgumentException( "Invalid integer search value {input}" );

            return Expression.Constant( i );
        }


        public override IEnumerable<string> GetOperators()
         => base.GetOperators()
         .Concat( new[]
         {
                GreaterThanOperator,
                GreaterThanEqualToOperator,
                LessThanOperator,
                LessThanEqualToOperator,
                NotEqualTo
         } );

        public override Expression GetComparison( MemberExpression left, string op, ConstantExpression right )
        {

            switch ( op.ToLower() )
            {
                case GreaterThanOperator:
                    return Expression.GreaterThan( left, right );
                case GreaterThanEqualToOperator:
                    return Expression.GreaterThanOrEqual( left, right );
                case LessThanOperator:
                    return Expression.LessThan( left, right );
                case LessThanEqualToOperator:
                    return Expression.LessThanOrEqual( left, right );
                case NotEqualTo:
                    return Expression.NotEqual( left, right );

                default:
                    return base.GetComparison( left, op, right );
            }

        }

    }
}
