using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DyeFramework.Modules{
    /// <summary>
    /// 音频库
    /// </summary>
    [CreateAssetMenu(menuName = "Dye/AudioLibrary")]
    public class AudioLibrary : Common.ScriptableDictBase<string, List<AudioClip>>
    {
        public AudioClip GetClip(string id){
            List<AudioClip> clips = Get(id);
            if(clips!=null && clips.Count > 0){
                return clips[Random.Range(0, clips.Count)];
            }
            return null;
        }
    }
}
