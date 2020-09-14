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
        
        private const int increments = 20;

        private GameObject[] points = new GameObject[increments];

        private void Start()
        {
            float xIncrement = (rightX - leftX) / increments;
            int index = 0;

            for (float x = leftX; x < rightX; x += xIncrement)
            {
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
            Debug.Log("clearing");
            foreach (GameObject point in points) point.SetActive(false);
        }

        private float SolveForY(float x, string equation)
        {
            float y = x;

            // bool leftX = false;
            // string[] sides = equation.Split('=');
            // if (sides[0].Contains('x')) leftX = true;

            // if (leftX)

            return y;
        }
    }
}
