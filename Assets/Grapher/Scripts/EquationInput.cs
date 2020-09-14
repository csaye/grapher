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

            if (!IsValue(left[0]) || !IsValue(left[left.Length - 1])) return false;
            if (!IsValue(right[0]) || !IsValue(right[right.Length - 1])) return false;

            if (ContainsConsecutiveNonValues(left)) return false;
            if (ContainsConsecutiveNonValues(right)) return false;

            return true;
        }

        private bool ContainsConsecutiveNonValues(string str)
        {
            for (int i = 0; i < str.Length - 2; i++)
            {
                if (!IsValue(str[i]) && !IsValue(str[i + 1])) return true;
            }
            return false;
        }

        private bool IsValue(char ch)
        {
            if (char.IsNumber(ch)) return true;
            if (ch == 'x' || ch == 'y') return true;
            return false;
        }
    }
}
