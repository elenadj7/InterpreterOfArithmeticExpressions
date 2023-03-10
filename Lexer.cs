namespace InterpreterOfArithmeticExpressions;

 public class Lexer{
        private readonly string source;
        private int sourcePosition = 0;
        private int linePosition = 1;
        private List<Token> tokensCollection = new();

        public Lexer(string input){
            source = input;
        }

        public List<Token> GetTokensCollection(){
            return tokensCollection;
        }

        private char Next(){
            return source.ToCharArray()[sourcePosition];
        }

        private void GetOneToken(){
            char character = Next();

            if(character == '+'){
                tokensCollection.Add(new Token("plus", "+", linePosition));
            }
            if(character == '-'){
                tokensCollection.Add(new Token("minus", "-", linePosition));
            }
            if(character == '*'){
                tokensCollection.Add(new Token("multiply", "*", linePosition));
            }
            if(character == '/'){
                tokensCollection.Add(new Token("divide", "/", linePosition));
            }
            if(character == '('){
                tokensCollection.Add(new Token("left bracket", "(", linePosition));
            }
            if(character == ')'){
                tokensCollection.Add(new Token("right bracket", ")", linePosition));
            }
            if(character == ';'){
                tokensCollection.Add(new Token("semicolon", ";", linePosition));
            }
            if(character == '='){
                tokensCollection.Add(new Token("equals", "=", linePosition));
            }
            if(character == '\n'){
                linePosition++;
            }
            if(Char.IsDigit(character)){
                string name = Char.ToString(character);
                while(sourcePosition < source.Length && sourcePosition + 1 < source.Length && Char.IsDigit(source[sourcePosition + 1])){
                    name = name + Char.ToString(source[sourcePosition + 1]);
                    sourcePosition++;
                }
                tokensCollection.Add(new Token("digit", name, linePosition));
            }
            if(Char.IsLetter(character)){
                string name = Char.ToString(character);
                while(sourcePosition < source.Length && sourcePosition + 1 < source.Length && Char.IsLetterOrDigit(source[sourcePosition + 1])){
                    name = name + Char.ToString(source[sourcePosition + 1]);
                    sourcePosition++;
                }
                if(name == "print"){
                    tokensCollection.Add(new Token("print", name, linePosition));
                }
                else{
                    tokensCollection.Add(new Token("variable", name, linePosition));
                }
            }
        }

        public void MakeTokens(){
            while(sourcePosition < source.Length){
                GetOneToken();
                sourcePosition++;
            }
            tokensCollection.Add(new Token("end of file", "EOF", linePosition));
        }

        public void CheckForNeg(){
            int position = 0;
            while(position < tokensCollection.Count){
                if(tokensCollection[position].Gettype() == "minus" && (position == 0 || tokensCollection[position - 1].Gettype() == "minus" || tokensCollection[position - 1].Gettype() == "plus" 
                || tokensCollection[position - 1].Gettype() == "divide" || tokensCollection[position - 1].Gettype() == "multiply"
                || tokensCollection[position - 1].Gettype() == "semicolon" || tokensCollection[position - 1].Gettype() == "left bracket") && tokensCollection[position + 1].Gettype() == "digit"){

                    string newElement = tokensCollection[position].GetValue() + tokensCollection[position + 1].GetValue();
                    tokensCollection[position + 1].SetValue(newElement);
                    tokensCollection.Remove(tokensCollection[position]);
                }

                position++;
            }
        }

        public void PrintTokens(){
            foreach(var element in tokensCollection){
                Console.Out.WriteLine(element.Gettype() + "   " + element.Gettype() + "   " + element.Gettype());
            }
        }
    }