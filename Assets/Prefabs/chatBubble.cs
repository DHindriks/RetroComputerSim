using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class chatBubble : MonoBehaviour
{
    private SpriteRenderer backgroundSpriteRenderer;
    private TextMeshPro textMeshPro;
    private void Awake(){
        backgroundSpriteRenderer = transform.find("Background").GetComponent<SpriteRenderer>();
    }
}
