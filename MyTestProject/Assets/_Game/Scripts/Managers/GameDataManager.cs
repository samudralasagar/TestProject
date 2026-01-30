using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System;
using Unity.VisualScripting;
 


namespace MatchTwoCard
{
    public class GameDataManager : Singleton<GameDataManager>
    {
        private string dataFilePath;
        private Dictionary<string, object> dataDictionary;
        private string jsonOutput;

        public static event Action onDataUpdated;
        //public static event Action onLocalDataUpdated;

        protected override void Awake(){
            base.Awake();
            dataFilePath = Path.Combine(Application.persistentDataPath, "gameData.json");
            dataDictionary = new Dictionary<string, object>();
            LoadJsonDataFromFile();
        }

        public T GetValue<T>(string key, T defaultValue){
            if(dataDictionary.ContainsKey(key)){
                return dataDictionary[key].ConvertTo<T>();
            }
            return defaultValue;
        }

        public void SetValue<T>(string key, T value, bool uploadToCloud = true){
            if(dataDictionary.ContainsKey(key)){
                dataDictionary[key] = value;
            }
            else{
                dataDictionary.Add(key, value);
            }
            SaveJsonDataToFile(uploadToCloud);
        }

        public void SaveJsonDataToFile(bool uploadToCloud = true){
            try{
                jsonOutput = JsonConvert.SerializeObject(dataDictionary, Formatting.None);
                File.WriteAllText(dataFilePath, jsonOutput);
                //Debug.Log("Data saved to file.");
                if(uploadToCloud == false) return;
                
            }
            catch (Exception e){
                Debug.LogError($"Error saving data to file: {e.Message}");
            }
        }

        public void LoadJsonDataFromFile(){
            try{
                if (File.Exists(dataFilePath)){
                    string json = File.ReadAllText(dataFilePath);
                    var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                    //Debug.Log("Data loaded from file.\n" + json);
                    if(string.IsNullOrEmpty(json)) return;
                    dataDictionary = dictionary;
                    //onLocalDataUpdated?.Invoke();
                    return;
                }
                else{
                    Debug.LogWarning("No save file found.");
                    dataDictionary = new Dictionary<string, object>();
                }
            }
            catch (Exception e){
                Debug.LogError($"Error loading data from file: {e.Message}");
                return;
            }
        }

        

        

        protected override void OnApplicationQuit() {
            base.OnApplicationQuit();
        } 

        private void OnApplicationPause(bool pauseStatus){
            if (pauseStatus){
                //Debug.Log("App went to background. Save data here!");
                 
            }
        }
        
        

        [ContextMenu("DeleteLocalData")]
        public void DeleteLocalData(){
            string dataFilePath = Path.Combine(Application.persistentDataPath, "gameData.json");
            File.Delete(dataFilePath);
        }

        [ContextMenu("PrintLocalData")]
        public void PrintLocalData(){
            LoadJsonDataFromFile();
            string data = "PrintLocalData:\n";
            foreach (var item in dataDictionary){
                data += item.Key + " : " + item.Value + "\n";
            }
            print(data);
        }

       
    }
}
