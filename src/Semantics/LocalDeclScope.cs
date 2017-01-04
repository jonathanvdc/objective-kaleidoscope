using System;
using System.Collections.Generic;
using System.Linq;

namespace ObjKaleidoscope.Semantics
{
    /// <summary>
    /// A local scope implementation for 
    /// local variable declarations.
    /// </summary>
    public sealed class LocalDeclScope : ILocalScope
    {
        public LocalDeclScope(
            ILocalScope Parent, 
            string VariableName, IValue Variable)
        {
            this.parentScope = Parent;
            this.variableName = VariableName;
            this.variable = Variable;
        }

        // This local scope's parent local scope.
        private ILocalScope parentScope;

        // The name of the variable defined by this scope.
        private string variableName;

        // The value of the variable defined by this scope.
        private IValue variable;

        /// <inheritdoc/>
        public IValue GetLocal(string Name)
        {
            if (variableName == Name)
                return variable;
            else
                return parentScope.GetLocal(Name);
        }
    }
}

