using System;
using ObjKaleidoscope.Semantics;
using Flame.Compiler.Expressions;

namespace ObjKaleidoscope.Ast
{
    /// <summary>
    /// A double literal AST node.
    /// </summary>
    public class DoubleLiteral : IExpressionNode
    {
        public DoubleLiteral(Token Value)
        {
            this.Value = Value;
        }

        /// <summary>
        /// Gets the value of this double literal, as a token.
        /// </summary>
        /// <value>The double literal's value.</value>
        public Token Value { get; private set; }

        /// <inheritdoc/>
        public TypeOrValue Analyze(LocalScope Scope)
        {
            double result;
            if (!double.TryParse(Value.Contents, out result))
            {
                throw new Exception(
                    "invalid double-precision floating point literal '" + 
                    Value.Contents + "'.");
            }

            // Wrap the double in an expression, and create a 
            // TypeOrValue instance from that.
            return new TypeOrValue(new Float64Expression(result));
        }
    }
}

