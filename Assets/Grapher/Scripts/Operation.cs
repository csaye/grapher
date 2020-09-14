namespace Grapher
{
    public class Operation
    {
        public static bool IsOperator(char ch)
        {
            return !IsNumber(ch) && !IsVariable(ch);
        }

        public static bool IsNumber(char ch)
        {
            return char.IsNumber(ch);
        }

        public static bool IsVariable(char ch)
        {
            return ch == 'x' || ch == 'y';
        }

        public static string ReplaceChar(string str, int index, char ch)
        {
            string str1 = str.Substring(0, index);
            string str2 = str.Substring(index + 1);
            return $"{str1}{ch}{str2}";
        }
    }
}
