using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchTwoCard
{
    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField] private float sfxVolume = 1;
        [SerializeField] private float musicVolume = 1;
        [SerializeField] private AudioSource audioSourceBG;
        [SerializeField] private AudioSource audioSourceSFX;
        [SerializeField] private List<AudioClip> audioClipSFXList;
        
        //private string baseKey = nameof(AudioManager);

        

        private void Start(){
            LoadData();
            UpdateVolume();
            UpdateBGAudio();
        }

        private void LoadData(){
            //foreach (var settingModel in settingModelList){
            //    settingModel.isDisabled = GameDataManager.Instance.GetValue(baseKey + settingModel.settingID.ToString(), settingModel.isDisabled);
            //}
        }

        private void UpdateVolume(){
            audioSourceBG.volume = musicVolume;
            audioSourceSFX.volume = sfxVolume;
        }

      

        

        public void UpdateBGAudio(){
            bool isDisabled = false;
            if(isDisabled)
                audioSourceBG.Stop();
            else
                audioSourceBG.Play();
        }

        public void PlayBGAudio() => audioSourceBG.Play();
        public void StopBGAudio() => audioSourceBG.Stop();

        public void PlayAudio(int index){            
            audioSourceSFX.PlayOneShot(audioClipSFXList[index]);
        }

        public void PlayAudio(AudioID audioID){            
            audioSourceSFX.PlayOneShot(audioClipSFXList[(int)audioID]);
        }
 
    }

    public enum AudioID {CardFlip,Coin,GameOver,Match,MissMatch,Success, BUTTON }
}
