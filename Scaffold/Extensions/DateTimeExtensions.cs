using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Scaffold.Extensions
{
	public static class DateTimeExtensions
	{
		public static String ToStringUniversal( this DateTime self ) => self.ToString( "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture );
	}
}
