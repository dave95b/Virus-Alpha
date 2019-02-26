using UnityEngine;
using System.Reflection;

public static class GameObjectExtensions {

    #region GameObject Extensions

    public static T AddOrGetComponent<T>(this GameObject gameObject) where T : Component {
        T component = gameObject.GetComponent<T>();

        if (component == null)
            component = gameObject.AddComponent<T>();

        return component;
    }

    public static GameObject InstantiateChild(this GameObject gameObject) {
        return InstantiateChild(gameObject, "New Child");
    }

    public static GameObject InstantiateChild(this GameObject gameObject, string name) {
        GameObject child = new GameObject();

        Transform childTransform = child.transform;
        childTransform.parent = gameObject.transform;
        childTransform.localPosition = Vector3.zero;
        childTransform.localRotation = Quaternion.identity;
        childTransform.localScale = Vector3.one;
        child.name = name;

        return child;
    }

    public static T AddComponent<T>(this GameObject gameObject, T copy) where T : Component {
        System.Type type = copy.GetType();
        Component copied = gameObject.AddComponent(type);
        FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
        foreach (FieldInfo field in fields) {
            field.SetValue(copied, field.GetValue(copy));
        }

        return copied as T;
    }

    #endregion GameObject Extensions

    #region Transform Extensions

    public static float DistanceBetween(this Transform transform, Transform other) {
        return (transform.position - other.transform.position).magnitude;
    }

    public static float SqrDistanceBetween(this Transform transform, Transform other) {
        return (transform.position - other.transform.position).sqrMagnitude;
    }

    #endregion

}
