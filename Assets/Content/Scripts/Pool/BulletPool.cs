using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class BulletPool
{
    private readonly Dictionary<BulletTypes, Bullet> _prefabs;
    private readonly Transform _container;
    private readonly int _startCapacity;

    private Dictionary<BulletTypes, List<Bullet>> _pool = new();

    public BulletPool(Transform container, int startCapacity = 5)
    {
        _prefabs = BulletsConfig.BulletPrefabs;
        _container = container;
        _startCapacity = startCapacity;
        
        _pool.Clear();

        foreach (var (type, _) in _prefabs)
            CreatePool(type);
    }

    private void CreatePool(BulletTypes type)
    {
        _pool[type] = new List<Bullet>();

        for (var i = 0; i < _startCapacity; i++)
            CreateElement(type);
    }

    private Bullet CreateElement(BulletTypes type)
    {
        var createObject = Object.Instantiate(_prefabs[type], _container);
        createObject.gameObject.SetActive(false);

        _pool[type].Add(createObject);

        return createObject;
    }

    private void TryGetElement(BulletTypes type, out Bullet element)
    {
        foreach (var i in _pool[type])
        {
            if (i.gameObject.activeInHierarchy)
                continue;
            
            element = i;
            i.gameObject.SetActive(true);
            return;
        }

        element = CreateElement(type);
        element.gameObject.SetActive(true);
    }

    public Bullet GetFreeElement(BulletTypes type)
    {
        TryGetElement(type, out var element);
        return element;
    }

    public Bullet GetFreeElement(BulletTypes type, Vector2 position)
    {
        var element = GetFreeElement(type);
        element.transform.position = position;
        return element;
    }
}