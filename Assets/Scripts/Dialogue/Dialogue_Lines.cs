using UnityEngine;

[System.Serializable]
public class Dialogue_Lines
{
    [SerializeField] private string[] lines;

    [SerializeField] private int line;
    
    public Dialogue_Lines(params string[] lines)
    {
        this.lines = lines;
    }

    public void StartDialogue()
    {
        line = 0;
    }
    
    public virtual string getLine()
    {
        if (line != lines.Length)
        {
            string new_line = lines[line];
            line++;
            return new_line;
        }
        else
        {
            return "Done";
        }
    }
}
