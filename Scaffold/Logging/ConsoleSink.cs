using System;
using System.Collections.Generic;
using System.Text;

namespace Scaffold.Logging
{
	public class ConsoleSink : ISink
	{
		public string Name { get; }

		public ConsoleSink(string name )
		{
			Name = name;
		}

		public void Handle( Event entry )
		{
			if ( entry == null )
				throw new ArgumentNullException( nameof( entry ) );

			Console.WriteLine( entry );
		}
	}
}
