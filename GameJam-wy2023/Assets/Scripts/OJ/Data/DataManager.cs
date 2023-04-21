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
        public static List<ArchiveData> archiveDatas;//存档列表
        public static ArchiveData archive;//当前使用的存档
        public static GameData gameData = new GameData();//游戏数据
        public static bool saving;
        /// <summary>
        /// 保存游戏
        /// </summary>
        /// <param name="callback"></param>
        public static void GameSave(Action<bool> callback){
            if(archive == null){
                callback?.Invoke(true);
                return;
            }
            saving = true;
            string path = Application.persistentDataPath + "/Save/" + archive.archiveId + ".bytes";
            try{
                if(File.Exists(path)) File.Delete(path);
                using(FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite)){
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, gameData);
                }
                archive.duration += System.DateTime.Now - archive.lastTime;
                archive.lastTime = System.DateTime.Now;
                SaveArchiveDatas();
                saving = false;
                callback?.Invoke(true);
            }
            catch(Exception e){
                Debug.LogError(e);
                saving = false;
                callback?.Invoke(false);
            }
        }
        /// <summary>
        /// 加载存档
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static async void LoadArchive(int id, Action<bool> callback){
            saving = true;
            foreach(var archiveData in archiveDatas){
                if(archiveData.archiveId == id){
                    archive = archiveData;
                    break;
                }
            }
            if(archive == null){
                archive = CreateArchive(id);
            }
            string path = Application.persistentDataPath + "/Save/" + archive.archiveId + ".bytes";
            if(!File.Exists(path)){
                gameData = new GameData();
                saving = false;
                callback?.Invoke(true);
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
        /// <summary>
        /// 获取存档列表
        /// </summary>
        /// <returns></returns>
        public static List<ArchiveData> LoadArchiveDatas(){
            if(archiveDatas!=null){
                return archiveDatas;
            }
            if(!Directory.Exists(Application.persistentDataPath + "/Save")){
                Directory.CreateDirectory(Application.persistentDataPath + "/Save");
            }
            string path = Application.persistentDataPath + "/Save/archive.bytes";
            if(!File.Exists(path)){//存档文件丢失，或者第一次运行游戏
                using(FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite)){
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, archiveDatas = new List<ArchiveData>());
                }
                return archiveDatas;
            }
            //加载存档列表
            byte[] bytes = File.ReadAllBytes(path);
            using(MemoryStream ms = new MemoryStream(bytes)){
                BinaryFormatter bf = new BinaryFormatter();
                archiveDatas = bf.Deserialize(ms) as List<ArchiveData>;
            }
            return archiveDatas;
        }
        /// <summary>
        /// 创建存档
        /// </summary>
        public static ArchiveData CreateArchive(int id){
            LoadArchiveDatas();
            foreach(var archiveData in archiveDatas){
                if(archiveData.archiveId == id){
                    archiveDatas.Remove(archiveData);
                }
            }
            ArchiveData archive = new ArchiveData(){
                archiveId = id, percent = 0, 
                lastTime = System.DateTime.Now,
                createdTime = System.DateTime.Now,
                duration = new System.DateTime(0)
            };
            archiveDatas.Add(archive);
            SaveArchiveDatas();
            return archive;
        }
        /// <summary>
        /// 删除存档
        /// </summary>
        /// <param name="id"></param>
        public static void DeleteArchive(int id){
            foreach(var archiveData in archiveDatas){
                if(archiveData.archiveId == id){
                    archiveDatas.Remove(archiveData);
                    SaveArchiveDatas();
                }
            }
        }
        /// <summary>
        /// 保存存档列表
        /// </summary>
        public static void SaveArchiveDatas(){
            string path = Application.persistentDataPath + "/Save/archive.bytes";
            if(File.Exists(path)) File.Delete(path);
            using(FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite)){
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, archiveDatas);
            }
        }
        #endregion
    }
}
