using System;

namespace ObjKaleidoscope.Semantics
{
    /// <summary>
    /// A common interface for local scopes.
    /// </summary>
    public interface ILocalScope
    {
        /// <summary>
        /// Gets the local variable with the given name, if
        /// such a variable exists. Otherwise, returns null.
        /// </summary>
        /// <returns>
        /// The local variable with the given name, if there is such a variable; 
        /// otherwise, null.</returns>
        /// <param name="Name">The name of the variable to find.</param>
        IValue GetLocal(string Name);
    }
}

