using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class HotspotManager : MonoBehaviour
{
    public static HotspotManager instance = null;
    public List<Hotspot> hotspots = new List<Hotspot>();
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text content;
    [SerializeField] RectTransform panel;
    [SerializeField] Button close;
    [SerializeField] Image infoImage;
    Sequence s;

    private void Awake()
    {
        if(!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        s = DOTween.Sequence();
        //gameObject.SetActive(false);
    }

    private void Start()
    {
        foreach(Transform t in transform)
        {
            Hotspot h = t.GetComponent<Hotspot>();
            if(h)
            {
                hotspots.Add(h);
            }
        }
        close.onClick.AddListener(Hide);
    }


    public void TurnOn()
    {
        foreach(Hotspot h in hotspots)
        {
            h.gameObject.SetActive(true);
        }
    }

    public void ShowInfo(string _title, string _info, Sprite _image = null)
    {
        title.text = _title;
        content.text = _info;
        if(_image)
        {
            infoImage.sprite = _image;
            infoImage.enabled = true;
            infoImage.preserveAspect = true;
        }
        else
        {
            infoImage.enabled = false;
        }
        Show();
    }

    public void SetUp(List<Transform> _transforms)
    {
        for (int i = 0; i < hotspots.Count; i++)
        {
            hotspots[i].anchor = _transforms[i];
        }
    }

    public void Show()
    {
        s?.Kill(true);
        s.Append(panel.DOAnchorPosX(panel.sizeDelta.x,0.3f));
    }

    public void Hide()
    {
        s?.Kill(true);
        s.Append(panel.DOAnchorPosX(0, 0.3f));
    }
}
