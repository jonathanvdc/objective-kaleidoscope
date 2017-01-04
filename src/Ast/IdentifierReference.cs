using System;
using ObjKaleidoscope.Semantics;
using Flame.Compiler.Expressions;

namespace ObjKaleidoscope.Ast
{
    /// <summary>
    /// An AST node that references an identifier. 
    /// </summary>
    public sealed class IdentifierReference : IExpressionNode
    {
        public IdentifierReference(Token Identifier)
        {
            this.Identifier = Identifier;
        }

        /// <summary>
        /// Gets the identifier that is referenced, as a token.
        /// </summary>
        /// <value>The identifier that is referenced.</value>
        public Token Identifier { get; private set; }

        /// <inheritdoc/>
        public TypeOrValue Analyze(ILocalScope Scope)
        {
            string name = Identifier.Contents;

            // TODO: handle types, functions and globals
            return new TypeOrValue(Scope.GetLocal(name));
        }
    }
}

