using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TitleImage : MonoBehaviour
{
    public Text TitleText;
    private Outline[] _outline;
    private Tween _titleWobble;

    private void Awake()
    {
        _titleWobble = TitleText.transform.DOBlendableScaleBy(Vector3.one * 0.2f, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
        _outline = TitleText.GetComponents<Outline>();
    }

    private void Update()
    {
        _outline[1].effectColor = Color.HSVToRGB(Time.unscaledTime * 0.5f % 1f, 1f, 1f);
        _outline[0].effectColor = Color.black;
        _outline[1].effectDistance = new Vector2(3f * Mathf.Sin(Time.unscaledTime), 3f * Mathf.Sin(Time.unscaledTime));

        JemmaCheck();
    }

    private void JemmaCheck()
    {
        if (AboutPanel.IsJemma)
        {
            TitleText.text = "I Love You Cutie!";
            TitleText.color = Color.HSVToRGB(Time.unscaledTime * 0.1f * Mathf.PI % 1f, 1f, 1f);
            TitleText.fontSize = 70;
            _titleWobble.SetEase(Ease.InOutElastic);
        }
        else
        {
            TitleText.text = "Cannon Game";
        }
    }
}