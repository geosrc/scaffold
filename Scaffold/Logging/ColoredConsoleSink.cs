using System;
using System.Collections.Generic;
using System.Text;

namespace Scaffold.Logging
{
	public class ColoredConsoleSink : ISink
	{
		private readonly Object _accessLock = new Object();
		public string Name { get; }

		public ColoredConsoleSink( string name )
		{
			Name = name;
		}

		public void Handle( Event entry )
		{
			if ( entry == null )
				throw new ArgumentNullException( nameof( entry ) );

			lock ( _accessLock )
			{
				var col = Console.ForegroundColor;
				Console.ForegroundColor = Defaults.ForegroundColor( entry.Severity );
				Console.WriteLine( entry );
				Console.ForegroundColor = col;
			}
		}
	}
}
