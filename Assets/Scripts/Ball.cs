using UnityEngine;

public class Ball : MonoBehaviour {

    //config params
    [SerializeField] Paddle paddle1;
    [SerializeField] float launchXSpeed = 2f;
    [SerializeField] float launchYSpeed = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;

    //state
    Vector2 paddleToBallVector;
    private bool hasStarted = false;

    //cached component references
    AudioSource myAudioSource;
    Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start ()
    {
        myAudioSource = GetComponent<AudioSource>();
        myRigidbody = GetComponent<Rigidbody2D>();
        paddleToBallVector = transform.position - paddle1.transform.position;    
	}

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchBallOnClick();
        }
    }
    private void LaunchBallOnClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            myRigidbody.velocity = new Vector2(launchXSpeed, launchYSpeed);
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2
            (Random.Range(0f, randomFactor), 
            Random.Range(0f, randomFactor));

        if (hasStarted)
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigidbody.velocity += velocityTweak;
        }
    }
}
