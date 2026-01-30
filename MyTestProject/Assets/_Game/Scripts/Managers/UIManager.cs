using System;
using System.Collections.Generic;
using UnityEngine;

namespace MatchTwoCard
{
    public class UIManager : Singleton<UIManager>
    {
        private List<UIPanel> uIPanelList;
        private UIPanel activeUIPanel;
        public static event Action onUIUpdated;

        protected override void Awake(){
            base.Awake();
            LoadUIPanels();
        }

        public bool IsUIActive<T>()where T : UIPanel{
            return GetUI<T>() == activeUIPanel;
        }

        private void LoadUIPanels(){
            if(transform.childCount == 0){
                Debug.Log("No UI panels found");
                return;
            }
            uIPanelList = new List<UIPanel>();
            foreach (Transform child in transform){
                UIPanel uIPanel = child.GetComponent<UIPanel>();
                uIPanel.Initialialize();
                uIPanel.Hide();
                uIPanelList.Add(uIPanel);
            }
            activeUIPanel = uIPanelList[0];
            activeUIPanel.Show();
        }

        public void SwitchUI<T>() where T : UIPanel {
            UIPanel uIPanel = uIPanelList.Find(x => x is T);
            if(uIPanel == null){
                Debug.Log(nameof(T) + " panel not found");
                return;
            }
            activeUIPanel?.Hide();
            activeUIPanel = uIPanel;
            activeUIPanel.Show();
            onUIUpdated?.Invoke();
        }

        public T GetUI<T>() where T : UIPanel {
            UIPanel uIPanel = uIPanelList.Find(x => x is T);
            if(uIPanel == null){
                Debug.Log(nameof(T) + " Panel not found");
                return null;
            }
            return (T)uIPanel;
        }

        public void HideUI(){
            if(activeUIPanel == null) return;
            activeUIPanel.Hide();
            activeUIPanel = null;
        }

        public void PlayButtonClickAudio()
        {
            AudioManager.Instance.PlayAudio(AudioID.BUTTON);
        }
    }

    public abstract class UIPanel : MonoBehaviour
    {
        public virtual void Hide() => gameObject.SetActive(false);
        public virtual void Show() => gameObject.SetActive(true);
        public virtual void Initialialize() {}
    }
}