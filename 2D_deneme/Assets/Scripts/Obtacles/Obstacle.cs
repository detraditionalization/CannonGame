using DG.Tweening;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Color Green = new Color(0f, 1f, 0f);
    private Color White = new Color(1f, 1f, 1f);

    public bool IsGreen;
    private SpriteRenderer _sprite;
    private Vector3 _origScale;
    private Sequence _seq;
    private SpriteRenderer _eyeSprite1;
    private Color EyeRed = new Color(1f, 0.6f, 0.6f);

    public bool CollisionUnderway { get; private set; }

    public enum ObstacleType
    {
        Triangle,
        Square,
        Circle,
    }

    private void Awake()
    {
        IsGreen = false;
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _eyeSprite1 = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
        _origScale = _sprite.transform.localScale;
        _seq = DOTween.Sequence();
    }

    private void Update()
    {
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Balls")) // when colliding with balls change color
        {
            var mag = collision.relativeVelocity.magnitude;
            var vect = new Vector3(mag * 0.01f, mag * 0.01f, 0f);
            // Fuck up one eye
            _eyeSprite1.DOColor(EyeRed, 0.1f);
            _eyeSprite1.flipX = true;

            // Bounce Animation with DOTween sequence. First it enlarges and then it bounces back
            // enlarge according to the relative speed of the colision
            _seq.Append(_sprite.transform.DOBlendableScaleBy(vect, 0.7f).SetEase(Ease.OutElastic));
            _seq.Append(_sprite.transform.DOBlendableScaleBy(-vect, 0.2f).SetEase(Ease.Linear));

            ChangeColor();
            CollisionUnderway = true;
            // send shake to the camera
            CameraController.Instance.Shake(mag / 100f, mag / 270f, 12);    // shake the camera
        }
    }

    public virtual void CollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Balls")) // when colliding with balls change color
        {
            CollisionUnderway = false;
        }
    }

    private void ChangeColor()
    {
        if (!GameManager.Instance.IsWinner)  // check win condition, if won dont change colors
        {
            if (!IsGreen)
            {
                _sprite.color = Green;  // if not green make the color green and change the tag to "Green"
                IsGreen = true;
                tag = "Green";
            }
            else
            {
                _sprite.color = White;  // default state is tagged "Gray"
                IsGreen = false;
                tag = "Gray";
            }
        }
    }
}