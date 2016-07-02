
## Lexer

### Goals

Okay, so the first phase of this whole compilation thing is _lexing._ The point is to take the _source code,_ which is just a long string of characters, and try to turn that into a sequence of tokens. _Tokens_ are really just substrings of the source text, and are always classified as a specific _kind_ of token. Having tokens makes the parser's life much, much easier, because it can always just consume one _token_ at a time, instead of one _character_ at a time.. 

As a practical example, the source code below

```
var x = other_var + 3
```

is to be lexed as follows

```
(VarKeyword, "var"), (Whitespace, " "), (Identifier, "x"), 
(Whitespace, " "), (Eq, "="), (Identifier, "other_var"), 
(Whitespace, " "), (Plus, "+"), (Whitespace, " "), 
(Integer, "3")
```

Note that the _lexer,_ the object that turns source code into tokens, has absolutely no notion of what the tokens it produces _mean._ That's up to the parsing and codegen phases. Gibberish like this

```
= + var
```

is perfectly fine from the lexer's perspective, which will produce the following stream of tokens

```
(Eq, "="), (Whitespace, " "), (Plus, "+"), 
(Whitespace, " "), (VarKeyword, "var")
```

Garbage in, garbage out. Finding out that these tokens make no sense in this order, is entirely the parser's problem.

### Implementation


