using System.Collections;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public static BirdController instance;

    public float bounceForce;

    private Rigidbody2D myBody;

    private Animator anim;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip

            flyClip,
            pingClip,
            diedClip;

    private bool isAlive;

    private bool didFlap;

    public float flag = 0;

    public int score = 0;

    private GameObject spawner;

    // Start is called before the first frame update
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        isAlive = true;
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        _MakeInstance();
        spawner = GameObject.Find("SpawnerPipe");
    }

    void _MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void _BirdMovement()
    {
        if (isAlive)
        {
            if (didFlap)
            {
                didFlap = false;
                myBody.velocity = new Vector2(myBody.velocity.x, bounceForce);
                audioSource.PlayOneShot (flyClip);
            }
        }

        // if (Input.GetMouseButtonDown(0))
        // {
        //     myBody.velocity = new Vector2(myBody.velocity.x, bounceForce);
        //     audioSource.PlayOneShot (flyClip);
        // }
        if (myBody.velocity.y > 0)
        {
            float angle = 0;
            angle = Mathf.Lerp(0, 90, myBody.velocity.y / 7);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else if (myBody.velocity.y == 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (myBody.velocity.y < 0)
        {
            float angle = 0;
            angle = Mathf.Lerp(0, -90, -myBody.velocity.y / 7);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        _BirdMovement();
    }

    public void FlabButton()
    {
        didFlap = true;
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "PipeHolder")
        {
            audioSource.PlayOneShot (pingClip);
            score++;
            if (GamePLayController.instance != null)
                GamePLayController.instance._SetScore(score);
        }
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "Pipe" || target.gameObject.tag == "Ground"
        )
        {
            flag = 1;
            if (isAlive)
            {
                isAlive = false;
                audioSource.PlayOneShot (diedClip);
                anim.SetTrigger("Died");
                Destroy (spawner);
                if (GamePLayController.instance != null)
                    GamePLayController.instance._BirdDiedShowPanel(score);
            }
        }
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
