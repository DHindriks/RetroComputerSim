using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tasks 
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

public class Task
{
    public bool Isvalid;
    public Task MainTask;
    public List<Identifiers> ReqIdentifiers;

}

public class TaskGenerator : MonoBehaviour
{
    [SerializeField] float TaskInvalidChance;
    Task CurrentTask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void GenerateTask()
    {
        CurrentTask = new Task();


    }
}
