using UnityEngine;

namespace OJ{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    public class PostEffectsBase : MonoBehaviour
    {
        bool enable;
        protected virtual void Start() {
            enable = Check();
        }

        protected virtual bool Check(){
            return true;
        }

        protected virtual Material CheckAndGetMaterial(Shader shader, Material material){
            if(shader == null || !shader.isSupported){
                return null;
            }
            if(material && material.shader == shader){
                return material;
            }
            else{
                material = new Material(shader);
                material.hideFlags = HideFlags.DontSave;
                if(material) return material;
                else return null;
            }
        }
    }
}
