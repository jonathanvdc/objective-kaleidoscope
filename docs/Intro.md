
# Objective-Kaleidoscope: implementing an object-oriented programming language with Flame

## Introduction

The [Kaleidoscope tutorials](http://llvm.org/docs/tutorial/) are a series of awesome tutorials that show how easy it is to implement a custom programming language with LLVM. Since Flame is a compiler framework much like LLVM, but for garbage-collected, object-oriented programming languages, I thought that Flame should have a similar tutorial, but for a strongly-typed, object-oriented dialect of the original Kaleidoscope language: Objective-Kaleidoscope.

We're going to build an ahead-of-time compiler for this programming language, because that's what Flame does. Throughout this tutorial, I'll assume you know the C# programming language, and have an IDE that can create projects and can install NuGet packages for you. Having some previous experience with compiler implementation is helpful, but shouldn't be necessary.

All right, now let's take a look at how ahead-of-time compilers usually operate, and then come up with a design of our own.

### Compilers in general  

Ahead-of-time compilation consists of a whole bunch of steps. You'll find that pretty much all compilers have the really important ones in common, and that these steps are complemented by minor steps, which may be specific to the source language, the back-end, or the compiler's optimization strategy. A simple compiler does this:

    Source Code    ->    Tokens    ->    Syntax tree    ->    Executable
                 Lexing          Parsing             Codegen

The _lexing_ phase turns a long string of _source code_ into bite-sized _tokens_, which are easier to _parse_ as a _syntax tree:_ a tree-like representation of the source code. At some point, the syntax tree gets turned into _executable code_ by the _codegen_ phase, which is short for _code generation phase._

Don't worry if this explanation hasn't quite quenched your thirst for knowledge: we'll take a more in-depth look at these phases right before we implement them.  

An _optimizing compiler_, which tries to make code more efficient, typically has a bunch of extra steps:

    Source code    ->    Tokens    ->    Syntax tree    ->    IR    
                 Lexing          Parsing              IRgen

        ->    Optimized IR    ->    Executable
    Optimization           Codegen


Optimization usually consists of a number of optimization _passes,_ which can arguably be considered to be specific phases in the compilation process, and codegen can be quite tricky, depending on the target platform. Their combined complexity can make building a compiler a daunting task: without a codegen phase, no executable is produced, and without optimizations, the performance of executables produced by your compiler may not be able to compete with executables that originated from other programming languages.

### Flame-based compilers

Fortunately, Flame has infrastructure in place to handle optimization and codegen independently of the source language. So what our compiler's design is going to look like (at least initially) is this:

    Source code    ->    Tokens    ->    Syntax tree    ->    IR    
                 Lexing          Parsing              IRgen

Optimization and codegen will still be performed, but - and I think this is a pretty important point here - our compiler will stop at the IRgen phase. After that, we'll hand everything over to Flame, and let it do the rest.

Oh, and maybe this would be a good time to introduce some nomenclature, as well. The lexical analysis (lexing), parsing and IRgen phases are called the front-end, the optimization pipeline is called the middle-end, and whatever comes after is called the back-end. You can build any of the above as a Flame extension, but right now, we'll implement a front-end. Everything else will be left to pre-existing Flame components.

I suppose that means we're all set to actually get started by implementing that Objective-Kaleidoscope programming language I talked about earlier. At least initially, we'll stick to the compilation pipeline's order during our implementation efforts: first the lexer, then the parser, and finally the IRgen phase will be implemented.

Later on, we can add cool bonus features, like source code diagnostics.
