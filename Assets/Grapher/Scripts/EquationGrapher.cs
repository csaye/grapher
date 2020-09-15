using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Grapher
{
    public class EquationGrapher : MonoBehaviour
    {
        [Header("Attributes")]
        [SerializeField] private float leftX = 0;
        [SerializeField] private float rightX = 0;
        
        [Header("References")]
        [SerializeField] private GameObject pointPrefab = null;
        
        private const int increments = 100;

        private GameObject[] points = new GameObject[increments];

        private void Start()
        {
            float xIncrement = (rightX - leftX) / increments;
            float x = leftX;
            int index = 0;

            for (int i = 0; i < increments; i++)
            {
                x += xIncrement;
                Vector3 pointPosition = new Vector3(x, 0, 0);
                GameObject pointObj = Instantiate(pointPrefab, pointPosition, Quaternion.identity, transform);
                pointObj.SetActive(false);
                points[index++] = pointObj;
            }
        }

        public void GraphEquation(string equation)
        {
            Debug.Log($"graphing {equation}");
            foreach (GameObject point in points)
            {
                point.SetActive(true);
                float pointX = point.transform.position.x;
                float pointY = SolveForY(pointX, equation);
                point.transform.position = new Vector3(pointX, pointY, 0);
            }
        }

        public void ClearEquation()
        {
            foreach (GameObject point in points) point.SetActive(false);
        }

        private float SolveForY(float x, string equation)
        {
            string[] sides = equation.Split('=');
            
            string left = sides[0];
            string right = sides[1];
            
            string[] leftValues = GetValues(left);
            char[] leftOperators = GetOperators(left);
            string[] rightValues = GetValues(right);
            char[] rightOperators = GetOperators(right);

            if (left == "x" && right == "y") return x;

            if (right == "y")
            {
                float newX = x;

                for (int i = 0; i < leftOperators.Length; i++)
                {
                    char ch = leftOperators[i];

                    int charIndex = 0;
                    for (int j = 0; j <= i; j++) charIndex += leftValues[j].Length;
                    for (int j = 0; j < i; j++) charIndex += 1;

                    if (ch == '*')
                    {
                        string leftValue = leftValues[i];
                        string rightValue = leftValues[i + 1];
                        if (Operation.IsVariable(leftValue[0]))
                        {
                            newX *= int.Parse(rightValue);
                            string partA = equation.Substring(0, charIndex);
                            string partB = equation.Substring(charIndex + rightValue.Length + 1);
                            string newEquation = $"{partA}{partB}";
                            return SolveForY(newX, newEquation);
                        }
                        else if (Operation.IsVariable(rightValue[0]))
                        {
                            newX *= int.Parse(leftValue);
                            string partA = equation.Substring(0, charIndex - leftValue.Length);
                            string partB = equation.Substring(charIndex + 1);
                            string newEquation = $"{partA}{partB}";
                            return SolveForY(newX, newEquation);
                        }
                        else
                        {
                            int newValue = int.Parse(leftValue) * int.Parse(rightValue);
                            string partA = equation.Substring(0, charIndex - leftValue.Length);
                            string partB = equation.Substring(charIndex + rightValue.Length + 1);
                            string newEquation = $"{partA}{newValue}{partB}";
                            Debug.Log(newEquation);
                            return SolveForY(x, newEquation);
                        }
                    }
                }

                for (int i = 0; i < leftOperators.Length; i++)
                {
                    char ch = leftOperators[i];

                    int charIndex = 0;
                    for (int j = 0; j <= i; j++) charIndex += leftValues[j].Length;
                    for (int j = 0; j < i; j++) charIndex += 1;

                    if (ch == '+')
                    {
                        string leftValue = leftValues[i];
                        string rightValue = leftValues[i + 1];
                        if (Operation.IsVariable(leftValue[0]))
                        {
                            newX += int.Parse(rightValue);
                            string partA = equation.Substring(0, charIndex);
                            string partB = equation.Substring(charIndex + rightValue.Length + 1);
                            string newEquation = $"{partA}{partB}";
                            return SolveForY(newX, newEquation);
                        }
                        else if (Operation.IsVariable(rightValue[0]))
                        {
                            newX += int.Parse(leftValue);
                            string partA = equation.Substring(0, charIndex - leftValue.Length);
                            string partB = equation.Substring(charIndex + 1);
                            string newEquation = $"{partA}{partB}";
                            return SolveForY(newX, newEquation);
                        }
                        else
                        {
                            int newValue = int.Parse(leftValue) + int.Parse(rightValue);
                            string partA = equation.Substring(0, charIndex - leftValue.Length);
                            string partB = equation.Substring(charIndex + rightValue.Length + 1);
                            string newEquation = $"{partA}{newValue}{partB}";
                            Debug.Log(newEquation);
                            return SolveForY(x, newEquation);
                        }
                    }
                }
            }

            return -1;

            // for (int i = 0; i < leftOperators; i++)
            // {
            //     char ch = leftOperators[i];

            //     if (ch == '*')
            //     {

            //     }
            // }
        }

        private string[] GetValues(string str)
        {
            string newStr = str;

            for (int i = 0; i < str.Length; i++)
            {
                char ch = str[i];
                if (Operation.IsOperator(ch))
                {
                    newStr = Operation.ReplaceChar(newStr, i, '#');
                }
            }

            return newStr.Split('#');
        }

        private char[] GetOperators(string str)
        {
            List<char> operators = new List<char>();

            for (int i = 0; i < str.Length; i++)
            {
                char ch = str[i];
                if (Operation.IsOperator(ch)) operators.Add(ch);
            }

            return operators.ToArray();
        }
    }
}
