using System;

namespace PVDevelop.UCoach.Mongo
{
	[AttributeUsage(AttributeTargets.Property)]
	public class MongoIndexNameAttribute : Attribute
	{
		public string Name { get; private set; }

		public MongoIndexNameAttribute(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException(nameof(name));
			}

			Name = name;
		}
	}
}
