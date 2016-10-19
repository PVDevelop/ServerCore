using System;

namespace PVDevelop.UCoach.Mongo
{
	[AttributeUsage(AttributeTargets.Class)]
	public class MongoCollectionAttribute : Attribute
	{
		public string Name { get; private set; }

		public MongoCollectionAttribute(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException(nameof(name));
			}

			Name = name;
		}
	}
}
