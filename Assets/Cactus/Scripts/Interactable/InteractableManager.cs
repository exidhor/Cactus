using System;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public class InteractableManager : MonoSingleton<InteractableManager>
{
    List<Interactable> _interactables = new List<Interactable>();

    public void Register(Interactable interactable)
    {
        _interactables.Add(interactable);
    }

    public void Unregister(Interactable interactable)
    {
        _interactables.Remove(interactable);
    }

    public void Actualize(float dt)
    {
        for(int i = 0; i < _interactables.Count; i++)
        {
            _interactables[i].Actualize(dt);
        }
    }
}