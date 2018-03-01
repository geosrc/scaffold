using System;

namespace Scaffold.Logging
{
	public interface ILogger
	{
		bool LogCritical { get; }
		bool LogDebug { get; }
		bool LogError { get; }
		bool LogInfo { get; }
		bool LogVerbose { get; }
		bool LogWarning { get; }
		Severity MinimumSeverity { get; set; }
		Logger Root { get; }
		string Tag { get; }

		void Critical( IFormatProvider provider, string format, params object[] args );
		void Critical( string message );
		void Critical( string format, params object[] args );
		void Debug( IFormatProvider provider, string format, params object[] args );
		void Debug( string message );
		void Debug( string format, params object[] args );
		void Error( IFormatProvider provider, string format, params object[] args );
		void Error( string message );
		void Error( string format, params object[] args );
		void Info( IFormatProvider provider, string format, params object[] args );
		void Info( string message );
		void Info( string format, params object[] args );
		void Log( Exception ex, bool logCallStack = true, Severity severity = Severity.Error );
		void Log( Exception ex, DateTime eventTime, bool logCallStack = true, Severity severity = Severity.Error );
		void Log( Severity severity, string tag, IFormatProvider provider, string format, params object[] args );
		void Log( Severity severity, string tag, string message, DateTime eventTime, object data );
		void Log( Severity severity, string tag, string message, object data );
		void Verbose( IFormatProvider provider, string format, params object[] args );
		void Verbose( string message );
		void Verbose( string format, params object[] args );
		void Warning( IFormatProvider provider, string format, params object[] args );
		void Warning( string message );
		void Warning( string format, params object[] args );
	}
}