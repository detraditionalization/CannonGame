using UnityEngine;

public class Ball : PooledObject
{
    public float BallTimer;
    public GameObject BallTrack;
    public SpriteRenderer TrackRenderer;

    private SpriteRenderer _rend;
    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;

    public static int Combo { get; set; }
    public static Ball Instance { get; private set; }
    public Rigidbody2D Body { get; private set; }

    private void Awake()
    {
        Instance = this;
        BallTimer = 0f;
        Combo = 0;
        Body = GetComponent<Rigidbody2D>();
        transform.parent = null;
        TrackRenderer = null;
        _rend = GetComponent<SpriteRenderer>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        TrackRenderer.transform.localScale *= 2f;     // double the size of the ball track when theres a collision

        if (collision.collider.GetComponent<Obstacle>() != null)
        {
            // Count the combo
            Combo += 1;
        }
    }

    private void FixedUpdate()
    {
        CreateTracks();
        DestroyIfStuck();
        DestroyIfOutOfBounds(1f);
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private void Update()
    {
        // Debug : Gravity disabler
        if (Input.GetKeyDown(KeyCode.G))
        {
            Body.gravityScale = 0f;
        }

        BallTimer += 1f * Time.deltaTime;
    }

    private void CreateTracks()
    {
        if (BallTrack != null)
        {
            var createdBallTrack = Instantiate(BallTrack, transform.position, Quaternion.identity) as GameObject;
            TrackRenderer = createdBallTrack.GetComponent<SpriteRenderer>();
            TrackRenderer.color = Color.HSVToRGB((BallTimer * 0.5f) % 1f, 1f, 1f);
        }
    }

    private void DestroyIfOutOfBounds(float offset)
    {
        //Check for minimum and maximum y value
        if (transform.position.y + offset < -Camera.main.orthographicSize || transform.position.y - offset > Camera.main.orthographicSize)
        {
            gameObject.SetActive(false);
        }
    }

    private void DestroyIfStuck()
    {
        if (Body.velocity.magnitude < 1f && Body.velocity.x < 0.05f)
        {
            Destroy(gameObject);
        }
    }
}