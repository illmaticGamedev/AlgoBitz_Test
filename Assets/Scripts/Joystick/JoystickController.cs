using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace algobitzTest
{
    public class JoystickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField] private Image joystickBG;
        [SerializeField] private Image controller;

        [HideInInspector] public Vector2 inputPos;
        
        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBG.rectTransform, eventData.position,
                eventData.pressEventCamera, out inputPos))
            {
                var bgSizeDelta = joystickBG.rectTransform.sizeDelta;
                inputPos.x = inputPos.x / (bgSizeDelta.x);
                inputPos.y = inputPos.y / (bgSizeDelta.y);
                
                var targetPos = Vector2.zero;
                var joystickSizeDelta = controller.rectTransform.sizeDelta;
                
                
                if(inputPos.magnitude > 1.0f) inputPos.Normalize();
                
                targetPos.x = inputPos.x * (joystickSizeDelta.x / 3);
                targetPos.y = -inputPos.y * (joystickSizeDelta.y / 3);
                
                controller.rectTransform.anchoredPosition = targetPos;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ResetController();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        void ResetController()
        {
            inputPos = Vector2.zero;
            controller.rectTransform.anchoredPosition = inputPos;
        }
    }
}
