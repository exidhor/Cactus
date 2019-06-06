using System;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public class InteractableLibrary : MonoSingleton<InteractableLibrary>
{
    [SerializeField] Interactable[] _interactables;

    public Interactable GetOne(RandomGenerator random)
    {
        int index = random.Next(_interactables.Length);

        Interactable model = _interactables[index];
        Interactable interactable = Instantiate(model);
        interactable.transform.parent = transform;

        return interactable;
    }
}