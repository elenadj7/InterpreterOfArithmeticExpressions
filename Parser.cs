namespace InterpreterOfArithmeticExpressions;

public class Parser
{
        private List<Token> tokensCollection = new();
        private Dictionary<string, int> variables = new Dictionary<string, int>();
        private int currentPosition = 0;
        private int brackets = 0;

        public Parser(Lexer lexer){
            tokensCollection = lexer.GetTokensCollection();
        }

        public IList<IExpression> ExpressionList(){
            List<IExpression> expressions = new();

            while(tokensCollection[currentPosition].Gettype() != "end of file"){
                if(tokensCollection[currentPosition].Gettype() == "print"){
                    int tmp = 0;
                    while(tokensCollection[currentPosition+1].Gettype() == "left bracket"){
                        tmp++;
                        currentPosition++;
                    }
                    currentPosition++;
                    foreach(var element in variables){
                        if(element.Key == tokensCollection[currentPosition].GetValue()){
                            Console.WriteLine(element.Value);
                        }
                    }
                   
                    while(tmp > 0){
                        currentPosition++;
                        tmp--;
                    }
                    currentPosition++;
                }
                if (tokensCollection[currentPosition].Gettype() == "digit"){
                    expressions.Add(Expression());
                }
                if(tokensCollection[currentPosition].Gettype() == "variable"){
                    currentPosition++;
                    currentPosition++;
                    int tmp1 = currentPosition;
                    IExpression exp = Expression();
                    expressions.Add(exp);
                    variables.Add(tokensCollection[tmp1 - 2].GetValue(), exp.value);
                }
                if(tokensCollection[currentPosition].Gettype() != "semicolon" || brackets != 0){
                   throw new Exception();
                }
                currentPosition++;
            }

            return expressions;
        }

        private IExpression Expression(){

            IExpression term = Term();

            if(tokensCollection[currentPosition].Gettype() == "right bracket"){
                brackets--;
                currentPosition++;
                return term;
            }
            if(tokensCollection[currentPosition].Gettype() == "plus"){
                currentPosition++;
                return new BinaryExpression(term, Expression(), "plus");
            }
            if(tokensCollection[currentPosition].Gettype() == "minus"){
                currentPosition++;
                
                if(tokensCollection[currentPosition].Gettype() == "digit" && (currentPosition + 1) < tokensCollection.Count && 
                tokensCollection[currentPosition + 1].Gettype() != "multiply" && tokensCollection[currentPosition + 1].Gettype() != "divide"){
                    BinaryExpression exp = new(term, Factor(), "minus");
                    tokensCollection[currentPosition].Settype("digit");
                    tokensCollection[currentPosition].SetValue(exp.value.ToString());
                    return Expression();
                }
                else{
                    return new BinaryExpression(term, Expression(), "minus");
                }
            }
            return term;
        }

        public IExpression Term(){


            if(tokensCollection[currentPosition].Gettype() == "left bracket"){
                brackets++;
                currentPosition++;
                IExpression expression = Expression();
                if(tokensCollection[currentPosition].Gettype() == "divide"){
                    currentPosition++;
                    return new BinaryExpression(expression, Term(), "divide");
                }
                if(tokensCollection[currentPosition].Gettype() == "multiply"){
                    currentPosition++;
                    return new BinaryExpression(expression, Term(), "multiply");
                }

                return expression;
            }
            
            IExpression factor = Factor();
            currentPosition++;

            if(tokensCollection[currentPosition].Gettype() == "multiply"){

                currentPosition++;
                return new BinaryExpression(factor, Term(), "multiply");
            }

            if(tokensCollection[currentPosition].Gettype() == "divide"){
                currentPosition++;

                if(tokensCollection[currentPosition].Gettype() == "digit" && (currentPosition + 1) < tokensCollection.Count){
                    BinaryExpression exp = new(factor, Factor(), "divide");
                    tokensCollection[currentPosition].Settype("digit");
                    tokensCollection[currentPosition].SetValue(exp.value.ToString());
                    return Expression();
                }
                else{
                    return new BinaryExpression(factor, Term(), "divide");
                }
            }
            return factor;
        }

        private IExpression Factor(){

            if(tokensCollection[currentPosition].Gettype() == "digit"){
                Literal node = new(){value = int.Parse(tokensCollection[currentPosition].GetValue()) };
                return node;
            }
            else if(tokensCollection[currentPosition].Gettype() == "variable"){
                Literal node = new(){value = variables[tokensCollection[currentPosition].GetValue()]};
                return node;
            }
            else throw new Exception();
        }
}