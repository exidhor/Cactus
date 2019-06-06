using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _speed;

    public void Actualize(float dt)
    {
        float direction = 0;

#if UNITY_ANDROID
        int touchCount = Input.touchCount;

        for(int i = 0; i < touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            Vector2 viewportPosition = Camera.main.ScreenToViewportPoint(touch.position);

            if(viewportPosition.x < 0.15f)
            {
                direction = -1;
            }
            else if(viewportPosition.x > 0.85f)
            {
                direction = 1;
            }
        }
#else
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
        {
            direction = -1;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            direction = 1;
        }
#endif

        float move = direction * _speed * dt;

        Vector3 pos = transform.position;
        pos.x += move;
        transform.position = pos;

        if(direction != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * direction;
            transform.localScale = scale;
        }
    }
}