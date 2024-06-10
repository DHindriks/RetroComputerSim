using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CredentialScript : MonoBehaviour
{
    public bool Scrambled = false;
    public bool Overshared = false;
    public ProofScriptableObject Linkedproof;

    public void SetValid(bool isvalid)
    {
        Linkedproof.Valid = isvalid;
    }
}
