using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPart : MonoBehaviour
{
    
    public string hexColor = "#FF4500"; // Ejemplo: un tono naranja
        private void OnEnable()
    {
        // Variable para almacenar el objeto Color 
        Color newColor;
        ColorUtility.TryParseHtmlString(hexColor, out newColor);
        GetComponent<Renderer>().material.color = newColor;
    }
}
