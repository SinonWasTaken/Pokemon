using UnityEngine;

[System.Serializable]
public class Question_Dialogue : Dialogue_Lines
{
    private string playerAnswer = "none";
    
    [SerializeField] private Answer positiveAnswer;
    [SerializeField] private Answer negativeAnswer;
    
    public override string getLine()
    {
        if (playerAnswer == "none")
        {
            return base.getLine();
        }
        else
        {
            if (playerAnswer == "yes")
            {
                return positiveAnswer.getLine();
            }
            else
            {
                return negativeAnswer.getLine();
            }
        }
    }

    public void SetAnswer(string answer)
    {
        playerAnswer = answer;
    }
}

[System.Serializable]
public class Answer
{
    [SerializeField] private string[] lines;
    [SerializeField] private bool repeatLineIfNegative;

    private int index;
    
    public string getLine()
    {
        string new_line = lines[index];
        index++;
        return new_line;
    }
    
    public string[] Lines => lines;
    public bool RepeatLine => repeatLineIfNegative;
}
