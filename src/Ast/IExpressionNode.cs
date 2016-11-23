using System;
using Flame.Compiler;
using ObjKaleidoscope.Semantics;

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
        /// A Flame IR type or a value.
        /// </returns>
        TypeOrValue Analyze(LocalScope Scope);
    }
}

