using System.Linq.Expressions;
using HeliumCat.Responses;
using HeliumCat.Responses.Transactions;
using LiteDB;

namespace HeliumCat.Services;

public class CacheService : IDisposable
{
    private const string DbFilename = "cache.db";
    private readonly LiteDatabase _db;
    public static CacheService Default;

    private CacheService()
    {
        _db = new LiteDatabase(DbFilename);
        _db.Checkpoint();
        _db.Rebuild();
    }

    static CacheService()
    {
        Default = new CacheService();
    }

    private ILiteCollection<T> GetCollection<T>()
    {
        switch (typeof(T))
        {
            case Type t when t == typeof(Hotspot):
            {
                var collection = _db.GetCollection<Hotspot>("hotspots");
                collection.EnsureIndex(x => x.Address, true);
                collection.EnsureIndex(x => x.Name);
                return (ILiteCollection<T>)collection;
            }
            case Type t when t == typeof(PocReceiptsV2Transaction):
            {
                var collection = _db.GetCollection<PocReceiptsV2Transaction>("poc_receipts");
                collection.EnsureIndex(x => x.Hash, true);
                collection.EnsureIndex(x => x.Time);
                return (ILiteCollection<T>)collection;
            }
            default:
                throw new ArgumentOutOfRangeException(typeof(T).FullName);
        }
    }

    public T GetOne<T>(Expression<Func<T, bool>> predicate)
    {
        var collection = GetCollection<T>();
        var documents = collection.Query().Where(predicate).ToDocuments();
        var document = documents.FirstOrDefault();
        return collection.EnsureDocumentValidity<T>(document);
    }

    public List<T> GetMany<T>(Expression<Func<T, bool>> predicate)
    {
        var collection = GetCollection<T>();
        var documents = collection.Query().Where(predicate).ToDocuments();
        return documents.Select(d => collection.EnsureDocumentValidity<T>(d)).ToList();
    }

    public void InsertOne<T>(T item)
    {
        var collection = GetCollection<T>();
        collection.Insert(item);
    }

    public void InsertMany<T>(IEnumerable<T> items)
    {
        var collection = GetCollection<T>();
        collection.InsertBulk(items);
    }

    private string Pluralize(string singleName)
    {
        if (singleName.EndsWith('y'))
        {
            return singleName.Substring(0, singleName.Length - 1) + "ies";
        }

        return singleName + "s";
    }

    public void Dispose()
    {
        if (_db != null)
        {
            _db.Dispose();
        }
    }
}

public static class CacheExtensions
{
    private static readonly TimeSpan EvictionTime = TimeSpan.FromDays(1);

    public static T? EnsureDocumentValidity<T>(this ILiteCollection<T> collection, BsonDocument document)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (document == null)
        {
            return default;
        }

        var documentId = document["_id"];
        var timespan = DateTime.Now - documentId.AsObjectId.CreationTime;
        if (timespan > EvictionTime)
        {
            collection.Delete(documentId);
            return default;
        }

        return BsonMapper.Global.ToObject<T>(document);
    }
}