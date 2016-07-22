
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
(Whitespace, " "), (Op, "+"), (Whitespace, " "),
(Integer, "32")
```

Note that the _lexer,_ the object that turns source code into tokens, has absolutely no notion of what the tokens it produces _mean._ That's up to the parsing and codegen phases. Gibberish like this

```
= + var
```

is perfectly fine from the lexer's perspective, which will produce the following stream of tokens

```
(Eq, "="), (Whitespace, " "), (Op, "+"),
(Whitespace, " "), (VarKeyword, "var")
```

Garbage in, garbage out. Finding out that these tokens make no sense in this order, is entirely the parser's problem.

### Implementation
