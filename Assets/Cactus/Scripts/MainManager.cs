using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    [SerializeField] CityBuilder _cityBuilder;

    private void Start()
    {
        _cityBuilder.Generate();

        // to add other stuff
    }

    private void Update()
    {
        float dt = Time.deltaTime;

        Player.instance.Actualize(dt);
    }
}
