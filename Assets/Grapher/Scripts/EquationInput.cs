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
        }

        private bool IsValidEquation(string equation)
        {
            string[] sides = equation.Split('=');
            
            if (sides.Length != 2) return false;

            if (string.IsNullOrEmpty(sides[0])) return false;
            if (string.IsNullOrEmpty(sides[1])) return false;

            bool leftX = sides[0].Contains('x');
            bool leftY = sides[0].Contains('y');
            bool rightX = sides[1].Contains('x');
            bool rightY = sides[1].Contains('y');

            if (leftX && leftY) return false;
            if (rightX && rightY) return false;
            if (!leftX && !leftY && !rightX && !rightY) return false;

            return true;
        }
    }
}
