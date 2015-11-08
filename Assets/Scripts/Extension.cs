using UnityEngine;
using System.Collections;
using System;

public static class Extension {
    public static T Clamp<T>(this T t, T a, T b) where T : IComparable<T> {
        if (t.CompareTo(a) < 0) return a;
        if (t.CompareTo(b) > 0) return b;
        return t;
    }
}
