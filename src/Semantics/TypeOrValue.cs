using System;
using Flame.Compiler;
using Flame;

namespace ObjKaleidoscope.Semantics
{
    /// <summary>
    /// Represents the union of a Flame IR type and a semantic analysis value.
    /// </summary>
    public sealed class TypeOrValue
    {
        public TypeOrValue(IValue Value, IType Type)
        {
            this.Value = Value;
            this.Type = Type;
        }
        public TypeOrValue(IValue Value)
            : this(Value, null)
        { } 
        public TypeOrValue(IExpression Value)
            : this(new ExpressionValue(Value), null)
        { } 
        public TypeOrValue(IType Type)
            : this(null, Type)
        { }

        /// <summary>
        /// Gets the type-or-value's value, if any.
        /// </summary>
        public IValue Value { get; private set; }

        /// <summary>
        /// Gets the type-or-value's IR type, if any.
        /// </summary>
        public IType Type { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is a value.
        /// </summary>
        /// <value><c>true</c> if this instance is a value; otherwise, <c>false</c>.</value>
        public bool IsValue { get { return Value != null; } }

        /// <summary>
        /// Gets a value indicating whether this instance is a type.
        /// </summary>
        /// <value><c>true</c> if this instance is a type; otherwise, <c>false</c>.</value>
        public bool IsType { get { return Type != null; } }

        /// <summary>
        /// Checks that the value is non-null, and then creates
        /// a get-expression.
        /// </summary>
        public IExpression CreateGetExpression(ILocalScope Scope)
        {
            if (Value == null)
                throw new Exception("Value was null");

            return Value.CreateGetExpression(Scope);
        }

        /// <summary>
        /// Checks that the type-or-value's value is non-null, 
        /// and then creates a set-statement.
        /// </summary>
        public IStatement CreateSetStatement(IExpression NewValue, ILocalScope Scope)
        {
            if (Value == null)
                throw new Exception("Value was null");

            return Value.CreateSetStatement(NewValue, Scope);
        }

        /// <summary>
        /// Checks that the type is non-null, and returns it.
        /// </summary>
        public IType CheckType(ILocalScope Scope)
        {
            if (Type == null)
                throw new Exception("Type was null");
            
            return Type;
        }
    }
}

