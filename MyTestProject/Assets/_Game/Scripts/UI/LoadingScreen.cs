using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MatchTwoCard
{
    public class LoadingScreen : UIPanel
    {
        


        private IEnumerator Start()
        {
            yield return new WaitForSeconds(2f);
            UIManager.Instance.SwitchUI<MainMenu>();
        }

    }
}
