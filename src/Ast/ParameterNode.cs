using System;
using Flame;
using Flame.Build;
using ObjKaleidoscope.Semantics;

namespace ObjKaleidoscope.Ast
{
    /// <summary>
    /// An AST node that represents a parameter declaration.
    /// </summary>
    public sealed class ParameterNode
    {
        public ParameterNode(
            Token Identifier,
            IExpressionNode Type)
        {
            this.Identifier = Identifier;
            this.Type = Type;
        }

        /// <summary>
        /// Gets the identifier token that names the parameter.
        /// </summary>
        public Token Identifier { get; private set; }

        /// <summary>
        /// Gets the parameter's type, as an expression AST node.
        /// </summary>
        public IExpressionNode Type { get; private set; }
        
        /// <summary>
        /// Creates an IR parameter specification for this
        /// parameter declaration.
        /// </summary>
        public IParameter ToParameter(LocalScope Scope)
        {
            return new DescribedParameter(
                Identifier.Contents, 
                Type.Analyze(Scope).CheckType(Scope));
        }
    }
}

