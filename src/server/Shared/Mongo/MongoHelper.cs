﻿using System;
using System.Linq;
using System.Reflection;
using MongoDB.Bson;
using MongoDB.Driver;
using PVDevelop.UCoach.Configuration;

namespace PVDevelop.UCoach.Mongo
{
	public static class MongoHelper
	{
		/// <summary>
		/// Возвращает имя для коллекции типа T
		/// </summary>
		public static string GetCollectionName<T>()
		{
			var mongoCollectionAttr =
				(MongoCollectionAttribute)typeof(T).
				GetTypeInfo().
				GetCustomAttributes(typeof(MongoCollectionAttribute), true).
				SingleOrDefault();

			if (mongoCollectionAttr != null)
			{
				return mongoCollectionAttr.Name;
			}

			return typeof(T).Name;
		}

		public static IMongoCollection<T> GetCollection<T>(IConnectionStringProvider settings)
		{
			var builder = new MongoUrlBuilder(settings.ConnectionString);

			var mongoClient = new MongoClient(builder.ToMongoUrl());
			var db = mongoClient.GetDatabase(builder.DatabaseName);

			var collectionName = GetCollectionName<T>();

			return db.GetCollection<T>(collectionName);
		}

		public static string GetIndexName<T>(string propertyName)
		{
			var property = typeof(T).GetTypeInfo().GetProperty(propertyName);
			var attr =
				(MongoIndexNameAttribute)property.GetCustomAttributes(typeof(MongoIndexNameAttribute), true).SingleOrDefault();
			if (attr == null)
			{
				return property.Name;
			}
			return attr.Name;
		}

		public static string GetCompoundIndexName<T>(params string[] propertyNames)
		{
			if (propertyNames == null)
			{
				throw new ArgumentNullException(nameof(propertyNames));
			}
			if (!propertyNames.Any())
			{
				throw new ArgumentException("No properties specified for compound index.");
			}
			return string.Join(".", propertyNames.Select(GetIndexName<T>));
		}

		public static bool IsUniqueIndex(BsonDocument document)
		{
			BsonElement element;
			return
				document.TryGetElement("unique", out element) &&
				element.Value.AsBoolean;
		}

		public static string SettingsToString(IConnectionStringProvider settings)
		{
			return settings.ConnectionString;
		}
	}
}
