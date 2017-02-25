using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashSetEqualityComparer<T> : IEqualityComparer<HashSet<T>> {

	public int GetHashCode(HashSet<T> set) {
		if(set==null)
			return 0;
		int h = 0x59750234;	//some arbitrary number
		foreach(T element in set) {
			h = h + set.Comparer.GetHashCode (element);
		}
		return h;
	}

	public bool Equals(HashSet<T> set1,HashSet<T> set2) {
		if(set1 == set2)
			return true;
		if(set1 == null || set2 == null)
			return false;
		return set1.SetEquals(set2);
	}
}
