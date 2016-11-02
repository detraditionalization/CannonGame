using UnityEngine;

public class CircleObstacle : Obstacle
{
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        // Reverse the direction of gravity for the ball
        collision.collider.GetComponent<Rigidbody2D>().gravityScale *= -1;
    }
}