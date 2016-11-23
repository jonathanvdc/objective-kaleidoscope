using System;
using ObjKaleidoscope.Semantics;
using Flame.Compiler.Expressions;

namespace ObjKaleidoscope.Ast
{
    /// <summary>
    /// An integer literal AST node.
    /// </summary>
    public class IntLiteral : IExpressionNode
    {
        public IntLiteral(Token Value)
        {
            this.Value = Value;
        }

        /// <summary>
        /// Gets the value of this integer literal, as a token.
        /// </summary>
        /// <value>The integer literal's value.</value>
        public Token Value { get; private set; }

        /// <inheritdoc/>
        public TypeOrValue Analyze(LocalScope Scope)
        {
            int result;
            if (!int.TryParse(Value.Contents, out result))
            {
                throw new Exception("invalid integer literal '" + Value.Contents + "'.");
            }

            // Wrap the integer in an expression, and create a 
            // TypeOrValue instance from that.
            return new TypeOrValue(new Int32Expression(result));
        }
    }
}

