using System.Collections.Generic;
using UnityEngine;

public class UpdateSystem : MonoBehaviour
{
    private readonly List<IUpdate> _updateActors = new();
    private readonly List<IFixedUpdate> _fixedUpdateActors = new();

    public void Register(IUpdate actor)
    {
        _updateActors.Add(actor);
    }

    public void Register(IFixedUpdate actor)
    {
        _fixedUpdateActors.Add(actor);
    }

    public void Register<T>(T actor) where T : IUpdate, IFixedUpdate
    {
        Register(actor as IUpdate);
        Register(actor as IFixedUpdate);
    }

    private void Update()
    {
        foreach (var updateActor in _updateActors)
            updateActor.ManualUpdate(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        foreach (var fixedUpdateActor in _fixedUpdateActors)
            fixedUpdateActor.ManualFixedUpdate(Time.fixedDeltaTime);
    }

    public void Unregister(IUpdate actor)
    {
        _updateActors.Remove(actor);
    }

    public void Unregister(IFixedUpdate actor)
    {
        _fixedUpdateActors.Remove(actor);
    }
    
    public void Unregister<T>(T actor) where T : IUpdate, IFixedUpdate
    {
        Unregister(actor as IUpdate);
        Unregister(actor as IFixedUpdate);
    }
}