using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskTypes 
{
    StudentLoanRequest,
}

public enum Identifiers
{
    ClientName,
    ClientAdress,
    ClientDateOfBirth,
    ClientSignature,
    ClientEmployment,

    IssuerName,
    IssuerID,
    IssuerSignature,

    CertExpiryDate,
}
public class MainTask
{
    public List<Proof> ReqIdentifiers;
}

public class Proof
{
    public List<Identifiers> ReqIdentifiers;
}

public class Task
{
    public bool Isvalid;
    public MainTask MTask;
    public List<Identifiers> ReqIdentifiers;

}


public class TaskGenerator : MonoBehaviour
{
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


    }
}
