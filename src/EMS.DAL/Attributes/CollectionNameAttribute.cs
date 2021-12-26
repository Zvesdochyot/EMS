namespace EMS.DAL.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class CollectionNameAttribute : Attribute
{
    public string CollectionName { get; }

    public CollectionNameAttribute(string collectionName)
    {
        CollectionName = collectionName;
    }
}
