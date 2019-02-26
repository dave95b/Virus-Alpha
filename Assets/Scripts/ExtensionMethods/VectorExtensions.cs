using UnityEngine;

public static class VectorExtensions {

	public static Vector2 ChangeX(this Vector2 vector, float x) {
		return new Vector2(x, vector.y);
	}

	public static Vector2 ChangeY(this Vector2 vector, float y) {
		return new Vector2(vector.x, y);
	}

	public static Vector3 ChangeX(this Vector3 vector, float x) {
		return new Vector3(x, vector.y, vector.z);
	}

	public static Vector3 ChangeY(this Vector3 vector, float y) {
		return new Vector3(vector.x, y, vector.z);
	}

	public static Vector3 ChangeZ(this Vector3 vector, float z) {
		return new Vector3(vector.x, vector.y, z);
	}
}
