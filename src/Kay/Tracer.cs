using System.Text;

namespace Kay;

public class Tracer : List<(INode[], INode[])>
{
    public static string Separator { get; set; } = " : ";

    public static int MaxColumnWidth { get; set; } = 120;

    public override string ToString() => TraceToString(this);

    public void Record(Interpreter i)
    {
        var stackNodes = i.Stack.ToArray();
        var queueNodes = i.Queue.SelectMany(x => x.Elements).ToArray();
        this.Add((stackNodes, queueNodes));
    }

    private static string TraceToString(List<(INode[], INode[])> history)
    {
        var lines = history
            .Select(x => new
            {
                Stack = string.Join(
                    ' ',
                    x.Item1.Select(x => x.ToRepresentation())),
                Queue = string.Join(
                    ' ',
                    x.Item2.Select(x => x.ToRepresentation())),
            })
            .Prepend(new
            {
                Stack = "stack",
                Queue = "queue",
            })
            .Select(x => new
            {
                Stack = x.Stack.Length > MaxColumnWidth
                    ? string.Concat(
                        "... ",
                        x.Stack.Substring(x.Stack.Length - MaxColumnWidth, MaxColumnWidth))
                    : x.Stack,
                Queue = x.Queue.Length > MaxColumnWidth
                    ? string.Concat(
                        x.Queue.Substring(0, MaxColumnWidth),
                        " ...")
                    : x.Queue,
            });

        var padding = lines.Max(x => x.Stack.Length);
        var max = lines.Max(x => padding + Separator.Length + x.Queue.Length);
        var h1 = "".PadRight(max, '=');
        var h2 = "".PadRight(max, '-');
        var caption = lines.First();
        var trace = lines.Skip(1);

        var buf = new StringBuilder();

        void AppendLine(string stack, string queue)
        {
            buf!.Append(stack.PadLeft(padding));
            buf.Append(Separator);
            buf.AppendLine(queue);
        }

        buf.AppendLine(h1);
        AppendLine(caption.Stack, caption.Queue);
        buf.AppendLine(h2);
        Array.ForEach(trace.ToArray(), t => AppendLine(t.Stack, t.Queue));
        buf.AppendLine(h2);
        return buf.ToString();
    }
}