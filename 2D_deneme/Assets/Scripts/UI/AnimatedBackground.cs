using DG.Tweening;
using NextGenSprites;
using UnityEngine;

public class AnimatedBackground : MonoBehaviour
{
    public ShaderFloat ShaderValue;
    public float Duration;
    public float TargetValue;
    private Material _mat;

    private void Awake()
    {
        _mat = GetComponent<MeshRenderer>().material;
        _mat.DOFloat(TargetValue, ShaderValue.GetString(), Duration).SetEase(Ease.InOutFlash).SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
    }

    public void Animate()
    {
        _mat.DOFloat(TargetValue, ShaderValue.ToString(), Duration).SetEase(Ease.InOutFlash).SetLoops(-1, LoopType.Yoyo);
    }
}