using System;
using Flame.Compiler;

namespace ObjKaleidoscope.Semantics
{
    /// <summary>
    /// Represents a value, which can be retrieved or stored to.
    /// </summary>
    public interface IValue
    {
        /// <summary>
        /// Creates an expression that retrieves this value.
        /// </summary>
        /// <returns>An IR expression that retrieves a value.</returns>
        /// <param name="Scope">The local scope.</param>
        IExpression CreateGetExpression(LocalScope Scope);


        /// <summary>
        /// Creates a statement that stores the given value in this
        /// storage location.
        /// </summary>
        /// <returns>An IR statement that performs a store.</returns>
        /// <param name="Value">The value to store in this storage location.</param>
        /// <param name="Scope">The local scope.</param>
        IStatement CreateSetStatement(IExpression Value, LocalScope Scope);
    }
}

