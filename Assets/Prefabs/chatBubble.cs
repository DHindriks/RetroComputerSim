using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class chatBubble : MonoBehaviour
{
    private SpriteRenderer backgroundSpriteRenderer;
    private TextMeshPro textMeshPro;
    private void Awake(){
        backgroundSpriteRenderer = transform.Find("chatBubbleBackground").GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }

    private void Start(){
    Setup("This should go on first line \n This on the second");
    }
    
    private void Setup(string text){
        textMeshPro.SetText(text);
        textMeshPro.ForceMeshUpdate();
        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2 (6f, 2f);
        backgroundSpriteRenderer.size = textSize + padding;
    }
}
