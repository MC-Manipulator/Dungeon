using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FastItemController : MonoBehaviour
{
    public Item item;
    public Image slotImage;

    public void Awake(){
        slotImage = gameObject.transform.Find("Image").GetComponent<Image>();
    }

    public void RemoveItem(){
        if(item != null){
            InventoryManager.Instance.Add(item);
            item = null;
            UpdateSlot();
        }
    }

    public void UpdateSlot(){
        if(item != null){
            slotImage.preserveAspect = true;//保持图片长宽比
            slotImage.sprite = item.icon;
            SetColorAlpha(1);
        }else{
            slotImage.sprite = null;
            SetColorAlpha(0.3f);
        }
    }

    public void SetColorAlpha(float alpha){
            Color newColor = new Color(255,255,255);
            newColor.a = alpha;
            slotImage.color = newColor;
    }
}
