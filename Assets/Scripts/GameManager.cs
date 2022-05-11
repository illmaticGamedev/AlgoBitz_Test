using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace algobitzTest
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public bool canMoveDropPosition = false;

        [SerializeField] private Text textHoldStatus;
        [SerializeField] private Text textBounceSpeedStatus;
        
        private void Awake()
        {
            Instance = this;
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(sceneName: "Algobitz");
        }

        public void UpdateBounceSpeedUI(string state)
        {
            textBounceSpeedStatus.text = state;
        }
        
        public void UpdateHoldStatusUI(bool state)
        {
            textHoldStatus.gameObject.SetActive(state);
        }
    }
}
