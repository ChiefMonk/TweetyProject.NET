namespace TweetyProject.NET.Commons.Helpers;

public static class CoreCollectionExtensions
{
    public static int RemoveAll<T>(this HashSet<T> set, Predicate<T> match)
    {
        // Create a list to hold elements that match the predicate
        var itemsToRemove = set.Where(x => match(x)).ToList();

        // Remove each matching element from the HashSet
        foreach (T item in itemsToRemove)
        {
            set.Remove(item);
        }

        // Return the count of removed elements
        return itemsToRemove.Count;
    }

    public static int RemoveAll<T>(this HashSet<T> set, ISet<T> itemsToRemove)
    {
        // Remove each matching element from the HashSet
        foreach (T item in itemsToRemove)
        {
            set.Remove(item);
        }

        // Return the count of removed elements
        return itemsToRemove.Count;
    }

    public static bool ContainsAll<T>(this ISet<T> currentItems, ICollection<T> checkItems)
    {
        foreach (T item in checkItems)
        {
            if(!currentItems.Contains(item))
                return false;
        }

        return true;
    }
}