/*************************************************************************************************
* Copyright 2022 Theai, Inc. (DBA Inworld)
*
* Use of this source code is governed by the Inworld.ai Software Development Kit License Agreement
* that can be found in the LICENSE.md file or at https://www.inworld.ai/sdk-license
*************************************************************************************************/
using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Inworld.Sample.UI
{
    /// <summary>
    ///     This class is used for the Record Button in the global chat panel.
    /// </summary>
    public class RecordButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        bool m_IsCapturingBeforeReset = false;
        InworldCharacter m_currentCharacter;
        Animator m_Animator;
        int m_ListeningAnimationID = 24;
        void OnEnable()
        {
            m_IsCapturingBeforeReset = InworldController.IsCapturing;
            InworldController.IsCapturing = false;  
        }
        void OnDisable()
        {
            InworldController.IsCapturing = m_IsCapturingBeforeReset;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            InworldController.Instance.StartRecording(false);
            SetListeningAnimation(true); 
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            InworldController.Instance.PushAudio();
            SetListeningAnimation(false); 
        }
        void SetListeningAnimation(bool isListening)
        {
            if(!InworldController.Instance)
                return;

            m_currentCharacter = InworldController.Instance.CurrentCharacter;
            m_Animator = m_currentCharacter.GetComponent<Animator>();

            int anim = 0;
            if(isListening)
                anim = m_ListeningAnimationID;
            
            UnityEngine.Debug.Log("SetListeningAnimation: " + anim);
            m_Animator.SetInteger(Animator.StringToHash("Gesture"), anim);
        }
    }
}
