using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace OJ
{
    
    //x167 y144
    //w480 h528
    
    public class Game_5 : RiddlePanelBase, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        // Start is called before the first frame update
        private RectTransform _rectTransform;
        public GameObject gameObject1;
        public GameObject gameObject2;
        public static int UiIndex=1;
    void Start()
    {
        riddleId = 5;
        _rectTransform = GetComponent<RectTransform>();
        // gameObject2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && UiIndex ==2)
        { 
            gameObject2.SetActive(true);
            gameObject1.SetActive(false);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //GetComponent<CanvasGroup>().blocksRaycasts = false;
        //throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        print(eventData.pointerCurrentRaycast.gameObject.name);
        // throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print(eventData.pointerCurrentRaycast.gameObject.name);
        if (Game5_1.T1 == true && Game5_1.T2 == true)
        {
            if (UiIndex == 1)
            {
                 _rectTransform.anchoredPosition = new Vector3(171f, 144f, 0);
                 UiIndex++;
            }
            else
            {
                _rectTransform.anchoredPosition = new Vector3(-126f, -145f, 0);
                Debug.Log(123);
               // Result(true);
            }
           
        }
        //    //throw new System.NotImplementedException();171 144
    }
    }
}
