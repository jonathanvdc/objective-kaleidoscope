using System;
using Flame.Compiler;

namespace ObjKaleidoscope.Ast
{
    /// <summary>
    /// A common interface for expression AST nodes.
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
        /// A Flame IR type, expression or variable.
        /// </returns>
        TypeOrExpression Analyze(LocalScope Scope);
    }
}

