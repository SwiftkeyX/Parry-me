using UnityEngine;

public interface IMoveableForSideScroller
{
    public void KeepCharacterInZAxis(Transform transform)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }
}