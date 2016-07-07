using System;
using System.IO;

namespace ObjKaleidoscope
{
    public class Lexer
    {
        public Lexer(string Source)
        {
            this.source = Source;
            this.pos = 0;
        }

        private string source;
        private int pos;

        /// <summary>
        /// Reads a character string of the given length 
        /// from the source document, without changing the 
        /// position index.
        /// </summary>
        private string PeekString(int Count)
        {
            return source.Substring(pos, Count);
        }

        /// <summary>
        /// Reads a character from the source document, at
        /// the given offset from the position in the document.
        /// </summary>
        private char PeekCharacter(int Offset)
        {
            return source[pos + Offset];
        }

        /// <summary>
        /// Reads a character from the source document.
        /// </summary>
        private char PeekCharacter()
        {
            return PeekCharacter(0);
        }

        /// <summary>
        /// Reads a character string from the source document.
        /// The resulting string is terminated when the end-of-file
        /// is reached, or when the given predicate returns false.
        /// </summary>
        private string PeekWhile(int Offset, Func<char, bool> Predicate)
        {
            int i = pos + Offset;
            while (i < source.Length
               && Predicate(source[i]))
            {
                i++;
            }

            return source.Substring(pos + Offset, i - pos - Offset);
        }

        /// <summary>
        /// Advances through the source document by adding
        /// the given offset to the position index.
        /// </summary>
        private void Advance(int Offset)
        {
            pos += Offset;
        }

        /// <summary>
        /// Determines whether this lexer instance can read the 
        /// specified number of characters from the source document,
        /// starting at the given offset from the current position.
        /// </summary>
        private bool CanReadString(int Offset, int Count)
        {
            return pos + Offset + Count <= source.Length;
        }

        /// <summary>
        /// Determines whether this lexer instance can read the 
        /// specified number of characters from the source document.
        /// </summary>
        private bool CanReadString(int Count)
        {
            return CanReadString(0, Count);
        }

        /// <summary>
        /// Reads a single token from the source document,
        /// without updating the current position in the 
        /// source document.
        /// </summary>
        public Token Peek()
        {
            if (!CanReadString(1))
                return Token.EndOfFile;

            // Look for whitespace
            if (char.IsWhiteSpace(PeekCharacter()))
                return new Token(
                    TokenKind.Whitespace, 
                    PeekWhile(0, char.IsWhiteSpace));

            // Look for comments
            if (PeekCharacter() == '#')
                return new Token(
                    TokenKind.Comment, 
                    PeekWhile(0, c => c != '\n' && c != '\r')); 

            // Classify identifiers and keywords
            if (char.IsLetter(PeekCharacter()))
            {
                string ident = PeekWhile(0, char.IsLetterOrDigit);
                return new Token(
                    Token.ClassifyIdentifierOrKeyword(ident), ident);
            }

            // Parse integer/double literals
            if (char.IsDigit(PeekCharacter()) || PeekCharacter() == '.')
            {
                // Integer literals: [0-9]+
                // Double literals: [0-9]*\.[0-9]+
                string first = PeekWhile(0, char.IsDigit);
                if (CanReadString(first.Length, 1)
                    && PeekCharacter(first.Length) == '.')
                {
                    string second = PeekWhile(
                        first.Length + 1, char.IsDigit);
                    if (second.Length > 0)
                    {
                        return new Token(
                            TokenKind.Float64,
                            first + "." + second);
                    }
                }
                if (first.Length > 0)
                {
                    return new Token(
                        TokenKind.Integer, first);
                }
            }

            // We can also try to parse operators
            string opString = PeekWhile(0, c => 
                !char.IsLetterOrDigit(c) 
                && !char.IsControl(c) 
                && !char.IsWhiteSpace(c));

            if (opString.Length > 0)
            {
                return new Token(
                    Token.ClassifyOperatorOrSpecial(opString),
                    opString);
            }

            return new Token(TokenKind.Undefined, PeekString(1));
        }

        /// <summary>
        /// Reads a single token from the source document.
        /// The current position in the source document
        /// is updated.
        /// </summary>
        public Token Read()
        {
            var result = Peek();
            Advance(result.Contents.Length);
            return result;
        }
    }
}

