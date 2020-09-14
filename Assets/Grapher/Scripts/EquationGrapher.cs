using UnityEngine;

namespace Grapher
{
    public class EquationGrapher : MonoBehaviour
    {
        // [Header("References")]
        // [SerializeField] private GameObject pointPrefab = null;

        private GameObject[] points = new GameObject[100];

        public void GraphEquation(string equation)
        {
            Debug.Log("graphing equation " + equation);
        }
    }
}
