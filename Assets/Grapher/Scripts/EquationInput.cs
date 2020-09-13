using TMPro;
using UnityEngine;

namespace Grapher
{
    [RequireComponent(typeof(TMP_InputField))]
    public class EquationInput : MonoBehaviour
    {
        private TMP_InputField input;

        private void Start()
        {
            input = GetComponent<TMP_InputField>();
        }

        public void UpdateEquation()
        {

        }
    }
}
