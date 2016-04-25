using log4net.Appender;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Skunkworks.Ics.Web.Managers
{
    public class LogManager : AppenderSkeleton
    {
        protected override void Append(LoggingEvent loggingEvent)
        {
            string newline = @"
";
            string timestamp = "*" + loggingEvent.TimeStamp.ToString("MM/dd/yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture) + "*    -";
            string level = @"    *[" + loggingEvent.Level.Name + "]*";
            string location = loggingEvent.LoggerName;
            string ex = loggingEvent.MessageObject.ToString();
            string message = loggingEvent.RenderedMessage;


            new SlackManager().PostMessage(timestamp + level + "```" + newline + location + newline + message + "```");
        }
    }
}