using System;

namespace ObjKaleidoscope
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string line;
            Console.Write("> ");
            while ((line = Console.ReadLine()) != null)
            {
                var lexer = new Lexer(line);
                while (lexer.Peek().Kind != TokenKind.EndOfFile)
                {
                    Console.Write(lexer.Read());
                    Console.Write(", ");
                }
                Console.WriteLine();
                Console.Write("> ");    
            }
        }
    }
}
