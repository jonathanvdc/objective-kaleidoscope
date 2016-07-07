using System;
using System.IO;

namespace ObjKaleidoscope
{
    public class Lexer
    {
        public Lexer(string Source)
        {
            pos = 0;
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
               && char.IsWhiteSpace(source[i]))
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
        /// specified number of characters from the source string.
        /// </summary>
        private bool CanReadString(int Count)
        {
            return pos + Count <= source.Length;
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

            // Next, classify identifiers and keywords
            if (char.IsLetter(PeekCharacter()))
            {
                string ident = PeekWhile(0, char.IsLetterOrDigit);
                return new Token(
                    Token.ClassifyIdentifierOrKeyword(ident), ident);
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

