using System;

namespace objectivekaleidoscope
{
    /// <summary>
    /// An enumeration of possible token kinds.
    /// </summary>
    public enum TokenKind
    {
        /// <summary>
        /// The "undefined" token kind. Tokens of the
        /// "undefined" kind were not recognized by the lexer.
        /// </summary>
        Undefined,
        /// <summary>
        /// A special token that marks the end of a file.
        /// </summary>
        EndOfFile,
        /// <summary>
        /// The token kind for whitespace.
        /// </summary>
        Whitespace,
        /// <summary>
        /// The token kind for comments.
        /// </summary>
        Comment,
        /// <summary>
        /// An identifier.
        /// </summary>
        Identifier,
        /// <summary>
        /// An integer number.
        /// </summary>
        Integer,
        /// <summary>
        /// A 64-bit floating point number.
        /// </summary>
        Float64,
        /// <summary>
        /// The 'def' keyword, which defines a function.
        /// </summary>
        DefKeyword,
        /// <summary>
        /// The 'var' keyword, which defines a variable, 
        /// or field, depending on the context.
        /// </summary>
        VarKeyword,
        /// <summary>
        /// The 'class' keyword, which defines a class.
        /// </summary>
        ClassKeyword,
        /// <summary>
        /// A ':' string.
        /// </summary>
        Colon,
        /// <summary>
        /// An '=' string.
        /// </summary>
        Eq,
        /// <summary>
        /// A '+' string.
        /// </summary>
        Plus,
        /// <summary>
        /// A '-' string.
        /// </summary>
        Minus,
        /// <summary>
        /// An '*' string.
        /// </summary>
        Asterisk,
        /// <summary>
        /// A '/' string.
        /// </summary>
        Slash,
        /// <summary>
        /// A '!' string.
        /// </summary>
        Not,
        /// <summary>
        /// An '==' string. 
        /// </summary>
        EqEq,
        /// <summary>
        /// A '!=' string.
        /// </summary>
        NotEq,
        /// <summary>
        /// A '&gt;' string.
        /// </summary>
        Gt,
        /// <summary>
        /// A '&lt;' string.
        /// </summary>
        Lt,
        /// <summary>
        /// A '&gt;=' string.
        /// </summary>
        GtEq,
        /// <summary>
        /// A '&lt;=' string.
        /// </summary>
        LtEq
    }

    /// <summary>
    /// Defines a type for tokens.
    /// </summary>
    public sealed class Token
    {
        public Token(TokenKind Kind, string Contents)
        {
            this.Kind = Kind;
            this.Contents = Contents;
        }

        /// <summary>
        /// Gets this token's kind.
        /// </summary>
        public TokenKind Kind { get; private set; }

        /// <summary>
        /// Gets this token's contents: the substring
        /// of the source document that this token
        /// represents.
        /// </summary>
        public string Contents { get; private set; }
    }
}

