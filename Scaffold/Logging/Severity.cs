using System;

namespace Scaffold.Logging
{
	/// <summary>
	/// Severity levels of <see cref="Scaffold.Logging.Event"/>.
	/// </summary>
	public enum Severity
	{
		/// <summary>
		/// Special severity level of events important to debug the application.
		/// </summary>
		Debug,

		Verbose,
		Info,
		Warning,

		/// <summary>
		/// Severity level of an error that should not be fatal for the application.
		/// </summary>
		Error,

		/// <summary>
		/// Severity level of an error that will prevent the application from continuing to work.
		/// </summary>
		Critical,
	}
}