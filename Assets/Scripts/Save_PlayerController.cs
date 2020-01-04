/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;

    float timerRotation;
    float lenghtRotation;
    float moveInput;
    Rigidbody2D rb;

    enum GravityDirection { Down, Left, Up, Right };
    GravityDirection m_GravityDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        m_GravityDirection = GravityDirection.Down;
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        if (m_GravityDirection == GravityDirection.Down || m_GravityDirection == GravityDirection.Up)
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        else
            rb.velocity = new Vector2(rb.velocity.x, moveInput * speed);
        
    }

    private void Update()
    {
        if (Input.anyKeyDown)
            InputIdentifier();
        timerRotation -= Time.deltaTime;
        if (timerRotation > 0)
            transform.Rotate(new Vector3(0, 0, lenghtRotation) * Time.deltaTime);
        Debug.Log(timerRotation);
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
                Rotation(180f);
  //              transform.eulerAngles = new Vector3(0, 0, 180f);
                break;

            case GravityDirection.Right:
                Physics2D.gravity = new Vector2(9.8f, 0);
                transform.eulerAngles = new Vector3(0, 0, 90f);
                SceneManager.LoadScene("player");
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
        else
            return;
        GravitySwitch();
    }

    void Rotation(float finalAngle)
    {
        timerRotation = 0.5f;
        lenghtRotation = (transform.rotation.eulerAngles.z - finalAngle) / timerRotation;
        // while (timerRotation >= 0)                                                   //This while loop make Unity crashes, so i duplicate it in the Update
        //   transform.Rotate(new Vector3(0, 0, lenghtRotation) * Time.deltaTime);   //But it sucks, it's not accurate, depending of the frame per second
    }
}
*/
