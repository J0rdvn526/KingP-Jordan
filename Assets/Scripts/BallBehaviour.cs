using UnityEngine;

public class BallBehaviour : MonoBehaviour {

    public float minX = -8.68f;
    public float minY = -4.43f;
    public float maxX = 8.62f;
    public float maxY = 4.42f;
    public float minSpeed;
    public float maxSpeed;
    public Vector2 targetPosition;

    public int secondsToMaxSpeed;

    public GameObject target;
    public float minLaunchSpeed;
    public float maxLaunchSpeed;
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
        initialPosition();
    }

    void FixedUpdate()
    {
        body = GetComponent<Rigidbody2D>();
        Vector2 currentPosition = body.position;
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

        float distance = Vector2.Distance((Vector2)transform.position, targetPosition);
        // float distance = Vector2.Distance(currentPosition, targetPosition);
        if (distance > 0.1f) {
            float difficulty = getDifficultyPercentage();
            float currentSpeed;
            if (launching == true) {
                float timeLaunching = Time.time - timeLaunchStart;
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

            /*
            Vector2 getRandomPositionInitial() {
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            if (randomX == && randomY ==) {
                getRandomPosition();
            } else {
            targetPosition = new Vector2(randomX, randomY);
            return targetPosition;
            }
        }
        */

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
            string collided = collision.gameObject.tag;
            Debug.Log(this + "Collided with: " + collision.gameObject.tag);
            if (collided == "Wall") {
                targetPosition = getRandomPosition();
            }
            if (collided == "Ball") {
                reroute(collision);
            }
        }

        private void OnCollisionStay2D(Collision2D collision) {
            if (collision.gameObject.tag == "Wall") {
                Debug.Log(this + " still collided with " + collision.gameObject.tag);
                targetPosition = getRandomPosition();

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

        public void setBounds(float miX, float maX, float miY, float maY) {
            minX = miX;
            maxX = maX;
            minY = miY;
            maxY = maY;
        }

        public void setTarget(GameObject pin) {
            target = pin;
        }
}
