using UnityEngine;

public class BallBehaviour : MonoBehaviour {

    public float minX = -8.68f;
    public float minY = -4.43f;
    public float maxX = 8.62f;
    public float maxY = 4.42f;
    public float minSpeed;
    public float maxSpeed;
    Vector2 targetPosition;

    public int secondsToMaxSpeed;

    public GameObject target;
    public float minLaunchSpeed;
    public float maxLaunchSpeed;
    //public float minTimeToLaunch;
    //public float maxTimeToLaunch;
    public float cooldown;
    public bool launching;
    public float launchDuration;
    public float timeLaunchStart;
    public float timeLastLaunch;

    Rigidbody2D body;
    public bool rerouting;





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        //secondsToMaxSpeed = 30;
        //minSpeed = 0.5f;
        //maxSpeed = 1.0f;
        targetPosition = getRandomPosition();
    }

    void FixedUpdate()
    {
        if (onCooldown() == false) {
            if (launching == true) {
                float currentLaunchTime = Time.time - timeLaunchStart;
                if (currentLaunchTime > launchDuration) {
                    startCooldown();
                }
            } else {
                Debug.Log("unim");
                launch();
            }
        }

        Vector2 currentPosition = gameObject.GetComponent<Transform>().position;
        float distance = Vector2.Distance((Vector2)transform.position, targetPosition);
        if (distance > 0.1f) {
            float difficulty = getDifficultyPercentage();
            float currentSpeed;
            if (launching == true) {
                float timeLaunching = Time.time - timeLastLaunch;
                if (timeLaunching > launchDuration) {
                    startCooldown();
                }
                currentSpeed = Mathf.Lerp(minLaunchSpeed, maxLaunchSpeed, difficulty);
            } else {
                currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, difficulty);
            }
            currentSpeed = currentSpeed * Time.deltaTime;
            Vector2 newPosition = Vector2.MoveTowards(currentPosition, targetPosition, currentSpeed);
            body.MovePosition(newPosition);
            //transform.position = newPosition;
        } else {
            if (launching == true) {
                startCooldown();
            }
            targetPosition = getRandomPosition();
        }
    }

        Vector2 getRandomPosition() {
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            Debug.Log("rx: " + randomX + "ry: " + randomY);
            targetPosition = new Vector2(randomX, randomY);
            return targetPosition;
        }

        public float getDifficultyPercentage() {
            float difficulty = Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxSpeed);
            return difficulty;
        }

        public void launch() {
            Rigidbody2D targetBody = target.GetComponent<Rigidbody2D>();
            targetPosition = targetBody.position;

            if (launching == false) {
                timeLaunchStart = Time.time;
                launching = true;
            }
        }

        public bool onCooldown() {
            bool result = false;
            float timeSinceLastLaunch = Time.time - timeLastLaunch;

            if (timeSinceLastLaunch < cooldown) {
                result = true;
            }
            return result;
        }

        public void startCooldown() {
            timeLastLaunch = Time.time;
            launching = false;
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            Debug.Log(this + "Collided with: " + collision.gameObject.name);
            if (collision.gameObject.tag == "Wall") {
                targetPosition = getRandomPosition();
            }
            if (collision.gameObject.tag == "Ball") {
                reroute(collision);
            }
        }

        public void initialPosition() {
            body = GetComponent<Rigidbody2D>();
            body.position = getRandomPosition();
            targetPosition = getRandomPosition();
            launching = false;
            rerouting = true;
        }

        public void reroute(Collision2D collision) {
            GameObject otherBall = collision.gameObject;
            if (rerouting == true) {
                Rigidbody2D ballBody = otherBall.GetComponent<Rigidbody2D>();
                Vector2 contact = collision.GetContact(0).normal;
                targetPosition = Vector2.Reflect(targetPosition, contact).normalized;
                launching = false;
                float seperationDistance = 0.1f;
                ballBody.position += contact * seperationDistance;
            } else {
                rerouting = true;
            }
        }
}
