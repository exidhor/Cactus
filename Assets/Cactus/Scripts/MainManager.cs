using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    [SerializeField] GameObject _youLoseScreen;

    [SerializeField] CityBuilder _cityBuilder;

    private void Awake()
    {
        _cityBuilder.Generate();

        // to add other stuff
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private void Update()
    {
        float dt = Time.deltaTime;

        GameCamera.instance.Actualize(dt);
        ColliderManager.instance.Clear();

        // move the world

        // add new colliders to the collider manager

        InteractableManager.instance.Actualize(dt);

        if(Player.instance.isAlive)
        {
            Player.instance.Actualize(dt);
        }

        TriggerZoneManager.instance.Actualize(dt);

        if (!Player.instance.isAlive)
        {
            if(!_youLoseScreen.activeInHierarchy)
            {
                _youLoseScreen.SetActive(true);
            }
        }
    }
}
