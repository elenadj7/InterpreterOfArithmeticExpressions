namespace InterpreterOfArithmeticExpressions;

public class Token
{
    private string type;
    private string value{get; set;}
    private int line{get; set;}

    public Token(string newType, string newValue, int newLine)
    {
        type = newType;
        value = newValue;
        line = newLine;
    }

    public string Gettype() 
    {
        return type;
    }

    public string GetValue()
    {
        return value;
    }

    public int GetLine()
    {
        return line;
    }

    public void Settype(string type)
    {
        this.type = type;
    }

    public void SetValue(string value)
    {
        this.value = value;
    }

    public void SetLine(int line)
    {
        this.line = line;
    }
}