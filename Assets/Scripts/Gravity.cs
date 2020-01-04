using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    enum GravityDirection { Down, Left, Up, Right };
    static GravityDirection m_GravityDirection;

    void Start()
    {
        m_GravityDirection = GravityDirection.Down;
    }

    private void Update()
    {
        if (Input.anyKeyDown)
            InputIdentifier();
    }

    void GravitySwitch()
    {
        switch (m_GravityDirection)
        {
            case GravityDirection.Down:
                Physics2D.gravity = new Vector2(0, -9.8f);
                transform.eulerAngles = new Vector3(0, 0, 0);
                break;

            case GravityDirection.Left:
                Physics2D.gravity = new Vector2(-9.8f, 0);
                transform.eulerAngles = new Vector3(0, 0, 270f);
                break;

            case GravityDirection.Up:
                Physics2D.gravity = new Vector2(0, 9.8f);
                transform.eulerAngles = new Vector3(0, 0, 180f);
                break;

            case GravityDirection.Right:
                Physics2D.gravity = new Vector2(9.8f, 0);
                transform.eulerAngles = new Vector3(0, 0, 90f);
                break;
        }
    }

    void InputIdentifier()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            m_GravityDirection = GravityDirection.Down;

        else if (Input.GetKeyDown(KeyCode.Alpha2))
            m_GravityDirection = GravityDirection.Up;

        else if (Input.GetKeyDown(KeyCode.Alpha3))
            m_GravityDirection = GravityDirection.Left;

        else if (Input.GetKeyDown(KeyCode.Alpha4))
            m_GravityDirection = GravityDirection.Right;

        GravitySwitch();
    }
}
