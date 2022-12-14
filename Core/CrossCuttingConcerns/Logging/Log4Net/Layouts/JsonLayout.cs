using log4net.Core;
using log4net.Layout;
using System.Text.Json;

namespace Core.CrossCuttingConcerns.Logging.Log4Net.Layouts;

public class JsonLayout : LayoutSkeleton
{
    public override void ActivateOptions()
    {
    }

    public override void Format(TextWriter writer, LoggingEvent loggingEvent)
    {
        var logEvent = new SerializableLogEvent(loggingEvent);
        JsonSerializerOptions options = new()
        {
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize(logEvent, options);
        writer.WriteLine(json);
    }
}
