using UnityEngine;
using UnityEditor;
using Springpunk.PrototypeEngine.State;
using System;

namespace Springpunk.PrototypeEngine.Editor.State
{
    public class GameAppStateManagerMonitor : EditorWindow
    {
        private static GameAppState currentState = GameAppState.Started;

        [MenuItem("Tools/Springpunk/Prototype Engine/State Manager Monitor")]
        public static void CustomEditorWindow()
        {
            GetWindow<GameAppStateManagerMonitor>("Game App State Monitor");
        }

        private void Update()
        {
            if (Application.isPlaying)
            {
                GameAppState oldState = currentState;
                currentState =  GameAppStateManager.Instance.State;
                if(oldState != currentState)
                {
                    Repaint();
                }
            }
        }

        private void OnGUI()
        {
            if (Application.isPlaying)
            {
                GUILayout.Label($"Current State: {currentState}");
            }
            else
            {
                GUILayout.Label("The application is not running.");
            }
        }

    }
}