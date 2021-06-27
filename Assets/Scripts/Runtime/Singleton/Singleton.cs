
namespace Utility.Singleton
{
	/// <summary>
	/// Base class for singleton.
	/// </summary>
	/// <typeparam name="T"> The class to apply the singleton pattern.</typeparam>
	public class Singleton<T> where T : class, new()
	{
		private static T m_instance = null;
		public T Instance
		{
			get
			{
				if(m_instance == null)
				{
					m_instance = new T();
				}

				return m_instance;
			}
		}
	}
}