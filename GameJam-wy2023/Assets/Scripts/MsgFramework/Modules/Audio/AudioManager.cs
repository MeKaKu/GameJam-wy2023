using System.Collections;
using System.Collections.Generic;
using DyeFramework.Common;
using UnityEngine;

namespace DyeFramework.Modules
{
    public class AudioManager : Base.ManagerBase<AudioManager>
    {
        public AudioManager(){
            Init();
        }

        #region Volume
        [SerializeField]int maxVolumeInt = 10;
        public float mainVolumePercent {get; private set;}//主音量大小百分比
        public float musicVolumePercent {get; private set;}//音乐音量大小百分比
        public float soundVolumePercent {get; private set;}//音效音量大小百分比
        public int mainVolumeInt => (int)(mainVolumePercent * maxVolumeInt);
        public int musicVolumeInt => (int)(musicVolumePercent * maxVolumeInt);
        public int soundVolumeInt => (int)(soundVolumePercent * maxVolumeInt);

        public enum AudioType{
            Main = 0,
            Music = 1,
            Sound = 2
        }
        private void InitVolume() {
            mainVolumePercent = PlayerPrefs.GetFloat("MainVolume", 1);
            musicVolumePercent = PlayerPrefs.GetFloat("MusicVolume", 1);
            soundVolumePercent = PlayerPrefs.GetFloat("SoundVolume", 1);
        }
        public static void ChangeVolume(AudioType type, int volume){
            Instance._ChangeVolume(type, volume);
        }
        public void _ChangeVolume(AudioType type, int volume){
            switch(type){
                case AudioType.Main:
                    mainVolumePercent = 1f*volume/maxVolumeInt;
                    PlayerPrefs.SetFloat("MainVolume", mainVolumePercent);
                    break;
                case AudioType.Music:
                    musicVolumePercent = 1f*volume/maxVolumeInt;
                    PlayerPrefs.SetFloat("MusicVolume", musicVolumePercent);
                    break;
                case AudioType.Sound:
                    soundVolumePercent = 1f*volume/maxVolumeInt;
                    PlayerPrefs.SetFloat("SoundVolume", soundVolumePercent);
                    break;
            }
        }
        #endregion

        #region AudioSource
        AudioSource audioSource2D;//2D音源
        List<AudioSource> audioSources3D;//3D音源列表
        Pool<AudioSource> pool;//3D音源对象池
        AudioSource[] audioSourcesMusic;//音乐音源，2个平滑切换音乐
        int curMusicIndex;//当前音乐音源索引
        IEnumerator musicCrossFade;//音乐淡入淡出携程
        AudioConfig config;//
        void Init() {
            Transform transform = GameManager.Mono.transform;
            //2D
            audioSource2D = new GameObject("AudioSourceGO").AddComponent<AudioSource>();
            audioSource2D.playOnAwake = false;
            audioSource2D.loop = false;
            audioSource2D.transform.parent = transform;
            audioSource2D.transform.localPosition = Vector3.zero;
            audioSources3D = new List<AudioSource>();
            //3D
            Transform audioSource3DParent = new GameObject("AudioSource3DGOs").transform;
            audioSource3DParent.parent = transform;
            audioSource3DParent.localPosition = Vector3.zero;
            pool = new Pool<AudioSource>(audioSource2D, audioSource3DParent);
            //Music
            audioSourcesMusic = new AudioSource[2];
            for(int i=0;i<2;i++){
                audioSourcesMusic[i] = new GameObject("AudioSourceMusic_"+i).AddComponent<AudioSource>();
                audioSourcesMusic[i].transform.parent = transform;
            }
            //Volume
            InitVolume();
            //Config
            config = ResManager.Load<AudioConfig>("Audio/config");
            if(config!=null){
                foreach(var lib in config.articles){
                    lib.value.Init();
                }
                config.Init();
            }

            GameManager.Mono.StartCoroutine(IEUpdate());
        }
        
        IEnumerator IEUpdate(){
            var waitTime = new WaitForSeconds(1f);
            while(true){
                yield return waitTime;
                for(int i=audioSources3D.Count-1;i>=0;i--){
                    if(!audioSources3D[i].isPlaying){
                        pool.Destroy(audioSources3D[i]);
                    }
                    audioSources3D.RemoveAt(i);
                }
            }
        }
        
        #region Sound 2D
        /// <summary>
        /// 播放2D音效
        /// </summary>
        /// <param name="resId">音频id</param>
        public static void PlaySound2D(string resId){
            Instance.PlaySound2D(GetAudioClip(resId));
        }
        /// <summary>
        /// 播放2D音效
        /// </summary>
        /// <param name="clipId">音频id</param>
        /// <param name="libId">音频库id</param>
        public static void PlaySound2D(string libId, string clipId){
            Instance.PlaySound2D(GetAudioClip(libId, clipId));
        }
        /// <summary>
        /// 播放2D音效
        /// </summary>
        /// <param name="clip">音频</param>
        public void PlaySound2D(AudioClip clip){
            if(clip != null)
                audioSource2D.PlayOneShot(clip, soundVolumePercent * mainVolumePercent);
        }
        #endregion
        
        #region Sound 3D
        /// <summary>
        /// 播放3D音效
        /// </summary>
        /// <param name="clipId">音频id</param>
        /// <param name="libId">音频库id</param>
        /// <param name="pos">物体位置</param>
        public static void PlaySound(string libId, string clipId, Transform pos = null){
            Instance.PlaySound(GetAudioClip(libId, clipId), pos);
        }
        /// <summary>
        /// 播放3D音效
        /// </summary>
        /// <param name="resId">音频id</param>
        /// <param name="pos">物体位置</param>
        public static void PlaySound(string resId, Transform pos = null){
            Instance.PlaySound(GetAudioClip(resId), pos);
        }
        /// <summary>
        /// 播放3D音效
        /// </summary>
        /// <param name="clipId">音频id</param>
        /// <param name="libId">音频库id</param>
        /// <param name="pos">位置</param>
        public static void PlaySound(string libId, string clipId, Vector3 pos){
            Instance.PlaySound(GetAudioClip(libId, clipId), pos);
        }
        /// <summary>
        /// 播放3D音效
        /// </summary>
        /// <param name="resId">音频id</param>
        /// <param name="pos">位置</param>
        public static void PlaySound(string resId, Vector3 pos){
            Instance.PlaySound(GetAudioClip(resId), pos);
        }
        /// <summary>
        /// 播放3D音效
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="pos"></param>
        public void PlaySound(AudioClip clip, Transform pos = null){
            if(pos == null){
                pos = GameManager.Mono.transform;
            }
            if(clip != null){
                AudioSource audioSource = pool.Create((a)=>{
                    a.transform.parent = pos;
                    a.transform.localPosition = Vector3.zero;
                });
                audioSource.clip = clip;
                audioSource.volume = soundVolumePercent * mainVolumePercent;
                audioSource.Play();
                audioSources3D.Add(audioSource);
            }
        }
        /// <summary>
        /// 播放3D音效
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="pos"></param>
        public void PlaySound(AudioClip clip, Vector3 pos){
            if(clip != null){
                AudioSource.PlayClipAtPoint(clip, pos, Instance.soundVolumePercent * Instance.mainVolumePercent);
            }
        }
        #endregion

        #region Music
        /// <summary>
        /// 播放音乐
        /// </summary>
        /// <param name="clipId">音频id</param>
        /// <param name="libId">音频库id</param>
        public static void PlayMusic(string libId, string clipId){
            Instance.PlayMusic(GetAudioClip(clipId, libId));
        }
        /// <summary>
        /// 播放音乐
        /// </summary>
        /// <param name="resId">音频id</param>
        public static void PlayMusic(string resId){
            Instance.PlayMusic(GetAudioClip(resId));
        }
        /// <summary>
        /// 播放音乐
        /// </summary>
        /// <param name="clip"></param>
        public void PlayMusic(AudioClip clip){
            if(musicCrossFade!=null){
                GameManager.Mono.StopCoroutine(musicCrossFade);
            }
            musicCrossFade = MusicCrossFade(clip);
            GameManager.Mono.StartCoroutine(musicCrossFade);
        }
        /// <summary>
        /// 停止背景音乐
        /// </summary>
        public static void StopMusic(){
            Instance.PlayMusic((AudioClip)null);
        }
        //背景音乐淡入淡出
        IEnumerator MusicCrossFade(AudioClip clip, float duration = 1f){
            AudioSource from = audioSourcesMusic[curMusicIndex];
            AudioSource to = audioSourcesMusic[curMusicIndex^1];
            while(to.volume > 0){
                to.volume -= 2 * Time.deltaTime;
                yield return null;
            }
            curMusicIndex ^= 1;
            float fromOriginVolume = from.volume;
            float toOriginVolume = 0;
            float percent = 0;
            to.clip = clip;
            if(clip!=null) to.Play();
            while(percent < 1){
                percent += Time.deltaTime / duration;
                yield return null;
                from.volume = Mathf.Lerp(fromOriginVolume, 0, percent);
                to.volume = Mathf.Lerp(toOriginVolume, musicVolumePercent * mainVolumePercent, percent);
            }
            from.Stop();
            musicCrossFade = null;
        }
        #endregion
        
        static AudioClip GetAudioClip(string libId, string clipId){
            AudioLibrary library = null;
            library = Instance.config.Get(libId);
            if(library == null){
                Debug.LogWarning($"库{libId}不存在");
                return GetAudioClip(libId + "/" + clipId);
            }
            AudioClip clip = library.GetClip(clipId);
            if(clip == null){
                Debug.LogWarning($"音频{clipId}不存在");
            }
            return clip;
        }
        static AudioClip GetAudioClip(string resId){
            return ResManager.Load<AudioClip>("Audio/" + resId);
        }
        #endregion
    }
}
