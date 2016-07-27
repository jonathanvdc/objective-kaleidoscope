using System;
using System.Collections.Generic;

namespace ObjKaleidoscope.Ast
{
    public sealed class FunctionNode
    {
        public FunctionNode(
            Token Identifier,
            IReadOnlyList<ParameterNode> Parameters,
            IExpressionNode Body)
        {
            this.Identifier = Identifier;
            this.Parameters = Parameters;
            this.Body = Body;
        }

        /// <summary>
        /// Gets the function's identifier: the name that is
        /// used to refer to this function.
        /// </summary>
        public Token Identifier { get; private set; }

        /// <summary>
        /// Gets the function's parameter list.
        /// </summary>
        public IReadOnlyList<ParameterNode> Parameters { get; private set; }

        /// <summary>
        /// Gets the function body, which is an expression.
        /// </summary>
        public IExpressionNode Body { get; private set; }
    }
}

