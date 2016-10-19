using System;

namespace PVDevelop.UCoach.Mongo
{
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	public class MongoDataVersionAttribute : Attribute
	{
		public int Version { get; private set; }

		public MongoDataVersionAttribute(int version)
		{
			if (version <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(version));
			}

			Version = version;
		}
	}
}
