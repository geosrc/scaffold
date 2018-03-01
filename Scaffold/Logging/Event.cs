using Scaffold.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Scaffold.Logging
{
	public class Event
	{
		public Severity Severity { get; }
		public String Source { get; }
		public DateTime LogTime { get;}
		public DateTime EventTime { get; }
		public String Message { get; }
		public Object Data { get; }

		#region Initialization
		public Event( Severity severity, String source, String message, DateTime logTime, DateTime eventTime, Object data )
		{
			Severity	= severity;
			Source		= source ?? "";
			LogTime		= logTime;
			EventTime	= eventTime;
			EventTime	= EventTime;
			Message		= message ?? "";
			Data		= data;
		}

		public override string ToString()
		{
			return String.Format( CultureInfo.InvariantCulture, "{0} {1} {2}: {3}", LogTime.ToStringUniversal(), Source, Defaults.Code( Severity ), Message );
		}
		#endregion Initialization
	}
}
