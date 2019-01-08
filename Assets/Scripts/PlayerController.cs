using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private float speed = 10.0f;
    private Rigidbody2D rb;
    private GameController gameController;
    private Vector3 initialAcceleration;
    private Vector3 currentAcceleration;
    private float smooth = 0.01f;
    private bool isMobile;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();

        initialAcceleration = Input.acceleration;
        currentAcceleration = Vector3.zero;
        isMobile = SystemInfo.supportsAccelerometer;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            SceneManager.LoadScene("MainMenu");
        }
        if (collision.gameObject.tag == "Food")
        {
            gameController.SpawnFood();
            gameController.SpawnEnemy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float moveVertical = 0.0f;
        float moveHorizontal = 0.0f;
        
        if (isMobile)
        {
             if (Input.touchCount > 0)
            {
                initialAcceleration = Input.acceleration;
                currentAcceleration = Vector3.zero;
            }

            currentAcceleration = Vector3.Lerp(currentAcceleration,
                Input.acceleration - initialAcceleration, Time.deltaTime / smooth);

            moveHorizontal = Mathf.Clamp(currentAcceleration.x, -1, 1);
            moveVertical = Mathf.Clamp(currentAcceleration.y, -1, 1);
            Debug.Log(currentAcceleration);
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
