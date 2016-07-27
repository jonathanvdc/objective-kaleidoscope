using System;
using Flame.Compiler;

namespace ObjKaleidoscope.Ast
{
    /// <summary>
    /// A base class for expression AST nodes.
    /// </summary>
    public interface IExpressionNode
    {
        /// <summary>
        /// Analyzes this AST node and its children.
        /// </summary>
        /// <param name="Scope">
        /// The expression's enclosing local scope.
        /// </param>
        /// <returns>
        /// A Flame IR expression node.
        /// </returns>
        IExpression Analyze(LocalScope Scope);
    }
}

