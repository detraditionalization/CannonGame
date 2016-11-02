using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 _originalPos;
    public static CameraController Instance;
    public Texture2D fadeOutTexture;
    public float fadeSpeed = 1f;

    private int drawDepth = -1000;
    private float alpha = 1.0f;
    private int fadeDir = -1;  // fade in = -1  , fade out = 1
    private Camera _cam;
    private float _origSize;

    private void Awake()
    {
        if (Singleton.Instance == null)
        {
            GameObject obj = (GameObject)Resources.Load("ManagerSingletons");
            Instantiate(obj);
        }

        Instance = this;
        _originalPos = transform.localPosition;
        _cam = GetComponent<Camera>();
        _origSize = _cam.orthographicSize;
    }

    private void OnGUI()
    {
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
    }

    public float BeginFade(int direction)
    {
        fadeDir = direction;
        return fadeSpeed;
    }

    private void OnLevelWasLoaded()
    {
        BeginFade(-1);
        _cam.orthographicSize = 0.7f;
        _cam.DOOrthoSize(_origSize, 0.5f).SetEase(Ease.OutCirc);
    }

    public void Shake(float duration, float strength = 1, int vibrato = 10)
    {
        transform.DOShakePosition(duration, strength, vibrato, 8).SetEase(Ease.OutElastic).OnComplete(ReturnToOrigPos);
    }

    public void ReturnToOrigPos()
    {
        transform.DOMove(_originalPos, 1f).SetEase(Ease.Linear);
    }
}