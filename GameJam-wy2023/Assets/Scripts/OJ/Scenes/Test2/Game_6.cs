using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace OJ
{
    
    //x167 y144
    //w480 h528
    
    public class Game_6 : RiddlePanelBase, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        // Start is called before the first frame update
        private RectTransform _rectTransform;
        public static int UiIndex=1;
        public static bool _b ;
        public static bool _h ;
        public static bool _i ;
        public static bool _u ;
        public static bool _y ;
    void Start()
    {
        riddleId = 5;
        _rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("进入"+other.transform.name);
        //bhiuy
        if (other.transform.name == "b" && transform.transform.name == "b")
        {
            _b = true;
        }
        if (other.transform.name == "h" && transform.transform.name == "h" )
        {
            _h = true;
        }
        if (other.transform.name == "i" && transform.transform.name == "i" )
        {
            _i = true;
        }
        if (other.transform.name == "u" && transform.transform.name == "u" )
        {
            _u = true;
        }
        if (other.transform.name == "y" && transform.transform.name == "y" )
        {
            _y = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("退出"+other.transform.name);
        if (other.transform.name == "b" && transform.transform.name == "b")
        {
            _b = false;
        }
        if (other.transform.name == "h" && transform.transform.name == "h" )
        {
            _h = false;
        }
        if (other.transform.name == "i" && transform.transform.name == "i" )
        {
            _i = false;
        }
        if (other.transform.name == "u" && transform.transform.name == "u" )
        {
            _u = false;
        }
        if (other.transform.name == "y" && transform.transform.name == "y" )
        {
            _y = false;
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
    }
    }
}
