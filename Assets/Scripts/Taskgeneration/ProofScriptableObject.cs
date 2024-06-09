using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProofIdentifier
{
    public Identifiers identifier;
    public string Name;
    public string value;
    public Sprite SpriteValue;
}

[CreateAssetMenu(fileName = "P_Proof", menuName = "Proof", order = 2)]
public class ProofScriptableObject : ScriptableObject
{
    public string ProofName;
    [TextArea(10, 15)] public string ProofDescription;
    public List<ProofIdentifier> ProofIdentifiers; 
}
