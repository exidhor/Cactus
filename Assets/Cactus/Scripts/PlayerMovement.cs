using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _speed;

    public void Actualize(float dt)
    {
        float direction = 0;

        if(Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
        {
            direction = -1;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            direction = 1;
        }

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