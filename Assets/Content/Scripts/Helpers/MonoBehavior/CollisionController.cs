using System;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    public event Action<Collision2D> CollisionEnter;

    private void OnCollisionEnter2D(Collision2D other)
    {
        CollisionEnter?.Invoke(other);
    }
}