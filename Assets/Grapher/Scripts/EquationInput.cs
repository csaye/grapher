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

            if (IsValidEquation(equation))
            {
                string formattedEquation = FormatEquation(equation);
                equationGrapher.GraphEquation(formattedEquation);
            }
            else equationGrapher.ClearEquation();
        }

        private string FormatEquation(string equation)
        {
            string newEquation = equation;
            int length = newEquation.Length - 1;
            
            for (int i = 0; i < length; i++)
            {
                char chA = newEquation[i];
                char chB = newEquation[i + 1];
                if (Operation.IsNumber(chA) && Operation.IsVariable(chB))
                {
                    newEquation = Operation.InsertChar(newEquation, i + 1, '*');
                    length++;
                }
            }
            
            string[] sides = newEquation.Split('=');
            if (!sides[0].Contains('x')) return $"{sides[1]}={sides[0]}";
            else return newEquation;
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
            if (leftX && rightX) return false;
            if (leftY && rightY) return false;
            if (!leftX && !rightX) return false;
            if (!leftY && !rightY) return false;

            if (Operation.IsOperator(left[0]) || Operation.IsOperator(left[left.Length - 1])) return false;
            if (Operation.IsOperator(right[0]) || Operation.IsOperator(right[right.Length - 1])) return false;

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
                if (Operation.IsOperator(chA) && Operation.IsOperator(chB)) return false;
                if (Operation.IsVariable(chA) && Operation.IsNumber(chB)) return false;
                if (chA == '/' && chB == '0') return false;
            }
            return true;
        }
    }
}
