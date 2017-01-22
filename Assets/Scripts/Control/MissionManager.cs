using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour {

    public VictoryPoint[] ports;

    VictoryPoint destination;
    VictoryPoint source;

    private int cashMoneys = 0;
    public int CashMoneys
    {
        get { return cashMoneys; }
    }

    public int cashPerDelivery = 100;

	void Start ()
    {
		for(int i=0; i < ports.Length; i++)
        {
            ports[i].ShipDocked += OnShipDocked;
        }

        SelectNewMissionPair();

        HUDController.Instance.SetGold(0);
	}

    void OnDestroy()
    {
        for (int i = 0; i < ports.Length; i++)
        {
            ports[i].ShipDocked -= OnShipDocked;
        }
    }

    private void OnShipDocked(VictoryPoint dock)
    {
        if(dock == source)
        {
            dock.DeactivateMarkers();
            HUDController.Instance.ShowCollect();
            source = null;
        }
        else if(dock == destination && source == null)
        {
            dock.DeactivateMarkers();
            cashMoneys += cashPerDelivery;
            HUDController.Instance.ShowDeliver();
            HUDController.Instance.SetGold(cashMoneys);
            SelectNewMissionPair();
        }
        else if(dock == destination && source != null)
        {
            HUDController.Instance.ShowShoutyInstructionPrompt();
        }
    }

    private void SelectNewMissionPair()
    {
        VictoryPoint lastDestination = destination;

        bool hasDestination = false;
        bool hasSource = false;

        bool isDone = false;

        while(!isDone)
        {
            VictoryPoint testPoint = ports[Random.Range(0, ports.Length)];

            if (!hasDestination)
            {
                if (testPoint != lastDestination)
                {
                    destination = testPoint;
                    hasDestination = true;
                }
            }

            if(hasDestination && !hasSource)
            {
                if(testPoint != lastDestination && testPoint != destination)
                {
                    source = testPoint;
                    hasSource = true;
                }
            }

            isDone = hasDestination && hasSource;
        }

        destination.ActivateMarker(true);
        source.ActivateMarker(false);

    }
}
