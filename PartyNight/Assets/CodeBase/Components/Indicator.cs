using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Components
{
    public class Indicator : MonoBehaviour
    {
        [SerializeField] private Image _filler;

        public void SetFillerValue(int value)
        {
            _filler.fillAmount = (float) value / 10;
        }
    }
}
