using System.Text;
using Antlr4.Runtime;
using Kay;

string[] banner = new string[]
{
    "Hi, I am **Kay**. I'm a programming language full of **Joy**.",
    "You can find more information about me at [Kay](https://github.com/basp/kay).",
    "Go wild and have fun! <3",
    string.Empty,
};

const char dot = '.';
const string prompt = "kay> ";
const int pointerOffset = 12;

var interpreter = new Interpreter();
var visitor = new CycleVisitor(interpreter);

Console.WriteLine(string.Join(Environment.NewLine, banner));

while (true)
{
    var buf = new StringBuilder();
    Console.Write(prompt);
    while (true)
    {
        // Console.ReadLine will eat up the new line which
        // we need for the parser so we'll just add it back in.
        var input = Console.ReadLine() + Environment.NewLine;
        if (string.IsNullOrWhiteSpace(input))
        {
            // This is just a hack to ensure that
            // an empty string does not result in a
            // parse error.
            buf.Append(".");
            break;
        }

        buf.Append(input);
        if (input.Trim().EndsWith(dot))
        {
            break;
        }

        // Since we are continuing a multiline prompt we'll indent
        // to give some visual feedback in the interactive.
        Console.Write(string.Empty.PadRight(prompt.Length));
    }

    var stream = new AntlrInputStream(buf.ToString());
    var lexer = new KayLexer(stream);
    var tokens = new CommonTokenStream(lexer);
    var parser = new KayParser(tokens);

    Console.WriteLine();

    void PrintStack(C5.IStack<INode> stack)
    {
        for (var i = stack.Count - 1; i >= 0; i--)
        {
            var pointer = "";
            if (i == stack.Count - 1)
            {
                pointer = "<- top";
            }

            var repr = stack[i].ToRepresentation();
            var offset = Math.Max(repr.Length + 1, pointerOffset);
            Console.WriteLine(
                string.Concat(repr.PadRight(offset), pointer));
        }

        if (stack.Count > 0)
        {
            Console.WriteLine();
        }
    }

    C5.IStack<INode>? stack = new C5.ArrayList<INode>(1024);

    try
    {
        var ctx = parser.cycle();
        stack = ctx.Accept(visitor);
    }
    catch (RuntimeException ex)
    {
        // These are generally thrown when stack validation
        // for a particular operation fails. They are usually
        // a result of user errors.
        Console.WriteLine($"Runtime exception: {ex.Message}");
    }
    catch (InvalidOperationException ex)
    {
        // These are thrown when the runtime attempts to
        // execute an operation with invalid operands. If this
        // exception is thrown it is usually the result of
        // insufficient stack validation or an bug in the 
        // interpreter.
        Console.WriteLine(ex.ToString());
    }
    finally
    {
        if (stack != null)
        {
            PrintStack(stack);
        }
    }
}