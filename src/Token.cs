using System;
using System.Collections.Generic;

namespace ObjKaleidoscope
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
        /// An operator.
        /// </summary>
        Operator,
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
        /// A '(' string.
        /// </summary>
        LeftParen,
        /// <summary>
        /// A ')' string.
        /// </summary>
        RightParen,
        /// <summary>
        /// A ';' string.
        /// </summary>
        Semicolon,
        /// <summary>
        /// A ',' string.
        /// </summary>
        Comma,
        /// <summary>
        /// A '.' string.
        /// </summary>
        Dot,
        /// <summary>
        /// An '=' string.
        /// </summary>
        Eq
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

        public override string ToString()
        {
            return "(" + Kind + ", \"" + Contents + "\")";
        }

        static Token()
        {
            keywordMap = new Dictionary<string, TokenKind>()
            {
                { "def", TokenKind.DefKeyword },
                { "var", TokenKind.VarKeyword },
                { "class", TokenKind.ClassKeyword }
            };
            opMap = new Dictionary<string, TokenKind>()
            {
                { ";", TokenKind.Semicolon },
                { "=", TokenKind.Eq },
                { ",", TokenKind.Comma },
                { ".", TokenKind.Dot },
                { "(", TokenKind.LeftParen },
                { ")", TokenKind.RightParen }
            };
        }

        // A dictionary that maps strings to the keyword token
        // kinds they correspond to.
        private static Dictionary<string, TokenKind> keywordMap;

        // A dictionary that maps strings to the operator token
        // kinds they correspond to.
        private static Dictionary<string, TokenKind> opMap;

        /// <summary>
        /// Classifies the given character string as an identifier
        /// or a keyword.
        /// </summary>
        public static TokenKind ClassifyIdentifierOrKeyword(string Value)
        {
            TokenKind result;
            if (keywordMap.TryGetValue(Value, out result))
                return result;
            else
                return TokenKind.Identifier;
        }

        /// <summary>
        /// Classifies the given character string as an operator 
        /// or a special token.
        /// </summary>
        public static TokenKind ClassifyOperatorOrSpecial(string Value)
        {
            TokenKind result;
            if (opMap.TryGetValue(Value, out result))
                return result;
            else
                return TokenKind.Operator;
        }

        /// <summary>
        /// Gets a token that represents the end-of-file
        /// marker.
        /// </summary>
        public static Token EndOfFile
        {
            get { return new Token(TokenKind.EndOfFile, ""); }
        }

        /// <summary>
        /// Tests if the given token kind is used for trivia
        /// tokens: tokens that are important to the lexing
        /// process, but can be safely ignored afterward.
        /// Whitespace and comments are trivia.
        /// </summary>
        public static bool IsTrivia(TokenKind Kind)
        {
            return Kind == TokenKind.Whitespace
                || Kind == TokenKind.Comment;
        }
    }
}

