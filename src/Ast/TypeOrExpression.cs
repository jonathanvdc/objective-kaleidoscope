using System;
using Flame.Compiler;
using Flame;

namespace ObjKaleidoscope.Ast
{
    /// <summary>
    /// Represents the union of a Flame IR type, expression and variable.
    /// </summary>
    public sealed class TypeOrExpression
    {
        public TypeOrExpression(IExpression Expression, IVariable Variable, IType Type)
        {
            this.Expression = Expression;
            this.Variable = Variable;
            this.Type = Type;
        }
        public TypeOrExpression(IExpression Expression)
            : this(Expression, null, null)
        { } 
        public TypeOrExpression(IVariable Variable)
            : this(null, Variable, null)
        { }
        public TypeOrExpression(IType Type)
            : this(null, null, Type)
        { }

        /// <summary>
        /// Gets the type-or-expression's IR expression.
        /// </summary>
        public IExpression Expression { get; private set; }

        /// <summary>
        /// Gets the type-or-expression's IR variable.
        /// </summary>
        public IVariable Variable { get; private set; }

        /// <summary>
        /// Gets the type-or-expression's IR type.
        /// </summary>
        public IType Type { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is an expression.
        /// </summary>
        /// <value><c>true</c> if this instance is an expression; otherwise, <c>false</c>.</value>
        public bool IsExpression { get { return Expression != null; } }

        /// <summary>
        /// Gets a value indicating whether this instance is a variable.
        /// </summary>
        /// <value><c>true</c> if this instance is a variable; otherwise, <c>false</c>.</value>
        public bool IsVariable { get { return Variable != null; } }

        /// <summary>
        /// Gets a value indicating whether this instance is a type.
        /// </summary>
        /// <value><c>true</c> if this instance is a type; otherwise, <c>false</c>.</value>
        public bool IsType { get { return Type != null; } }

        /// <summary>
        /// Checks that the expression is non-null, and returns it.
        /// If the expression is null, and the variable is not,
        /// then an attempt will be made to create a get-expression
        /// for the variable instead.
        /// </summary>
        public IExpression CheckExpression(LocalScope Scope)
        {
            if (Expression != null)
                return Expression;

            if (Variable != null)
            {
                var getExpr = Variable.CreateGetExpression();
                if (getExpr != null)
                    return getExpr;
            }

            throw new Exception("Expression was null");
        }

        /// <summary>
        /// Checks that the variable is non-null, and returns it.
        /// </summary>
        public IVariable CheckVariable(LocalScope Scope)
        {
            if (Variable == null)
                throw new Exception("Variable was null");

            return Variable;
        }

        /// <summary>
        /// Checks that the type is non-null, and returns it.
        /// </summary>
        public IType CheckType(LocalScope Scope)
        {
            if (Type == null)
                throw new Exception("Type was null");
            
            return Type;
        }
    }
}

