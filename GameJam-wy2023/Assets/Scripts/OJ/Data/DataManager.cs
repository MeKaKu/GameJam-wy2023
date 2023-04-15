using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dye;
using Bright.Serialization;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace OJ
{
    public class DataManager
    {
        static DataManager(){
            configs = new Configs(LoadByteBuf);
        }
        #region 配置
        public static Configs configs;

        static ByteBuf LoadByteBuf(string file) {
            return new ByteBuf(File.ReadAllBytes($"{Application.streamingAssetsPath}/{file}.bytes"));
        }
        #endregion

        #region 游戏数据
        public static GameData gameData;
        public static int archiveId;
        public static bool saving;
        public static void GameSave(Action<bool> callback){
            saving = true;
            string path = Application.streamingAssetsPath + "/save_" + archiveId + ".bytes";
            try{
                if(File.Exists(path)) File.Delete(path);
                using(FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite)){
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, gameData);
                }
                saving = false;
                callback?.Invoke(true);
            }
            catch{
                saving = false;
                callback?.Invoke(false);
            }
        }
        public static async void LoadArchive(Action<bool> callback){
            saving = true;
            string path = Application.streamingAssetsPath + "/save_" + archiveId + ".bytes";
            if(!File.Exists(path)){
                saving = false;
                callback?.Invoke(false);
                return;
            }
            try{
                byte[] bytes = await File.ReadAllBytesAsync(path);
                using(MemoryStream ms = new MemoryStream(bytes)){
                    BinaryFormatter bf = new BinaryFormatter();
                    gameData = bf.Deserialize(ms) as GameData;
                }
                saving = false;
                callback?.Invoke(true);
            }
            catch{
                saving = false;
                callback?.Invoke(false);
            }
        }
        #endregion
    }
}
