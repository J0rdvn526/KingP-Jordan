// Written by Jordan Jefferis
using UnityEngine;

public class PinBehaviour : MonoBehaviour
{
    public float speed;
    public float timeDashStart;
    public float dashSpeed = 1.0f;
    public float baseSpeed = 0.1f;
    public float dashDuration = 0.3f;
    public bool dashing;

    // Dash Cooldown Variables
    public static float cooldownRate = 5.0f;
    public static float cooldown;
    public float endLastDash;

    public Vector2 newPosition;
    public Vector3 mousePosG;
    Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        cam = Camera.main;
        dashing = false;
    }

    // Update is called once per frame
    void Update() {
        dash();

        mousePosG = cam.ScreenToWorldPoint(Input.mousePosition);
        newPosition = Vector2.MoveTowards(transform.position, mousePosG, speed * Time.fixedDeltaTime);
        transform.position = newPosition;
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        string collided = collision.gameObject.tag;
        Debug.Log(this + "Collided with: " + collision.gameObject.name);
        if (collided == "Ball" || collided == "Wall") {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        }
    }

    public void dash() {
        if (dashing == true) {
            float howLong = Time.time - timeDashStart;
            if (howLong > dashDuration) {
                dashing = false;
                speed = baseSpeed;
                // Set the dash cooldown
                endLastDash = Time.time;
                cooldown = cooldownRate;
            }
        } else {
            cooldown = cooldown - Time.deltaTime;

            if(cooldown < 0.0) {
                cooldown = 0.0f;
            }
            if (Input.GetMouseButtonDown(0) == true && cooldown == 0.0) {
                dashing = true;
                speed = dashSpeed;
                timeDashStart = Time.time;
            }
        }
    }
}
