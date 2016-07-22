
## Extensions and improvements

If you feel like you're up for it, you can now extend or improve the Objective-Kaleidoscope compiler. Just fork the [Objective-Kaleidoscope repository on GitHub](https://github.com/jonathanvdc/objective-kaleidoscope) to get started.

Here are a few suggestions to get you started:   

* Use an input stream (`TextStream`) instead of a simple `string` in the `Lexer` type, to avoid allocating a huge `string` whenever an input file is parsed. This will have to be paired with some type of buffering mechanism.
