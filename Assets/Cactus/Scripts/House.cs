using System;
using System.Collections.Generic;
using UnityEngine;
using Tools;

[RequireComponent(typeof(RectCollider))]
public class House : Decor
{
    [Header("Infos")]
    [SerializeField] Transform[] _spots;

    List<Interactable> _interactables = new List<Interactable>();
    
    public override void Init(RandomGenerator random)
    {
        base.Init(random);

        int interactableCount = random.Next(_spots.Length);
        _spots.Shuffle(random);

        for(int i = 0; i < interactableCount; i++)
        {
            Interactable interactable = InteractableLibrary.instance.GetOne(random);
            interactable.transform.position = _spots[i].position;

            _interactables.Add(interactable);
        }
    }
}