using UnityEngine;

[ExecuteAlways]
public class ColliderGizmoDrawer : MonoBehaviour
{
    public Color activeColor = Color.red;
    public Color inactiveColor = Color.gray;
    public bool isActive = true;

    private Collider col;

    void OnDrawGizmos()
    {
        if (col == null) col = GetComponent<Collider>();
        if (col == null) return;

        Gizmos.color = isActive ? activeColor : inactiveColor;

        Bounds b = col.bounds;
        Gizmos.DrawWireCube(b.center, b.size);
    }
}