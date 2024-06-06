using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ProofIdentifier
{
    public Identifiers identifier;
    public string value;
}

[CreateAssetMenu(fileName = "Proof", menuName = "Proof", order = 2)]
public class ProofScriptableObject : ScriptableObject
{
    public string ProofName;
    [TextArea(10, 15)] public string ProofDescription;
    public List<ProofIdentifier> ProofIdentifiers; 
}
