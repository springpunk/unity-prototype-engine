using System;
using UnityEngine;

namespace Springpunk.PrototypeEngine.State
{
    /// <summary>
    /// Do not use Started!!!
    /// </summary>
    public enum GameAppState
    {
        Started = 0,
        NotPlaying = 1,
        Playing = 2,
        BeforePlay = 3,
        AfterPlay = 4,
        Paused = 5,
    }

    public class GameAppStateManager
    {
        private static GameAppStateManager instance;
        private static bool exists;

        private GameAppState state;
        public GameAppState State
        {
            get => state;
            set
            {
                // Skip same state
                if (state == value || value == GameAppState.Started) return;
                state = value;
                OnStateChangedOneShot();
                OnStateChanged();
            }
        }

        private event Action<GameAppState> stateChanged;
        private event Action<GameAppState> stateChangedOneShot;

        private event Action stateNotPlaying, statePlaying, stateBeforePlay, stateAfterPlay, statePaused;
        private event Action stateNotPlayingOneShot, statePlayingOneShot, stateBeforePlayOneShot, stateAfterPlayOneShot, statePausedOneShot;

        public static GameAppStateManager Instance
        {
            get
            {
                if (!exists)
                {
                    instance = new GameAppStateManager();
                    exists = true;
                }
                return instance;
            }
        }

        public GameAppStateManager()
        {
            state = GameAppState.Started;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Init()
        {
            instance= new GameAppStateManager();
            Debug.Log("Initialized GamAppStateManager instance");
        }

        /// <summary>
        /// Listen to all state changes
        /// </summary>
        /// <param name="listener">Delegate to invoke on state change</param>
        /// <param name="oneShot">Set to true if the delegate should only be invoked once</param>
        public void AddListener(Action<GameAppState> listener, bool oneShot = false) {
            if (oneShot) stateChangedOneShot += listener;
            else stateChanged += listener;
        }

        public void RemoveListener(Action<GameAppState> listener)
        {
            stateChanged -= listener;
        }

        /// <summary>
        /// Listen for a specific state change
        /// </summary>
        /// <param name="listener">Delegate to invoke on state change</param>
        /// <param name="state">State to listen for</param>
        /// <param name="oneShot">Set to true if the delegate should only be invoked once</param>
        public void AddStateListener(Action listener, GameAppState state, bool oneShot = false)
        {
            switch (state)
            {
                case GameAppState.NotPlaying:
                    if (oneShot) stateNotPlayingOneShot += listener;
                    else stateNotPlaying += listener;
                    break;
                case GameAppState.Playing:
                    if (oneShot) statePlayingOneShot += listener;
                    else statePlaying += listener;
                    break;
                case GameAppState.BeforePlay:
                    if (oneShot) stateBeforePlayOneShot += listener;
                    else stateBeforePlay += listener;
                    break;
                case GameAppState.AfterPlay:
                    if (oneShot) stateAfterPlayOneShot += listener;
                    else stateAfterPlay += listener;
                    break;
                case GameAppState.Paused:
                    if (oneShot) statePausedOneShot += listener;
                    else statePaused += listener;
                    break;
            }
        }

        public void RemoveStateListener(Action listener, GameAppState state)
        {
            switch (state)
            {
                case GameAppState.NotPlaying:
                    stateNotPlaying -= listener;
                    break;
                case GameAppState.Playing:
                    statePlaying -= listener;
                    break;
                case GameAppState.BeforePlay:
                    stateBeforePlay -= listener;
                    break;
                case GameAppState.AfterPlay:
                    stateAfterPlay -= listener;
                    break;
                case GameAppState.Paused:
                    statePaused -= listener;
                    break;
            }
        }

        private void OnStateChanged()
        {
            stateChanged?.Invoke(state);
            switch(state)
            {
                case GameAppState.NotPlaying:
                    stateNotPlaying?.Invoke();
                    break;
                case GameAppState.Playing:
                    statePlaying?.Invoke();
                    break;
                case GameAppState.BeforePlay:
                    stateBeforePlay?.Invoke();
                    break;
                case GameAppState.AfterPlay:
                    stateAfterPlay?.Invoke();
                    break;
                case GameAppState.Paused:
                    statePaused?.Invoke();
                    break;
            }
        }

        private void OnStateChangedOneShot()
        {
            stateChangedOneShot?.Invoke(state);
            stateChangedOneShot = null;
            switch (state)
            {
                case GameAppState.NotPlaying:
                    stateNotPlayingOneShot?.Invoke();
                    stateNotPlayingOneShot = null;
                    break;
                case GameAppState.Playing:
                    statePlayingOneShot?.Invoke();
                    statePlayingOneShot = null;
                    break;
                case GameAppState.BeforePlay:
                    stateBeforePlayOneShot?.Invoke();
                    stateBeforePlayOneShot = null;
                    break;
                case GameAppState.AfterPlay:
                    stateAfterPlayOneShot?.Invoke();
                    stateAfterPlayOneShot = null;
                    break;
                case GameAppState.Paused:
                    statePausedOneShot?.Invoke();
                    statePausedOneShot = null;
                    break;
            }
        }


    }
}