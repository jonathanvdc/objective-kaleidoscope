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
        private static string PeekString(int Count)
        {
            return source.Substring(pos, Count);
        }

        /// <summary>
        /// Advances through the source document by adding
        /// the given offset to the position index.
        /// </summary>
        private static void Advance(int Offset)
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

