using System;
using System.Collections.Generic;
using System.Text;

namespace Scaffold.Logging
{
	/// <summary>
	/// Helper class providing default representation infos for <see cref="Event"/>.
	/// </summary>
	public static class Defaults
	{
		/// <summary>
		/// Return a 3 char code for the given <see cref="Severity"/>.
		/// </summary>
		public static string Code( Severity severity )
		{
			switch ( severity )
			{
				case Severity.Debug:	return "DEB";
				case Severity.Verbose:	return "VRB";
				case Severity.Info:		return "INF";
				case Severity.Warning:	return "WRN";
				case Severity.Error:	return "ERR";
				case Severity.Critical: return "CRT";
				default:
					throw new ArgumentException( $"Unknown severity: {severity}", nameof( severity ) );
			}
		}

		/// <summary>
		/// Return a console color for the given <see cref="Severity"/>.
		/// </summary>
		public static ConsoleColor ForegroundColor( Severity severity )
		{
			switch ( severity )
			{
				case Severity.Debug:	return ConsoleColor.Blue;
				case Severity.Verbose:	return ConsoleColor.Gray;
				case Severity.Info:		return ConsoleColor.White;
				case Severity.Warning:	return ConsoleColor.Yellow;
				case Severity.Error:	return ConsoleColor.Red;
				case Severity.Critical: return ConsoleColor.Red;
				default:
					throw new ArgumentException( $"Unknown severity: {severity}", nameof( severity ) );
			}
		}
	}
}
