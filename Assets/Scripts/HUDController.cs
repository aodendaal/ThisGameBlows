using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public static HUDController Instance;

    public Text goldPrompt;
    public GameObject messageBackground;
    public GameObject collectPrompt;
    public GameObject deliveryPrompt;
    public GameObject instructionPrompt;
    public GameObject deathPrompt;

    public MissionManager mManager;

    private float killTime;

    void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
	void Start ()
    {
        HidePrompts();
	}

    void HidePrompts()
    {
        messageBackground.SetActive(false);
        deliveryPrompt.SetActive(false);
        collectPrompt.SetActive(false);
        instructionPrompt.SetActive(false);
        deathPrompt.SetActive(false);
    }

    void Update()
    {
        if (killTime == 0)
            return;

        if (killTime <= Time.time)
        {
            HidePrompts();
            killTime = 0;
        }
    }
	
	public void SetGold(int gold)
    {
        goldPrompt.text = gold.ToString();
    }

    public void ShowCollect()
    {
        messageBackground.SetActive(true);
        collectPrompt.SetActive(true);
        killTime = Time.time + 2f;
    }

    public void ShowDeliver()
    {
        messageBackground.SetActive(true);
        deliveryPrompt.SetActive(true);
        killTime = Time.time + 2f;
    }

    public void ShowShoutyInstructionPrompt()
    {
        messageBackground.SetActive(true);
        instructionPrompt.SetActive(true);
        killTime = Time.time + 2f;
    }

    public void ShowDeathPrompt()
    {
        deathPrompt.GetComponent<Text>().text = "SHIP DESTROYED. IT HAD DONATED " + mManager.CashMoneys.ToString() + " GOLD IN TRIBUTE.";

        messageBackground.SetActive(true);
        deathPrompt.SetActive(true);
        killTime = Time.time + 2f;
    }
}
