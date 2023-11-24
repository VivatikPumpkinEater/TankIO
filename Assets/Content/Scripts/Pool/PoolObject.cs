using UnityEngine;

public class PoolObject : MonoBehaviour
{
    protected virtual void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}