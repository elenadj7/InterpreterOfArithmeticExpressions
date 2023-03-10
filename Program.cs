namespace InterpreterOfArithmeticExpressions;

public class Program
{
    public static void Main(string[] args){
        String input = File.ReadAllText("test.txt");
        Lexer lexer = new Lexer(input);
        lexer.MakeTokens();
        lexer.CheckForNeg();
        //lexer.PrintTokens();


        Parser parser = new Parser(lexer);
        IList<IExpression> expressionList = parser.ExpressionList();
        
    }
}