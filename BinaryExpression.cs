namespace InterpreterOfArithmeticExpressions;
    
public class BinaryExpression : IExpression{

    private IExpression left;
    private IExpression right;
    private string operation;

    public BinaryExpression(IExpression left, IExpression right, string operation){
        this.left = left;
        this.right = right;
        this.operation = operation;
    }

    public int value{
        get{
            return operation switch{
                "plus" => left.value + right.value,
                "minus" => left.value - right.value,
                "multiply" => left.value * right.value,
                "divide" => left.value / right.value,
                _ => throw new Exception(),
            };
        }
    }
}
