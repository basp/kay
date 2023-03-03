using System.Text;

namespace Kay;

public class Tracer : List<(INode[], INode[])>
{
    public static string Separator { get; set; } = " : ";

    public static int ColumnWidth { get; set; } = 120;

    public override string ToString() => TraceToString(this);

    public void Record(Interpreter i)
    {
        var stackNodes = i.Stack.ToArray();
        var queueNodes = i.Queue.SelectMany(x => x.Elements).ToArray();
        this.Add((stackNodes, queueNodes));
    }

    private static string TraceToString(List<(INode[], INode[])> history)
    {
        var buf = new StringBuilder();
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
            .Select(x => new
            {
                Stack = x.Stack.Length > ColumnWidth
                    ? string.Concat(
                        "... ",
                        x.Stack.Substring(x.Stack.Length - ColumnWidth, ColumnWidth))
                    : x.Stack,
                Queue = x.Queue.Length > ColumnWidth
                    ? string.Concat(
                        x.Queue.Substring(0, ColumnWidth),
                        " ...")
                    : x.Queue,
            });

        var padding = lines.Max(x => x.Stack.Length);
        var max = lines.Max(x => padding + Separator.Length + x.Queue.Length);
        var header = "".PadRight(max, '=');
        var spacer = "".PadRight(max, '-');

        buf.AppendLine(header);
        buf.Append("stack".PadLeft(padding));
        buf.Append(Separator);
        buf.Append("queue");
        buf.AppendLine();
        buf.AppendLine(spacer);
        foreach (var t in lines)
        {
            buf.Append(t.Stack.PadLeft(padding));
            buf.Append(Separator);
            buf.Append(t.Queue);
            buf.AppendLine();
        }

        buf.AppendLine(spacer);
        return buf.ToString();
    }
}