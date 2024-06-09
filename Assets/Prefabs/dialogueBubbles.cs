using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Burst.CompilerServices;
using System.Threading;

namespace ChristinaCreatesGames.Typography.Typewriter
{
    [RequireComponent(typeof(TMP_Text))]


public class dialogueBubbles : MonoBehaviour
{
    [SerializeField] GameObject RCBlocker;

    public TMP_Text textBox;
    public int currentVisibleCharacterIndex;
    public Coroutine typewriterCoroutine;

    public WaitForSeconds simpleDelay;
    public WaitForSeconds _interpunctuationDelay;
    public float charactersPerSecond = 20;
    public float interpunctuationDelay = 0.5f;

    public bool CurrentlySkipping ;
    public WaitForSeconds skipDelay;

    [SerializeField] private bool quickSkip;
    [SerializeField] [Min(1)] private int skipSpeedup = 5;
    public bool isSkipped;
    public GameObject objToBeDestroyed;
    public GameObject nextObjToAppear;

    public void Awake ()
    {
        RCBlocker.SetActive(true);
        textBox = GetComponent<TMP_Text>();

        simpleDelay = new WaitForSeconds(1 / charactersPerSecond);
        _interpunctuationDelay = new WaitForSeconds(interpunctuationDelay);

        skipDelay = new WaitForSeconds(1 / (charactersPerSecond * skipSpeedup));

    }

    public void Start()
    {
        isSkipped = false;
        SetText(textBox.text);
        StartCoroutine(routine:pulamea());
    }


    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(textBox.maxVisibleCharacters != textBox.textInfo.characterCount -1)
            {
                Skip();    
            }

            if(isSkipped == true){
                Destroy(objToBeDestroyed);
                    if (nextObjToAppear != null)
                    {
                        nextObjToAppear.SetActive(true);
                    }else
                    {
                        RCBlocker.SetActive(false);
                    }
                }
        }
    }

    public void SetText(string text)
    {

        if(typewriterCoroutine != null)
            StopCoroutine(typewriterCoroutine);

        textBox.text = text;
        textBox.maxVisibleCharacters = 0;
        currentVisibleCharacterIndex = 0;

        typewriterCoroutine = StartCoroutine(routine:Typewriter());
    }

    private IEnumerator Typewriter()
    {
        TMP_TextInfo textInfo = textBox.textInfo;

        while (currentVisibleCharacterIndex < textInfo.characterCount + 1)
        {

            char character = textInfo.characterInfo[currentVisibleCharacterIndex].character;

            textBox.maxVisibleCharacters++;
            if (!CurrentlySkipping && (character == '?' || character == '.' || character == ','))
            {
                yield return _interpunctuationDelay;
            } else
            {
                yield return CurrentlySkipping ? skipDelay : simpleDelay;
            }

            currentVisibleCharacterIndex++;
        }
    }

    void Skip()
    {
        if(CurrentlySkipping)
            return;

        CurrentlySkipping = true;

        if(!quickSkip)
        {
            StartCoroutine(routine:SkipSpeedupReset());
            return;
        }

        StopCoroutine(typewriterCoroutine);
        textBox.maxVisibleCharacters = textBox.textInfo.characterCount;
    }

    public IEnumerator SkipSpeedupReset()
    {
        yield return new WaitUntil(() => textBox.maxVisibleCharacters == textBox.textInfo.characterCount -1);
        CurrentlySkipping = false;
    }

    public IEnumerator pulamea()
    {
        yield return new WaitUntil (() => textBox.maxVisibleCharacters == textBox.textInfo.characterCount);
        isSkipped = true;
    }

}
}
