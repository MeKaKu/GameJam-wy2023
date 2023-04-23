using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json.Linq;
using DyeFramework.Modules;
using UnityEngine.UI;

using System.IO;
using Dye;

namespace OJ
{
    public class Bagdata : PanelBase
    {
        // Start is called before the first frame update
        //全部模板数据
        private List<Dye.Item> _list = new List<Item>();
        //背包数据
        private List<Dye.Item> _listBag = new List<Item>();
        
        private List<RawImage> _rawImages = new List<RawImage>();
        [Header("物品本体")]
        public List<Texture2D> texture2D = new List<Texture2D>();
        [Header("物品说明包含文字")]
        public List<Texture2D> texture2Ds = new List<Texture2D>();
        [Header("上面的物品")]
        public RawImage rawImageDown;
        [Header("下面的说明")]
        public RawImage rawImageUp;
        public int s;
        private String _objectName;
        private GameObject _bagDataRawImage;
        
        //对应关系
        private Dictionary<String, Texture2D> _texture2Dss = new Dictionary<string, Texture2D>();
        private int _index = 0;
        private bool _isOpen = true;
        private int _pageIndex = 0;
        //当前页
        private List<Dye.Item> _pageBagItem = new List<Item>();
        void Start()
        {
            Hide();
            _list = DataManager.configs.TbItem.DataList;
            PlayerPrefs.SetString("ObjectName",null);
            for (int i = 0; i < 16; i++)
            {
                
                _rawImages.Add(GameObject.Find("Image/Panel/"+(i+1)+"/RawImage").GetComponent<RawImage>());
            }

            _bagDataRawImage = GameObject.Find("BagData/RawImage");
            GameObject.Find("BagData/RawImage/Button").GetComponent<Button>().onClick.AddListener(() =>
            {
                _bagDataRawImage.SetActive(false);
            });;
            _bagDataRawImage.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.B) )
            {
                if (_isOpen)
                {
                     Show();
                     _isOpen = false;
                }
                else
                {
                  Hide();
                  _isOpen = true;   
                }
            }
            
            GetObjectData();
        }

        void GetObjectData()
        {
           String s= PlayerPrefs.GetString("ObjectName");
           PlayerPrefs.SetString("ObjectName",null);
           if (!String.IsNullOrWhiteSpace(s))
           {
               for (var i = 0; i < _list.Count(); i++)
               {
                   if (s == _list[i].Name) 
                   {  
                      // Debug.Log(2333+_list[i].Name);
                       _listBag.Add(_list[i]);
                      // Debug.Log(_listBag.Count);
                       //更新背包
                       PushBag();
                   }
               }
               _objectName = s;
           }
           
        }

        void PushBag()
        {
            Debug.Log(_pageIndex);
            for (int i = 0; i < _rawImages.Count(); i++)
            {
                _rawImages[i].texture = null;
                _rawImages[i].color = new Color(255, 255, 255, 0);
            }
            if (_listBag.Count<= 16)
            {
                for (int i = 0; i < _listBag.Count; i++)
                {
                    for (int j = 0; j < texture2D.Count; j++)
                    {
                        if (_listBag[i].Name == texture2D[j].name)
                        {
                            _rawImages[i].texture = texture2D[j];
                            _rawImages[i].color = new Color(255, 255, 255, 255);
                        }
                    }
                }
            }
            else
            {
                int startIndex = _pageIndex * 16;
                int endIndex = Math.Min(startIndex + 16, _listBag.Count); 
                // 提取指定页码的数据
                List<Dye.Item> listBagPage = _listBag.GetRange(startIndex, endIndex - startIndex);
                for (int i = 0; i < listBagPage.Count; i++)
                {
                    for (int j = 0; j < texture2D.Count; j++)
                    {
                        if (listBagPage[i].Name == texture2D[j].name)
                        {
                            _rawImages[i].texture = texture2D[j];
                            _rawImages[i].color = new Color(255, 255, 255, 255);
                        }
                    }
                }
            }
            
        }
        protected override void OnClick(string name)
        {
            Debug.Log(name);
            if (name == "Image/Button Right")
            {
                _pageIndex++;
                PushBag();
                return;
            }
            if (name == "Image/Button Left")
            {
                _pageIndex--;
                PushBag();
                return;
            }
            for (int i = 1; i <= _rawImages.Count; i++)
            {
                if (name == "Image/Panel/"+i)
                {
                    // Debug.Log("Image/Panel/"+i);
                    // Debug.Log(_rawImages[i-1].texture.name);
                   
                    for (int j = 0; j < texture2Ds.Count; j++)
                    {
                        //文件01
                        if (texture2Ds[j].name == _rawImages[i-1].texture.name)
                        {
                            if (_rawImages[i-1].texture.name == "文件01" || _rawImages[i-1].texture.name == "文件02"  || 
                                _rawImages[i-1].texture.name == "文件03" || _rawImages[i-1].texture.name == "石板")
                            {
                                _bagDataRawImage.SetActive(true);
                                GameObject.Find("BagData/RawImage/RawImage").GetComponent<RawImage>().texture =  texture2Ds[j];
                            }
                            else
                            {
                                 Debug.Log(_rawImages[i-1].texture.name);
                                 rawImageDown.texture = texture2Ds[j];
                                 rawImageDown.color = new Color(255, 255, 255, 255);
                                 rawImageUp.texture = _rawImages[i-1].texture;
                                 rawImageUp.color = new Color(255, 255, 255, 255);
                            }
                           
                        }
                    }
                    // rawImageDown.texture = 
                }
            }
            //base.OnClick(name);
        }

        public override void Hide()
        {
            base.Hide();
        }
    }
}
