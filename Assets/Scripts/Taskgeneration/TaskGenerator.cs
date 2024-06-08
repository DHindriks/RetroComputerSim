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

public enum ProofTypes
{
    ID,
    Employment,
    Income,
    GuardianIncome,
    Diploma,
    Residence,
}

public class TaskGenerator : MonoBehaviour
{
    [SerializeField] List<MainTaskScriptableObject> PossibleTasks;

    [SerializeField] List<ProofScriptableObject> IDProofs;
    [SerializeField] List<ProofScriptableObject> EmploymentProofs;
    [SerializeField] List<ProofScriptableObject> IncomeProofs;
    [SerializeField] List<ProofScriptableObject> GuardianIncomeProofs;
    [SerializeField] List<ProofScriptableObject> DiplomaProofs;
    [SerializeField] List<ProofScriptableObject> ResidenceProofs;

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
        CurrentTask.Proofs = new List<ProofScriptableObject>();

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
            foreach(ProofTypes proof in CurrentTask.MTask.RequiredProofs)
            {
                 switch(proof)
                {
                    case ProofTypes.ID:
                        CurrentTask.Proofs.Add(IDProofs[Random.Range(0, IDProofs.Count)]);
                        break;
                    case ProofTypes.Employment:
                        CurrentTask.Proofs.Add(EmploymentProofs[Random.Range(0, EmploymentProofs.Count)]);
                        break;
                    case ProofTypes.Income:
                        CurrentTask.Proofs.Add(IncomeProofs[Random.Range(0, IncomeProofs.Count)]);
                        break;
                    case ProofTypes.GuardianIncome:
                        CurrentTask.Proofs.Add(GuardianIncomeProofs[Random.Range(0, GuardianIncomeProofs.Count)]);
                        break;
                    case ProofTypes.Diploma:
                        CurrentTask.Proofs.Add(DiplomaProofs[Random.Range(0, DiplomaProofs.Count)]);
                        break;
                    case ProofTypes.Residence:
                        CurrentTask.Proofs.Add(ResidenceProofs[Random.Range(0, ResidenceProofs.Count)]);
                        break;
                }
            }
        }else
        {

        }


    }
}
