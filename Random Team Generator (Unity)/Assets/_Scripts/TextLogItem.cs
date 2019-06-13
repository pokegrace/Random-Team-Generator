using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// from c00pala on youtube; Scroll Menu Pt 4
public class TextLogItem : MonoBehaviour {

    public void SetText(string myText, Color myColor)
    {
        GetComponent<Text>().text = myText;
        GetComponent<Text>().color = myColor;
    }
}
