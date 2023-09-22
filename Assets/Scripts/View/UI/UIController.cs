using System;
using UnityEngine;

namespace View
{
    public abstract class UIController : MonoBehaviour
    {
        void Awake()
        {
            ViewManager.I.onReceiveOutputMessageEvent.AddListener(OnOutputMessage);
        }

        void OnDisable()
        {
            ViewManager.I.onReceiveOutputMessageEvent.RemoveListener(OnOutputMessage);
        }

        protected virtual void OnOutputMessage(IOutputMessage outputMessage)
        {
            
        }
    }
}