namespace Scaffold.Logging
{
	public interface ISink
	{
		string Name { get; }
		void Handle( Event entry );
	}
}