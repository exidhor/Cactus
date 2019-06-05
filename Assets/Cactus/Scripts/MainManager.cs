using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    [SerializeField] CityBuilder _cityBuilder;

    private void Awake()
    {
        _cityBuilder.Generate();

        // to add other stuff
    }

    private void Update()
    {
        float dt = Time.deltaTime;

        ColliderManager.instance.Clear();

        // move the world

        // add new colliders to the collider manager

        Player.instance.Actualize(dt);
        TriggerZoneManager.instance.Actualize(dt);
    }
}
