using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AboutPanel : MonoBehaviour
{
    public GameObject SecretButton;
    public GameObject HeartPanel;
    public GameObject HearticleSystem;
    public Text AboutText;
    public Text InputText;
    public Light DirectionalLight;

    public static bool IsJemma;

    private bool TextChanged;

    private void Awake()
    {
        IsJemma = false;
        // about is deactivated by default

        if (SecretButton)
        {
            SecretButton.SetActive(false);
        }

        TextChanged = false;
    }

    private void Update()
    {
        if (!TextChanged)
        {
            var targetText = "Give me your name, horse-master," + "\r\n" + "and I shall give you mine." + "\r\n" + "-Gimli";
            AboutText.DOText(targetText, 5f, true, ScrambleMode.Lowercase).OnComplete(ActivateSecretButton);
            TextChanged = true;
        }
    }

    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 0.5f);
    }

    private void OnDisable()
    {
    }

    public void ExitAboutPanel()
    {
        TextChanged = false;
        SecretButton.SetActive(false);
        gameObject.SetActive(false);
    }

    public void ActivateSecretButton()
    {
        SecretButton.SetActive(true);
    }

    public void SecretActivate()
    {
        if (InputText.text == "Jemma" || InputText.text == "jemma" || InputText.text == "Jemma Forster" || InputText.text == "Jemma Claire Forster")
        {
            JemmaIsHere();
        }
    }

    private void JemmaIsHere()
    {
        IsJemma = true;
        HearticleSystem.SetActive(true);
        HeartPanel.SetActive(true);
        Debug.Log("Jemma is here!");
        DirectionalLight.DOColor(new Color(1f, 0f, 0f), 2f);
        ExitAboutPanel();
    }
}