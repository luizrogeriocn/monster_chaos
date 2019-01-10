using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 10.0f;
    private Rigidbody2D rb;
    private GameController gameController;
    private Vector3 currentAcceleration;
    private bool isMobile;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();

        currentAcceleration = Vector3.zero;
        isMobile = SystemInfo.supportsAccelerometer;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            gameController.PlayerDied();
        if (collision.gameObject.tag == "Food")
            gameController.PlayerGotFood();
    }

    // Update is called once per frame
    void Update()
    {
        float moveVertical = 0.0f;
        float moveHorizontal = 0.0f;
        
        if (isMobile)
        {
            currentAcceleration.x = Input.acceleration.x;
            currentAcceleration.y = Input.acceleration.y;

            moveHorizontal = Mathf.Clamp(currentAcceleration.x, -1, 1);
            moveVertical = Mathf.Clamp(currentAcceleration.y, -1, 1);
        }
        else
        {
            moveVertical = Input.GetKey(KeyCode.DownArrow) ? moveVertical - 1.0f : moveVertical;
            moveVertical = Input.GetKey(KeyCode.UpArrow) ? moveVertical + 1.0f : moveVertical;

            moveHorizontal = Input.GetKey(KeyCode.LeftArrow) ? moveHorizontal - 1.0f : moveHorizontal;
            moveHorizontal = Input.GetKey(KeyCode.RightArrow) ? moveHorizontal + 1.0f : moveHorizontal;
        }

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        rb.velocity = movement * speed;
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, 5.0f);
    }
}
