using UnityEngine;
using DG.Tweening;
using System.Collections;

public class ObstacleEye : MonoBehaviour {
    private Vector3 BallPos;
    private Sprite _parentSprite;

    void Awake () {
        
    }


    void Update()
    {
        // If theres a ball look at it
        if (GameObject.FindWithTag("Balls") != null)
        {
            BallPos = GameObject.FindWithTag("Balls").transform.position;
            LookAt(BallPos);
        }
        else
        {            
            LookAtMouse();
        }
    }

    public void LookAt(Vector3 BallPos)
    {
        // DowTween Look at implementation
        transform.DOKill();

        var dir = BallPos - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        var rot = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.DORotate(rot.eulerAngles, 0.06f).SetEase(Ease.Linear);
    }

    public void LookAtMouse()
    {
        // DowTween Look at implementation
        transform.DOKill();

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
        lookPos = lookPos - transform.position;
        float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        var rot = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.DORotate(rot.eulerAngles, 0.08f).SetEase(Ease.Linear);
    }
}
