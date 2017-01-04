using System;
using Flame.Compiler;

namespace ObjKaleidoscope.Semantics
{
    /// <summary>
    /// A simple value implementation that wraps a 
    /// simple get-expression.
    /// </summary>
    public class ExpressionValue : IValue
    {
        public ExpressionValue(IExpression GetExpression)
        {
            this.GetExpression = GetExpression;
        }

        /// <summary>
        /// Gets the expression that is used as the get-expression.
        /// </summary>
        /// <value>The get-expression.</value>
        public IExpression GetExpression { get; private set; }

        /// <inheritdoc/>
        public IExpression CreateGetExpression(ILocalScope Scope)
        {
            return GetExpression;
        }

        /// <inheritdoc/>
        public IStatement CreateSetStatement(IExpression Value, ILocalScope Scope)
        {
            throw new Exception("cannot store a value here");
        }
    }
}

