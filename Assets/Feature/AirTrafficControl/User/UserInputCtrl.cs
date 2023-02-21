using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hsinpa.UserInput;

namespace Hsinpa.Game
{
    public class UserInputCtrl : MonoBehaviour
    {

        MainInputCtrl m_mainInputCtrl;

        void Start()
        {
            m_mainInputCtrl = new MainInputCtrl(Camera.main);
            m_mainInputCtrl.OnPressEvent += OnMousePress;
        }

        void Update()
        {

        }

        private void OnMousePress(Vector3 screenPosition, Vector3 worldPosition)
        {
            Debug.Log($"Screen {screenPosition.ToString()}, World  {worldPosition.ToString()}");
        }
    }
}