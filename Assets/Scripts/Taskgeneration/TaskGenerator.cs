using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Task
{
    public bool Isvalid;
    public MainTaskScriptableObject MTask;
    public List<ProofScriptableObject> Proofs;
    public string ClientFullName;
    public string ClientDateOfBirth = "04/09/2008";
    public string ClientAdress;

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
    IssuerLogo,

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
    [SerializeField] GameObject WalletContainer;
    [SerializeField] GameObject ProofCard;
    [SerializeField] GameObject ProofTitle;
    [SerializeField] GameObject ProofButtons;
    [SerializeField] GameObject IdentifierTextCard;
    [SerializeField] GameObject IdentifierImgCard;
    [SerializeField] List<MainTaskScriptableObject> PossibleTasks;

    [SerializeField] List<ProofScriptableObject> IDProofs;
    [SerializeField] List<ProofScriptableObject> EmploymentProofs;
    [SerializeField] List<ProofScriptableObject> IncomeProofs;
    [SerializeField] List<ProofScriptableObject> GuardianIncomeProofs;
    [SerializeField] List<ProofScriptableObject> DiplomaProofs;
    [SerializeField] List<ProofScriptableObject> ResidenceProofs;

    [SerializeField, Range(0, 1)] float TaskInvalidChance;
    Task CurrentTask;
    [Space(10), Header("Client values")]
    [SerializeField] List<string> FirstNames;
    [SerializeField] List<string> LastNames;
    [SerializeField] List<string> Adresses;




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

        //Overshare?

        //Generate Client values
        CurrentTask.ClientFullName = ReturnRandomFromList(FirstNames) + " " + ReturnRandomFromList(LastNames);
        CurrentTask.ClientDateOfBirth = Random.Range(1, 28) + "/" + Random.Range(1, 13) + "/" + Random.Range(1990, 2014);
        CurrentTask.ClientAdress = ReturnRandomFromList(Adresses) + " " + Random.Range(1, 40);

        //place client data in the proofs, create proof cards in wallet, scramble/remove some data if invalid
        for (int i = 0; i < CurrentTask.Proofs.Count; i++)
        {
            GameObject proofcard = Instantiate(ProofCard, WalletContainer.transform);
            GameObject prooftitle = Instantiate(ProofTitle, proofcard.transform);
            prooftitle.GetComponentInChildren<TextMeshProUGUI>().text = CurrentTask.Proofs[i].ProofName;
            for (int j = 0; j < CurrentTask.Proofs[i].ProofIdentifiers.Count; j++)
            {

                switch(CurrentTask.Proofs[i].ProofIdentifiers[j].identifier)
                {
                    case (Identifiers.ClientName):
                        CurrentTask.Proofs[i].ProofIdentifiers[j].value = CurrentTask.ClientFullName;
                        break;
                    case (Identifiers.ClientDateOfBirth):
                        CurrentTask.Proofs[i].ProofIdentifiers[j].value = CurrentTask.ClientDateOfBirth;
                        break;
                    case (Identifiers.ClientAdress):
                        CurrentTask.Proofs[i].ProofIdentifiers[j].value = CurrentTask.ClientAdress;
                        break;
                }

                if (CurrentTask.Proofs[i].ProofIdentifiers[j].SpriteValue != null)
                {
                    GameObject identifiercard = Instantiate(IdentifierImgCard, proofcard.transform);
                    identifiercard.GetComponentInChildren<Image>().sprite = CurrentTask.Proofs[i].ProofIdentifiers[j].SpriteValue;
                }
                else
                {
                    GameObject identifiercard = Instantiate(IdentifierTextCard, proofcard.transform);
                    identifiercard.GetComponentInChildren<TextMeshProUGUI>().text = CurrentTask.Proofs[i].ProofIdentifiers[j].Name + ": " + CurrentTask.Proofs[i].ProofIdentifiers[j].value;
                }
            }
            GameObject proofbuttons = Instantiate(ProofButtons, proofcard.transform);
        }
    
    }

    string ReturnRandomFromList(List<string> list)
    {
        return list[Random.Range(0, list.Count)];
    }
}
