namespace CMDParser.Internals
{
	/// <summary>
	/// An empty implementation of the type,
	/// simulating <see cref="void"/>.
	/// </summary>
	/// <remarks>
	/// We use this class as a type argument for 
	/// flag options' callbacks (i.e., those
	/// having no arguments).
	/// 
	/// This is the internal structure and 
	/// shall never be accessible from outer API.
	/// Yet, due to language modifiers, we are
	/// supposed to keep it public.
	/// </remarks>
	public sealed class Void
	{
		private Void() { }
	}
}
