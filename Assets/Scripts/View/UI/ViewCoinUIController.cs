using TMPro;
using UnityEngine;

namespace View
{
    public class ViewCoinUIController : UIController
    {
        [SerializeField] TMP_Text tmpText;

        void Start()
        {
            UpdateText();
        }

        protected override void OnOutputMessage(IOutputMessage outputMessage)
        {
            UpdateText();
        }
        
        void UpdateText()
        {
            tmpText.text = $"{DataStore.CoinNum}";
        }
    }
}