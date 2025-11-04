using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Scriptable Objects/Dialogue")]
public class DialogueLine : ScriptableObject
{
    public string speakerName;
    public string dialogueLine;
}
