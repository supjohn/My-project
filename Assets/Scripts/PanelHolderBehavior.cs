using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public class PanelHolderBehavior : MonoBehaviour
{
    public GameObject outerPanel;

    public int width = 3, height = 2;

    public int heightOffset, widthOffset;

    public int defaultHeight, defaultWidth;

    public List<GameObject> panelList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        panelList = new List<GameObject>(width * height);
        Debug.Log(panelList.Capacity + " " + panelList.Count);
    }

    // Update is called once per frame
    void Update()
    {
        RenderPanels();
    }

    void RenderPanels()
    {
        if (panelList.Count > 0)
        {
            for (int i = 0; i < height; i++)
            {
                Debug.Log(i);
                for (int j = 0; j < width; j++)
                {
                    Debug.Log(j);
                    if (i * width + j > panelList.Count) return;
                    GameObject panel = panelList[i * width + j];
                    if (panel != null)
                    {
                        panel.transform.position = new Vector3(widthOffset * (2 * j + 1), heightOffset * (2 * i + 1), 0);
                    }
                }
            }
        }
    }
}
