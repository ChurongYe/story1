using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDialogueDetector : MonoBehaviour
{
    public float detectRadius = 5f;
    public LayerMask npcLayer;
    public Transform dialoguepostion;
    private NpcEntity nearestNpc;
    public AudioSource audioSource;
    public AudioClip npc;
    public Image starth;
    int starthint;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        starth.enabled = false;
        starthint = 1;
    }
    void Update()
    {
        FindNearestNpc();

        if (nearestNpc != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                nearestNpc.Say();
            }
        }
    }

    void FindNearestNpc()
    {
        Collider[] hits = Physics.OverlapSphere(dialoguepostion.position, detectRadius, npcLayer);

        float minDistance = float.MaxValue;
        NpcEntity closest = null;

        foreach (var hit in hits)
        {
            NpcEntity npc = hit.GetComponent<NpcEntity>();
            if (npc != null)
            {
                //Debug.Log($"检测到NPC: {npc.name}");
                float dist = Vector3.Distance(dialoguepostion.position, npc.transform.position);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    closest = npc;
                }
            }
        }

        // 更新显示
        if (closest != nearestNpc)
        {
            if (nearestNpc != null)
            {
                nearestNpc.ShowIndicator(false);
                starth.enabled = false;
            }

            if (closest != null)
            {
                closest.ShowIndicator(true);
                audioSource.PlayOneShot(npc);
                starth.enabled = true;
            }

        }

        // 更新引用
        nearestNpc = closest;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(dialoguepostion.position, detectRadius);
    }
}