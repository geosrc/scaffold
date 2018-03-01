using Scaffold.Cmd;
using Scaffold.Logging;
using System;
using System.Linq.Expressions;

namespace Scaffold.Demo
{
	class Program
	{
		static void Main(string[] args)
		{
			var log = new Logger();
			log.AddSink( new ColoredConsoleSink( "console" ) );

			var log2 = log.Attach( "Log2" );

			log2.Debug( "Debug log event" );
			log2.Verbose( "Verbose log event" );
			log2.Info( "Info log event" );
			log2.Warning( "Warning log event" );
			log2.Error( "Error log event" );
			log2.Critical( "Critical log event" );
			try
			{
				throw new ApplicationException( "App failed", new Exception( "Inner" ) );
			}
			catch ( Exception ex )
			{
				log2.Log( ex );
			}

			try
			{
				throw new Exception( "Fatal ex" );
			}
			catch ( Exception ex )
			{
				log2.Log( ex, false, Severity.Critical );
			}
		}
	}
}
