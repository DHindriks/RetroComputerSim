using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Task
{
    public bool Isvalid;
    public MainTaskScriptableObject MTask;
    public List<ProofScriptableObject> Proofs;

}

public enum Identifiers
{
    ClientName,
    ClientAdress,
    ClientDateOfBirth,
    ClientSignature,
    ClientEmployment,
    ClientGuardianIncome,
    ClientGuardianAdress,

    IssuerName,
    IssuerID,
    IssuerSignature,

    CertExpiryDate,
}


public class TaskGenerator : MonoBehaviour
{
    [SerializeField] List<MainTaskScriptableObject> PossibleTasks;
    [SerializeField] List<ProofScriptableObject> PossibleProofs;
    [SerializeField, Range(0, 1)] float TaskInvalidChance;
    Task CurrentTask;

    // Start is called before the first frame update
    void Start()
    {
        GenerateTask();
    }

    void GenerateTask()
    {
        CurrentTask = new Task();

        //decides if application will be valid(approved) or invalid(denied)
        float Randvalue = Random.value;
        if (Randvalue <= TaskInvalidChance)
        {
            CurrentTask.Isvalid = false;
        }else
        {
            CurrentTask.Isvalid = true;
        }

        //decides on the main task
        CurrentTask.MTask = PossibleTasks[Random.Range(0, PossibleTasks.Count)];

        //Picks proofs
        if (CurrentTask.Isvalid)
        {
            foreach(Identifiers identifier in CurrentTask.MTask.RequiredIdentifiers)
            {
                 
            }
        }else
        {

        }


    }
}
