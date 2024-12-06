using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FontManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI[] textElements;
    public float maxFontSize = 40f;
    public float minFontSize = 10f;

    void Start()
    {
        AdjustFontSize();
    }

    void AdjustFontSize()
    {
        float smallestFontSize = maxFontSize;

        foreach (var textElement in textElements)
        {
            textElement.enableAutoSizing = true;
            textElement.fontSizeMin = minFontSize;
            textElement.fontSizeMax = maxFontSize;
            textElement.ForceMeshUpdate();
            smallestFontSize = Mathf.Min(smallestFontSize, textElement.fontSize);
        }

        foreach (var textElement in textElements)
        {
            textElement.fontSize = smallestFontSize;
            textElement.enableAutoSizing = false;
        }
    }
}
