using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//
public class PanelHolderBehavior : MonoBehaviour
{
    public GameObject outerPanel;

    public int holderWidth = 3, holderHeight = 2;

    public int heightOffset = 40, widthOffset = 100;


    public List<GameObject> panelList = new List<GameObject>();

    public int verticalBorder, horizontalBorder, verticalSpacing, horizontalSpacing;
    public Vector3 panelSize;

    // Start is called before the first frame update
    void Start()
    {
        panelList = new List<GameObject>(holderWidth * holderHeight);
        //Debug.Log(panelList.Capacity + " " + panelList.Count);
    }

    // Update is called once per frame
    void Update()
    {
        RenderPanels(); //Get rid of this; we should re-render panels only when something changes
    }

    public int addPanel(GameObject panel)
    {
        //Insert a panel at the lowest-indexed available slot
        //If there is no slot available, return -1; otherwise, return the index of the new slot
        return -1;
    }

    public bool InsertPanel(GameObject panel, int index)
    {
        //Attempts to add a panel to a specific index
        //Returns True if successful; otherwise, returns false
        return false;
    }

    void RenderPanels()
    {
        if (panelList.Count > 0)
        {
            for (int i = 0; i < holderHeight; i++)
            {
                //Debug.Log(i);
                for (int j = 0; j < holderWidth; j++)
                {
                    //Debug.Log(j);
                    if (i * holderWidth + j + 1 > panelList.Count) return;
                    //Debug.Log(i * holderWidth + j + " vs " + panelList.Count);
                    GameObject panel = panelList[i * holderWidth + j];
                    if (panel != null)
                    {
                        int xCoord = (horizontalBorder / 2) + horizontalSpacing * (2*j+1)/2 + (int)panelSize.x * (2 * j + 1)/2;
                        int yCoord = (verticalBorder / 2) + verticalSpacing * (2 * i + 1) / 2 + (int)panelSize.y * (2 * i + 1) / 2;
                        panel.transform.position = new Vector3(xCoord, yCoord, 0);
                        panel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, panelSize.x);
                        panel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, panelSize.y);
                    }
                }
            }
        }
    }
}