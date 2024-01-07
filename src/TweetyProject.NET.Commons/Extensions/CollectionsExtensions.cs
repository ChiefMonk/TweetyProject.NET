namespace TweetyProject.NET.Commons.Extensions;

public static class CollectionsExtensions
{
	public static bool DoesFirstContainAllSecond<T>(this ISet<T> firstSet, ISet<T> secondSet)
	{
		return secondSet.All(firstSet.Contains);
	}

	public static void AddAll<T>(this ISet<T>? currentSet, ISet<T>? addSet)
	{
		if (addSet is null || !addSet.Any())
			return;

		currentSet ??= new HashSet<T>();

		foreach (var entry in addSet)
		{
			if (entry is null)
				continue;

			currentSet.Add(entry);
		}
	}
}