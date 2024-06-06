using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Task", menuName = "Main task", order = 1)]
public class MainTaskScriptableObject : ScriptableObject
{
    public string TaskName;
    [TextArea(10, 15)]public string TaskDescription;
    public List<Identifiers> RequiredIdentifiers;
}
