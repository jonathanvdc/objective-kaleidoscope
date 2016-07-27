
## Parser

### Syntactic analysis

In this particular context, [_parsing_ or _syntactic analysis_](https://en.wikipedia.org/wiki/Parsing) is the process of analyzing a stream of tokens, which conform to the rules of Objective-Kaleidoscope's formal grammar, that is, tokens which originate from a syntactically correct source document: the source code does not contain any syntax errors.

#### Abstract syntax tree (AST)

We will represent the results of the syntactic analysis &ndash; the parser's output &ndash; as an [_abstract syntax tree_](https://en.wikipedia.org/wiki/Abstract_syntax_tree) (AST). An AST captures the program's syntactic _structure_ in a tree data structure, which closely models the source language: Objective-Kaleidoscope.

An AST captures a lot of information about the source program, and presents it in an orderly fashion. This will make code generation &ndash; actually, IR generation, in our case &ndash; much easier later on.

We'll want to define a number of different categories of AST nodes. For now, we'll define interfaces for expression AST nodes, parameter AST nodes, function AST nodes and class AST nodes:

```cs
/// <summary>
/// A common interface for expression AST nodes.
/// </summary>
public interface IExpressionNode
{
    /// <summary>
    /// Analyzes this AST node and its children.
    /// </summary>
    /// <param name="Scope">
    /// The expression's enclosing local scope.
    /// </param>
    /// <returns>
    /// A Flame IR type, expression or variable.
    /// </returns>
    TypeOrExpression Analyze(LocalScope Scope);
}
```

Note how an AST provides a neat little buffer between the parsing and the IR generation stages: the parser can simply construct an AST, without performing any [_semantic analysis_](https://en.wikipedia.org/wiki/Semantic_analysis_(compilers)), and the IR generation stage only has to run semantic analysis on the AST nodes that the parser constructed. This is somewhat similar to the way _tokens_ separate the lexical analysis and parsing phases.

#### Parsing

The parsing process itself
