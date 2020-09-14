using System.Linq;
using TMPro;
using UnityEngine;

namespace Grapher
{
    [RequireComponent(typeof(TMP_InputField))]
    public class EquationInput : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private EquationGrapher equationGrapher = null;

        private TMP_InputField input;

        private void Start()
        {
            input = GetComponent<TMP_InputField>();
        }

        public void UpdateEquation()
        {
            string equation = input.text;

            if (IsValidEquation(equation)) equationGrapher.GraphEquation(equation);
            else equationGrapher.ClearEquation();
            // equationGrapher.GraphEquation(equation);
        }

        private bool IsValidEquation(string equation)
        {
            string[] sides = equation.Split('=');
            
            if (sides.Length != 2) return false;

            string left = sides[0];
            string right = sides[1];

            if (string.IsNullOrEmpty(left)) return false;
            if (string.IsNullOrEmpty(right)) return false;

            bool leftX = left.Contains('x');
            bool leftY = left.Contains('y');
            bool rightX = right.Contains('x');
            bool rightY = right.Contains('y');

            if (leftX && leftY) return false;
            if (rightX && rightY) return false;
            if (!leftX && !leftY) return false;
            if (!rightX && !rightY) return false;

            if (IsOperator(left[0]) || IsOperator(left[left.Length - 1])) return false;
            if (IsOperator(right[0]) || IsOperator(right[right.Length - 1])) return false;

            if (!HasValidConsecutives(left)) return false;
            if (!HasValidConsecutives(right)) return false;

            return true;
        }

        private bool HasValidConsecutives(string str)
        {
            for (int i = 0; i < str.Length - 1; i++)
            {
                char chA = str[i];
                char chB = str[i + 1];
                if (IsOperator(chA) && IsOperator(chB)) return false;
                if (IsVariable(chA) && IsNumber(chB)) return false;
            }
            return true;
        }

        private bool IsOperator(char ch)
        {
            return !IsNumber(ch) && !IsVariable(ch);
        }

        private bool IsNumber(char ch)
        {
            return char.IsNumber(ch);
        }

        private bool IsVariable(char ch)
        {
            return ch == 'x' || ch == 'y';
        }
    }
}
