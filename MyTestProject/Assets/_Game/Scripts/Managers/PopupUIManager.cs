using System;
using System.Collections.Generic;
using UnityEngine;

namespace MatchTwoCard
{
    public class PopupUIManager : Singleton<PopupUIManager>
    {
        [SerializeField] private bool restrictToSinglePopup;
        private List<PopupUIPanel> popupUIPanelList;
        private Stack<PopupUIPanel> popupUIPanelStack;

        public int ActivePopupUICount => popupUIPanelStack.Count;
        public PopupUIPanel ActivePopupUI => popupUIPanelStack.Peek();

        public static event Action onPopupUIUpdated;

        protected override void Awake(){
            base.Awake();
            LoadPopupUIPanels();
        }

        private void LoadPopupUIPanels(){
            if(transform.childCount == 0){
                Debug.Log("No PopupUI panels found");
                return;
            }
            popupUIPanelList = new List<PopupUIPanel>();
            popupUIPanelStack = new Stack<PopupUIPanel>();
            foreach (Transform child in transform){
                PopupUIPanel popupUIPanel = child.GetComponent<PopupUIPanel>();
                popupUIPanel.Initialialize();
                popupUIPanel.Hide();
                popupUIPanelList.Add(popupUIPanel);
            }
        }

        public void OpenUI<T>() where T : PopupUIPanel {
            PopupUIPanel popupUIPanel = popupUIPanelList.Find(x => x is T);
            if(popupUIPanel == null){
                Debug.Log(nameof(T) + " popupPanel not found");
                return;
            }
            if(ActivePopupUICount > 0 && popupUIPanelStack.Peek() == popupUIPanel){
                Debug.Log(nameof(T) + " popupPanel is already open ");
                return;
            }
            if(restrictToSinglePopup){
                HideActiveUI();
            }
            popupUIPanelStack.Push(popupUIPanel);
            popupUIPanel.Show();
            popupUIPanel.transform.SetSiblingIndex(transform.childCount-1);
            onPopupUIUpdated?.Invoke();
        }

        public void CloseUI(PopupUIPanel popupUIPanel){
            if(ActivePopupUICount == 0) return;
            if(popupUIPanelStack.Peek() != popupUIPanel) return;
            popupUIPanelStack.Pop().Hide();
            onPopupUIUpdated?.Invoke();
            if(restrictToSinglePopup){
                ShowHiddenUI();
            }
        }

        private void HideActiveUI(){
            if(ActivePopupUICount == 0) return;
            popupUIPanelStack.Peek().Hide();
        }

        private void ShowHiddenUI(){
            if(ActivePopupUICount == 0) return;
            popupUIPanelStack.Peek().Show();
            onPopupUIUpdated?.Invoke();
        }

        public void CloseActiveUI(){
            if(ActivePopupUICount == 0) return;
            popupUIPanelStack.Peek().CloseUI();
        }

        public T GetUI<T>() where T : PopupUIPanel {
            PopupUIPanel popupUIPanel = popupUIPanelList.Find(x => x is T);
            if(popupUIPanel == null){
                Debug.Log(nameof(T) + " popupPanel not found");
                return null;
            }
            return (T)popupUIPanel;
        }

        public void CloseAllPopupUI(){
            if(ActivePopupUICount > 0){
                CloseActiveUI();
                CloseAllPopupUI();
            }
        }
    }

    public abstract class PopupUIPanel : MonoBehaviour
    {
        public virtual void Hide() => gameObject.SetActive(false);
        public virtual void Show() => gameObject.SetActive(true);
        public virtual void Initialialize() {}
        public virtual void CloseUI() => PopupUIManager.Instance.CloseUI(this);
    }
}