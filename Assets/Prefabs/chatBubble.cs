using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;





public class chatBubble : MonoBehaviour
{
    public SpriteRenderer backgroundSpriteRenderer;

    public TextMeshPro PLMText;
    private string textToWrite;
    private int characterIndex;
    private float timePerCharacter;
    private float timer;
    private bool invisibleCharacters;

    public void AddWriter(TextMeshPro PLMText, string textToWrite, float timePerCharacter, bool invisibleCharacters)
    {
        this.PLMText = PLMText;
        this.textToWrite = textToWrite;
        this.timePerCharacter = timePerCharacter;
        this.invisibleCharacters = invisibleCharacters;
        characterIndex = 0;
        PLMText.SetText(textToWrite);
        PLMText.ForceMeshUpdate();
        Vector2 textSize = PLMText.GetRenderedValues(false);
        Vector2 padding = new Vector2(6f, 2f);
        backgroundSpriteRenderer.size = textSize + padding;
    }

    private void Start(){
        //Setup("This should go on first lineasdasdasdasdadsa \n This on the second \n and this is shit");
        PLMText.text = "cacat cu multa muie";
        AddWriter(PLMText, "ciobanica cu cacat,asdasdasdasdasdasdasdasdasdadsadasdas", .05f, true);
    }

    private void Update(){
        if(PLMText != null){
            timer -= Time.deltaTime;
            while (timer <= 0f){
                timer += timePerCharacter;
                characterIndex++;
                string text = textToWrite.Substring(0, characterIndex);
                if(invisibleCharacters)
                {
                    text += "<color=#00000000>" + textToWrite.Substring(characterIndex) + "/color";
                }
                PLMText.text = text;

                if(characterIndex >= textToWrite.Length){
                    PLMText = null;
                    return;
                }
            }
        }
    }
    
   /* private void Setup(string text){
        textMeshPro.SetText(text);
        textMeshPro.ForceMeshUpdate();
        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(6f, 2f);
        backgroundSpriteRenderer.size = textSize + padding;
    } */
}
