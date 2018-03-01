using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Scaffold.Logging
{
	public class Logger : ILogger
	{
		private readonly Object _accessLock = new Object();
		private ISink[]			_sinks		= new ISink[0];
		public Logger Root { get; }
		public String Tag { get; }
		public Severity MinimumSeverity { get; set; }

		public bool LogDebug	=> MinimumSeverity <= Severity.Debug;
		public bool LogVerbose	=> MinimumSeverity <= Severity.Verbose;
		public bool LogInfo		=> MinimumSeverity <= Severity.Info;
		public bool LogWarning	=> MinimumSeverity <= Severity.Warning;
		public bool LogError	=> MinimumSeverity <= Severity.Error;
		public bool LogCritical => MinimumSeverity <= Severity.Critical;

		// I'm root
		public Logger()
		{
			Root			= this;
			Tag				= "Logger";
			MinimumSeverity = Severity.Debug;
		}

		public Logger( Severity minimumSeverity, string tag = "Logger" )
		{
			Root			= this;
			Tag				= tag;
			MinimumSeverity = minimumSeverity;
		}

		protected Logger( String source, Logger root )
		{
			Root = root;
			Tag = source;
		}

		public ILogger Attach( String source, Severity minimumSeverity = Severity.Debug )
		{
			if ( source == null )
				throw new ArgumentNullException( nameof( source ) );

			return new Logger( source, Root );
		}

		public void AddSink( ISink sink )
		{
			if ( sink == null )
				throw new ArgumentNullException( nameof( sink ) );

			lock ( _accessLock )
			{
				if ( _sinks.Any( s => s.Equals( sink ) ) )
					return;

				var updated = new ISink[_sinks.Length + 1];
				Array.Copy( _sinks, updated, _sinks.Length );
				updated[_sinks.Length] = sink;

				// Swap
				_sinks = updated;
			}
		}

		public void RemoveSink( ISink sink )
		{
			if ( sink == null )
				throw new ArgumentNullException( nameof( sink ) );

			lock ( _accessLock )
			{
				_sinks = _sinks.Where( s => s != sink ).ToArray();
			}
		}

		public ISink RemoveSink( String name )
		{
			if ( name == null )
				throw new ArgumentNullException( nameof( name ) );

			lock ( _accessLock )
			{
				var sink = _sinks.FirstOrDefault( s => s.Name == name );
				if ( sink != null )
					RemoveSink( sink );
				return sink;
			}
		}

		protected void Log( Event logEvent )
		{
			if ( logEvent == null )
				throw new ArgumentNullException( nameof( logEvent ) );

			var sinks = _sinks;

			if ( sinks.Length == 0 )
				return;

			foreach ( var sink in sinks )
			{
				sink.Handle( logEvent );
			}
		}

		public void Log( Severity severity, String tag, String message, DateTime eventTime, Object data )
		{
			var now = DateTime.UtcNow;
			Root.Log( new Event( severity, tag, message, now, eventTime, data ) );
		}

		public void Log( Severity severity, String tag, String message, Object data )
		{
			var now = DateTime.UtcNow;
			Root.Log( new Event( severity, tag, message, now, now, data ) );
		}

		public void Log( Severity severity, String tag, IFormatProvider provider, String format, params Object[] args )
		{
			var now = DateTime.UtcNow;
			Root.Log( new Event( severity, tag, String.Format( provider, format, args ), now, now, null ) );
		}

		public void Log( Exception ex, bool logCallStack = true, Severity severity = Severity.Error )
		{
			var now = DateTime.UtcNow;
			var msg = logCallStack ? ex.ToString() : ex.Message;
			Root.Log( new Event( severity, Tag, ex.ToString(), now, now, null ) );
		}

		public void Log( Exception ex, DateTime eventTime, bool logCallStack = true, Severity severity = Severity.Error )
		{
			var now = DateTime.UtcNow;
			var msg = logCallStack ? ex.ToString() : ex.Message;
			Root.Log( new Event( severity, Tag, ex.ToString(), now, eventTime, null ) );
		}

		public void Debug( IFormatProvider provider, String format, params Object[] args )
		{
			if ( LogDebug )
				Log( Severity.Debug, Tag, provider, format, args, null );
		}
		public void Debug( String format, params Object[] args ) => Debug( CultureInfo.InvariantCulture, format, args );
		public void Debug( String message )
		{
			if ( LogDebug )
				Log( Severity.Debug, Tag, message, null );
		}

		public void Verbose( IFormatProvider provider, String format, params Object[] args )
		{
			if (LogVerbose)
				Log( Severity.Verbose, Tag, provider, format, args, null );
		}
		public void Verbose( String format, params Object[] args ) => Verbose( CultureInfo.InvariantCulture, format, args );
		public void Verbose( String message )
		{
			if ( LogVerbose )
				Log( Severity.Verbose, Tag, message, null );
		}

		public void Info( IFormatProvider provider, String format, params Object[] args )
		{
			if ( LogInfo )
				Log( Severity.Info, Tag, provider, format, args, null );
		}
		public void Info( String format, params Object[] args ) => Info( CultureInfo.InvariantCulture, format, args );
		public void Info( String message )
		{
			if ( LogInfo )
				Log( Severity.Info, Tag, message, null );
		}

		public void Warning( IFormatProvider provider, String format, params Object[] args )
		{
			if ( LogWarning )
				Log( Severity.Warning, Tag, provider, format, args, null );
		}
		public void Warning( String format, params Object[] args ) => Warning( CultureInfo.InvariantCulture, format, args );
		public void Warning( String message )
		{
			if ( LogInfo )
				Log( Severity.Warning, Tag, message, null );
		}

		public void Error( IFormatProvider provider, String format, params Object[] args )
		{
			if ( LogError )
				Log( Severity.Error, Tag, provider, format, args, null );
		}
		public void Error( String format, params Object[] args ) => Error( CultureInfo.InvariantCulture, format, args );
		public void Error( String message )
		{
			if ( LogInfo )
				Log( Severity.Error, Tag, message, null );
		}

		public void Critical( IFormatProvider provider, String format, params Object[] args )
		{
			if ( LogCritical )
				Log( Severity.Critical, Tag, provider, format, args, null );
		}
		public void Critical( String format, params Object[] args ) => Critical( CultureInfo.InvariantCulture, format, args );
		public void Critical( String message )
		{
			if ( LogInfo )
				Log( Severity.Critical, Tag, message, null );
		}
	}
}
