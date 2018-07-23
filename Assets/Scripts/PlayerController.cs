using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public const float BASE_SPEED = 50; //Default speed multiplier of the player
    public float MAX_VELOCITY = 20; //Maximum velocity the player can move at

    private const float SPEEDUP_MAX_VELOCITY = 30;
    private const float SPEEDUP_SPEED = 100;

    private float speed; //How fast the player picks up speed (multiplied by the movement vector)
    public Text countText;
    public Text winText;

    private Rigidbody rb;
    private int count; //How many coins the player has
    private bool isSpedUp; //If the player is currently sped up
    private float speedUpTime = 3; //Seconds the player speeds up upon getting a speed up object

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        this.speed = BASE_SPEED;
        this.count = 0;
        SetCountText();
        winText.text = "";
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0.0f;
        movement.Normalize();

        //Speeding the player up for a certain amount of time (specified as speedUpTime)
        if(this.isSpedUp)
        {
            this.speedUpTime -= Time.smoothDeltaTime;
            if (this.speedUpTime >= 0)
            {
                this.speed = SPEEDUP_SPEED;
                this.MAX_VELOCITY = SPEEDUP_MAX_VELOCITY;
            }
            else
            {
                resetSpeed();
                this.isSpedUp = false;
            }
        }

        rb.AddForce(movement * speed);

        //Making sure the player's speed is capped.
        Vector3 clampedVel = rb.velocity;
        clampedVel.x = Mathf.Clamp(clampedVel.x, -MAX_VELOCITY, MAX_VELOCITY);
        clampedVel.z = Mathf.Clamp(clampedVel.z, -MAX_VELOCITY, MAX_VELOCITY);
        rb.velocity = clampedVel;
        Debug.Log(rb.velocity);
    }

    //Handle object collisions
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }

        if (other.gameObject.CompareTag("Speed Up"))
        {
            other.gameObject.SetActive(false);
            this.isSpedUp = true;
            this.speedUpTime = 3; //If the player picks up more even if it has a speed up, the timer will reset.
        }

        if(other.gameObject.CompareTag("Jump Pad"))
        {
            Vector3 jumpForce = new Vector3(0.0f, 2500.0f, 0.0f);
            rb.AddForce(jumpForce);
        }

        if (other.gameObject.CompareTag("Level End")) //If the player collides with the level ender
        {
            SetWinText();
        }
    }

    void resetSpeed()
    {
        this.speed = BASE_SPEED;
        this.MAX_VELOCITY = 100;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }

    void SetWinText()
    {
        if (count >= 12) //Making sure the player has all the coins
        {
            winText.text = "You Win!";
        }
    }
}