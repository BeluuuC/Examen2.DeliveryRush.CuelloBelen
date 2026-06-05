using System.Collections.Generic;
using UnityEngine;

namespace DeliveryRushExam.UI
{
    public class ScorePopupPool : MonoBehaviour
    {
        [SerializeField] public ScorePopupView popupPrefab;  // <-- CAMBIADO a public
        [SerializeField] private int poolSize = 10;
        
        private Queue<ScorePopupView> pool = new Queue<ScorePopupView>();
        
        private void Awake()
        {
            for (int i = 0; i < poolSize; i++)
            {
                ScorePopupView popup = Instantiate(popupPrefab, transform);
                popup.gameObject.SetActive(false);
                pool.Enqueue(popup);
            }
        }
        
        public ScorePopupView GetPopup()
        {
            if (pool.Count > 0)
            {
                ScorePopupView popup = pool.Dequeue();
                popup.gameObject.SetActive(true);
                return popup;
            }
            else
            {
                ScorePopupView popup = Instantiate(popupPrefab, transform);
                return popup;
            }
        }
        
        public void ReturnPopup(ScorePopupView popup)
        {
            popup.gameObject.SetActive(false);
            pool.Enqueue(popup);
        }
    }
}