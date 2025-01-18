using System.Reflection.Metadata;

namespace Finatech.Infrastructure.Decorators;

using System.Linq;
using System.Linq.Expressions;

public static class ExpressionEvaluator
{
    public static object EvaluateExpression(string expression, object parameter)
    {
        var parameterType = parameter.GetType();
        var paramExpr = Expression.Parameter(parameterType, "param");

        Expression body;
        // Parse the expression (e.g., "param.Id")
        var parts = expression.Split('.');
        if (parts.Length > 1)
        {
            body = Expression.PropertyOrField(paramExpr, parts[1]);
        }
        else
        {
            // Handle the case where the expression is just "param"
            body = paramExpr;
        }

        // Compile and execute
        var lambda = Expression.Lambda(body, paramExpr);
        var compiled = lambda.Compile();
        return compiled.DynamicInvoke(parameter);
    }
}