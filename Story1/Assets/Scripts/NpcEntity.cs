using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcEntity : MonoBehaviour
{
    [Header("NPC名字，需与Block名字一致")]
    public string othernpc;

    public Flowchart flowchart;

    public GameObject talkIndicatorPrefab;

    public Vector3 indicatorOffset = new Vector3(0, 3f, 0);

    private GameObject indicatorInstance;

    void Start()
    {
        if (flowchart == null)
        {
            flowchart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
        }

        if (talkIndicatorPrefab != null)
        {
            indicatorInstance = Instantiate(talkIndicatorPrefab, transform.position + indicatorOffset, Quaternion.Euler(-180f, 0f, 0f));
            indicatorInstance.SetActive(false);
        }
    }

    public void ShowIndicator(bool show)
    {
        if (indicatorInstance != null)
        {
            indicatorInstance.SetActive(show);
        }
    }

    public void Say()
    {
        if (flowchart != null && flowchart.HasBlock(othernpc))
        {
            flowchart.ExecuteBlock(othernpc);
        }
    }
}
