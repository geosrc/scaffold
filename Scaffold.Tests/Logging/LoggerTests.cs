using Scaffold.Logging;
using System;
using Xunit;

namespace Scaffold.Tests.Logging
{
	public class LoggerTests
	{
		[Fact]
		public void AttachChildWorks()
		{
			var root = new Logger();
			var child = root.Attach( "Child" );
			Assert.NotNull( child );
			Assert.Equal( "Child", child.Tag );
			Assert.Equal( root, child.Root );
		}
	}
}
