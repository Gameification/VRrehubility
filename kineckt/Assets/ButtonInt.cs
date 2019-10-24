using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInt : MonoBehaviour
{
    public Sprite onSelect;
    public Sprite onDeselect;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSelect()
    {
        GetComponent<Image>().sprite = onSelect;
        transform.localPosition = new Vector3(-250f, transform.localPosition.y, transform.localPosition.z);
    }

    public void OnDeselect()
    {
        GetComponent<Image>().sprite = onDeselect;
        transform.localPosition = new Vector3(-270f, transform.localPosition.y, transform.localPosition.z);
    }

    
}
