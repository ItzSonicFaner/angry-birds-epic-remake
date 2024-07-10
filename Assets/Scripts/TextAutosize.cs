using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(Text))]
public class AutoSizeText : MonoBehaviour
{
    private Text uiText;
    private RectTransform rectTransform;

    void Start()
    {
        uiText = GetComponent<Text>();
        rectTransform = GetComponent<RectTransform>();
        ResizeTextToFit();
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (!Application.isPlaying)
        {
            uiText = GetComponent<Text>();
            rectTransform = GetComponent<RectTransform>();
            ResizeTextToFit();
        }
    }
#endif

    void ResizeTextToFit()
    {
        if (uiText == null || rectTransform == null)
            return;

        int originalFontSize = uiText.fontSize;

        // Decrease the font size until the text fits within the RectTransform
        while (uiText.preferredWidth > rectTransform.rect.width || uiText.preferredHeight > rectTransform.rect.height)
        {
            if (uiText.fontSize <= 1)
                break;

            uiText.fontSize--;
        }

        // If we decreased too much, increase until it just fits
        while (uiText.preferredWidth <= rectTransform.rect.width && uiText.preferredHeight <= rectTransform.rect.height && uiText.fontSize < originalFontSize)
        {
            uiText.fontSize++;
        }

        // One step back to ensure it fits
        uiText.fontSize--;
    }
}
