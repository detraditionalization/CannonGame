using DG.Tweening;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    private void Awake()
    {
    }

    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 0.4f).SetEase(Ease.OutBack).SetUpdate(true);
    }

    private void Update()
    {
    }
}