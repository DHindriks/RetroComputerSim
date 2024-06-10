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
    public string ClientFirstName;
    public string ClientLastName;
    public string ClientDateOfBirth;
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
    [SerializeField] PRMeter prmeter;
    [SerializeField] GameObject BadRepObj;
    [SerializeField] GameObject GoodRepobj;
    
    
    [SerializeField] GameObject WalletContainer;
    [SerializeField] GameObject JudgedProofWallet;
    [SerializeField] TextMeshProUGUI MainTaskText;
    [SerializeField] TextMeshProUGUI ClientText;
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
    [SerializeField] int InvalidMistakesAmount;
    int CurrentMistakes;

    Task CurrentTask;
    [Space(10), Header("Client values")]
    [SerializeField] List<string> FirstNames;
    [SerializeField] List<string> LastNames;
    [SerializeField] List<string> Adresses;

    [SerializeField] Color ValidColor;
    [SerializeField] Color InvalidColor;



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

        Debug.Log("Task generated as: " + CurrentTask.Isvalid);

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
        ProofScriptableObject PickedProof;
        foreach (ProofTypes proof in CurrentTask.MTask.OversharedProofs)
        {
            switch (proof)
            {
                case ProofTypes.ID:
                    PickedProof = IDProofs[Random.Range(0, IDProofs.Count)];
                    PickedProof.Overshared = true;
                    CurrentTask.Proofs.Add(PickedProof);
                    break;
                case ProofTypes.Employment:
                    PickedProof = EmploymentProofs[Random.Range(0, EmploymentProofs.Count)];
                    PickedProof.Overshared = true;
                    Debug.Log("Employment overshared");
                    CurrentTask.Proofs.Add(PickedProof);
                    break;
                case ProofTypes.Income:
                    PickedProof = IncomeProofs[Random.Range(0, IncomeProofs.Count)];
                    PickedProof.Overshared = true;
                    CurrentTask.Proofs.Add(PickedProof);
                    break;
                case ProofTypes.GuardianIncome:
                    PickedProof = GuardianIncomeProofs[Random.Range(0, GuardianIncomeProofs.Count)];
                    PickedProof.Overshared = true;
                    CurrentTask.Proofs.Add(PickedProof);
                    break;
                case ProofTypes.Diploma:
                    PickedProof = DiplomaProofs[Random.Range(0, DiplomaProofs.Count)];
                    PickedProof.Overshared = true;
                    CurrentTask.Proofs.Add(PickedProof);
                    break;
                case ProofTypes.Residence:
                    PickedProof = ResidenceProofs[Random.Range(0, ResidenceProofs.Count)];
                    PickedProof.Overshared = true;
                    CurrentTask.Proofs.Add(PickedProof);
                    break;
            }
        }


        //Generate Client values
        CurrentTask.ClientFirstName = ReturnRandomFromList(FirstNames);
        CurrentTask.ClientLastName = ReturnRandomFromList(LastNames);
        CurrentTask.ClientFullName = CurrentTask.ClientFirstName + " " + CurrentTask.ClientLastName;
        CurrentTask.ClientDateOfBirth = GenerateDateOfBirth();
        CurrentTask.ClientAdress = GenerateAdress();

        MainTaskText.text = "Huidige taak: " + CurrentTask.MTask.name;
        ClientText.text = "Client: " + CurrentTask.ClientFullName;

        //place client data in the proofs, create proof cards in wallet, scramble/remove some data if invalid
        for (int i = 0; i < CurrentTask.Proofs.Count; i++)
        {
            GameObject proofcard = Instantiate(ProofCard, WalletContainer.transform);
            proofcard.GetComponent<CredentialScript>().Linkedproof = CurrentTask.Proofs[i];
            GameObject prooftitle = Instantiate(ProofTitle, proofcard.transform);
            prooftitle.GetComponentInChildren<TextMeshProUGUI>().text = CurrentTask.Proofs[i].ProofName;
            CurrentTask.Proofs[i].Scrambled = false;
            CurrentTask.Proofs[i].Valid = true;


            for (int j = 0; j < CurrentTask.Proofs[i].ProofIdentifiers.Count; j++)
            {

                //writes data into the proofs
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

                //generates mistakes
                if (!CurrentTask.Isvalid && CurrentMistakes < InvalidMistakesAmount)
                {
                    float mistakeRNG = Random.value;
                    if (mistakeRNG < .75f)
                    {
                        switch (CurrentTask.Proofs[i].ProofIdentifiers[j].identifier)
                        {
                            case (Identifiers.ClientName):
                                CurrentTask.Proofs[i].ProofIdentifiers[j].value = CurrentTask.ClientFirstName + " " + ReturnRandomFromList(LastNames);
                                CurrentTask.Proofs[i].Scrambled = true;
                                break;
                            case (Identifiers.ClientDateOfBirth):
                                CurrentTask.Proofs[i].ProofIdentifiers[j].value = GenerateDateOfBirth();
                                CurrentTask.Proofs[i].Scrambled = true;
                                break;
                            case (Identifiers.ClientAdress):
                                CurrentTask.Proofs[i].ProofIdentifiers[j].value = GenerateAdress();
                                CurrentTask.Proofs[i].Scrambled = true;
                                break;
                        }

                    }
                }

                //generates identifiers in UI
                if (CurrentTask.Proofs[i].ProofIdentifiers[j].SpriteValue != null)
                {
                    GameObject identifiercard = Instantiate(IdentifierImgCard, proofcard.transform);
                    identifiercard.transform.GetChild(0).GetComponent<Image>().sprite = CurrentTask.Proofs[i].ProofIdentifiers[j].SpriteValue;
                }
                else
                {
                    GameObject identifiercard = Instantiate(IdentifierTextCard, proofcard.transform);
                    identifiercard.GetComponentInChildren<TextMeshProUGUI>().text = CurrentTask.Proofs[i].ProofIdentifiers[j].Name + ": " + CurrentTask.Proofs[i].ProofIdentifiers[j].value;
                }
            }
            GameObject proofbuttons = Instantiate(ProofButtons, proofcard.transform);
            proofbuttons.GetComponent<ProofValidBtns>().Valid.onClick.AddListener(delegate {
                //proofcard.transform.SetParent(JudgedProofWallet.transform);
                proofcard.GetComponent<CredentialScript>().SetValid(true);
                ChangeImgColor(proofcard.GetComponent<Image>(), ValidColor);
            });

            proofbuttons.GetComponent<ProofValidBtns>().Invalid.onClick.AddListener(delegate { 
                //proofcard.transform.SetParent(JudgedProofWallet.transform);
                proofcard.GetComponent<CredentialScript>().SetValid(false);
                ChangeImgColor(proofcard.GetComponent<Image>(), InvalidColor);
            });
        }
    
    }

    string ReturnRandomFromList(List<string> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    string GenerateDateOfBirth()
    {
        return Random.Range(1, 28) + "/" + Random.Range(1, 13) + "/" + Random.Range(1990, 2014);
    }

    string GenerateAdress()
    {
        return ReturnRandomFromList(Adresses) + " " + Random.Range(1, 40);
    }

    void ChangeImgColor(Image img, Color newclr)
    {
        img.color = newclr;
    }

    public void SubmitApproved()
    {
        foreach(ProofScriptableObject proof in CurrentTask.Proofs)
        {
            if (proof.Scrambled == true || proof.Overshared == true && proof.Valid)
            {
                prmeter.IncreasePR(-4);
                BadRepObj.SetActive(true);
                Debug.Log("You lost, you approved an invalid application");
                return;
            }
        }
        prmeter.IncreasePR(4);
        GoodRepobj.SetActive(true);
        Debug.Log("You won, submission approved");
    }

    public void SubmitDenied()
    {
        foreach (ProofScriptableObject proof in CurrentTask.Proofs)
        {
            if (proof.Scrambled == true || proof.Overshared == true)
            {
                Debug.Log("You won, submission Denied");
                prmeter.IncreasePR(4);
                GoodRepobj.SetActive(true);
                return;
            }
        }
        Debug.Log("You lost, You denied a valid application");
        prmeter.IncreasePR(-4);
        BadRepObj.SetActive(true);
    }
}
