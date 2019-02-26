using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class CollectionExtensions {

#region IList

	public static void Shuffle<T>(this IList<T> list) {
		int count = list.Count;
		var last = count - 1;
		for (int i = 0; i < last; ++i) {
			int randomIndex = Random.Range(i, count);
			T tmp = list[i];
			list[i] = list[randomIndex];
			list[randomIndex] = tmp;
		}
	}

	public static T GetRandom<T>(this IList<T> list) {
		return list[Random.Range(0, list.Count)];
	}

    public static T GetRandom<T>(this IList<T> list, System.Random random)
    {
        return list[random.Next(list.Count)];
    }

    public static bool IsEmpty<T>(this IList<T> list) {
		return list.Count == 0;
	}

    public static void Swap<T>(this IList<T> list, int sourceIndex, int destinationIndex) {
        if (sourceIndex == destinationIndex)
            return;

        T temp = list[sourceIndex];
        list[sourceIndex] = list[destinationIndex];
        list[destinationIndex] = temp;
    }

    public static T RemoveRandom<T>(this IList<T> list)
    {
        var value = list[Random.Range(0,list.Count)];
        list.Remove(value);
        return value;
    }

    public static T RemoveFirst<T>(this IList<T> list)
    {
        var value = list[0];
        list.Remove(value);
        return value;
    }

    #endregion IList

    #region Arrays

    public static void Shuffle<T>(this T[] list) {
		int count = list.Length;
		var last = count - 1;
		for (int i = 0; i < last; ++i) {
			int randomIndex = Random.Range(i, count);
			T tmp = list[i];
			list[i] = list[randomIndex];
			list[randomIndex] = tmp;
		}
	}

	public static T GetRandom<T>(this T[] list){
		return list[Random.Range(0, list.Length)];
	}

    public static T GetRandom<T>(this T[] list, System.Random random)
    {
        return list[random.Next(list.Length)];
    }

    public static bool IsEmpty<T>(this T[] list) {
		if (list.Length == 0) return true;

		for (int i = 0; i < list.Length; i++)
			if (list[i] != null)
				return false;

		return true;
	}

    public static void Clear<T>(this T[] list) {
        for (int i = 0; i < list.Length; i++)
            list[i] = default(T);
    }

    public static void SortByDistance(this Collider2D[] list, Vector3 referencePosition) {
        System.Array.Sort(list, (hit1, hit2) => hit1 == null ? 1 : hit2 == null ? -1 :
                    (hit1.transform.position - referencePosition).sqrMagnitude
                    .CompareTo(
                        (hit2.transform.position - referencePosition).sqrMagnitude
                    ));
    }

    #endregion Arrays

    #region IEnumerable

    public static T GetRandom<T>(this IEnumerable<T> enumerable) {
		return enumerable.ElementAt(Random.Range(0, enumerable.Count()));
	}

    public static T GetRandom<T>(this IEnumerable<T> enumerable, System.Random random)
    {
        return enumerable.ElementAt(random.Next(enumerable.Count()));
    }

    public static bool IsEmpty<T>(this IEnumerable<T> enumerable) {
        //return enumerable.Count() == 0;
        return !enumerable.Any();
    }

#endregion IEnumerable
}
