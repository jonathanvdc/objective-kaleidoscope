
## Lexer

### A brief explanation of lexical analysis

The first phase of this whole compilation thing is [lexical analysis](https://en.wikipedia.org/wiki/Lexical_analysis). The plan is to take the _source code,_ which is just a long string of characters, and try to turn that into a sequence of [tokens](https://en.wikipedia.org/wiki/Lexical_analysis#Token). _Tokens_ capture a _string of related characters_ that is also present in the source text, and every token is classified as a specific _kind_ of token.

The primary merit of tokens is that they are useful to the parser, which will always consume one _token_ at a time, rather than one _character_ at a time. For example, when parsing `"some_name = 32"`, it makes perfect sense to consume token `"some_name"` of kind `Identifier` in one step. [Scannerless parsing](https://en.wikipedia.org/wiki/Scannerless_parsing), that is, parsing the identifier character-by-character without a lexical analysis phase, is not unprecedented, but typically complicates the parser's logic.

As a practical example, we'd like to lex the (Objective-Kaleidoscope) source code below

```
var x = other_var + 32
```

as

```
(VarKeyword, "var"), (Whitespace, " "), (Identifier, "x"),
(Whitespace, " "), (Eq, "="), (Identifier, "other_var"),
(Whitespace, " "), (Operator, "+"), (Whitespace, " "),
(Integer, "32")
```

Note that the _lexer,_ the object that turns source code into tokens, has absolutely no notion of what the tokens it produces _mean._ That's up to the parsing and codegen phases. Gibberish like this

```
= + var
```

is perfectly fine from the lexer's perspective, which will produce the following stream of tokens

```
(Eq, "="), (Whitespace, " "), (Operator, "+"),
(Whitespace, " "), (VarKeyword, "var")
```

Garbage in, garbage out. Finding out that these tokens make no sense in this order, is entirely the parser's problem.

### Implementation

#### Data structures

The implementation of the lexical analysis pass is split across two source files: [`Token.cs`](https://github.com/jonathanvdc/objective-kaleidoscope/blob/master/src/Token.cs) defines supporting data structures &ndash; `enum TokenKind` and `class Token` &ndash; while [`Lexer.cs`](https://github.com/jonathanvdc/objective-kaleidoscope/blob/master/src/Lexer.cs) implements the lexer itself.

Let's take a look at some of the highlights from `Token.cs`. [`TokenKind`](https://github.com/jonathanvdc/objective-kaleidoscope/blob/master/src/Token.cs#L9) is a simple enumeration of all possible token kinds. There are three main categories of token kinds defined here:

* Token kinds for tokens which can encapsulate any number of strings, like `Identifier`, which is the correct token kind for non-keyword character strings whose first character is not a digit.
* Token kinds for fixed-string tokens, such as keywords and certain types of punctuation: `DefKeyword` tokens can only contain string `"def"`, and a `Semicolon` token can only be `";"`.
* The special end-of-file pseudo-token's kind. An end-of-file token tells the parser that the end of the input stream has been reached.

[`Token`](https://github.com/jonathanvdc/objective-kaleidoscope/blob/master/src/Token.cs#L86) is a fairly simple data structure:

```cs
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

    // ... (static members, omitted here)
}
```

#### Lexer

Now, let's take a look at [`Lexer.cs`](https://github.com/jonathanvdc/objective-kaleidoscope/blob/master/src/Lexer.cs). A [`Lexer`](https://github.com/jonathanvdc/objective-kaleidoscope/blob/master/src/Lexer.cs) instance consists of two bits of information: the entirety of the source document, as a character string, and an integer index that refers to a position in this `string`: the starting position of the next token to read.

```cs
public class Lexer
{
    public Lexer(string Source)
    {
        this.source = Source;
        this.pos = 0;
    }

    private string source;
    private int pos;

    // ... (methods, omitted here)
}
```

[`Lexer`](https://github.com/jonathanvdc/objective-kaleidoscope/blob/master/src/Lexer.cs) has two important public methods: `Read` and `Peek`. The latter reads a single token from the input stream, and does not change the lexer's state. Repeatedly calling `Peek` will yield the same token. The parameterless `Read` overload, on the other hand, reads a single token, and then advances the position in the source document. Its implementation is straightforward.

```cs
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
```

`Peek`'s implementation has been omitted here, because it is fairly long, and it depends on a lot of small helper methods. `Peek`'s inner workings shouldn't be all that hard to understand, though. Just Ctrl-F for "Peek" in [`Lexer`](https://github.com/jonathanvdc/objective-kaleidoscope/blob/master/src/Lexer.cs) if you're interested in seeing how it works.

At times, we'll want to read a token, and make sure that its kind matches a specific token kind. For example, when parsing a parenthesized expression `(expr`, we _expect_ to find closing parentheses (`)`) &ndash; anything else is syntax error. Since this is a fairly common scenario for parsers, we'll add a convenient overload to the lexer.

```cs
/// <summary>
/// Reads a single token from the source document,
/// and updates the current position. The token's
/// kind must match the given token kind.
/// </summary>
public Token Read(TokenKind Kind)
{
    var result = Read();
    if (result.Kind != Kind)
        throw new Exception(
            "Expected a token of type '" + Kind +
            "', got '" + result.Contents + "'.");

    return result;
}
```

#### Driver program

We can now write a simple driver program that applies the lexer to the input we give it, line-by-line.

```cs
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
```

### Conclusion

Now that we've written a fully-functional lexer, we can get started on writing the parser, which will take the lexer's output, and use that to build a parse tree.
