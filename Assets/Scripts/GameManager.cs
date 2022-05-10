using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace algobitzTest
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public bool canMoveDropPosition = false;
        
        private void Awake()
        {
            Instance = this;
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(sceneName: "Algobitz");
        }
    }
}
