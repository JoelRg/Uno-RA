using UnityEngine;
using System.Collections.Generic;
using Vuforia;
using System.Linq;

public class changeColor : MonoBehaviour, IVirtualButtonEventHandler,ITrackableEventHandler {

    private GameObject btn_CC;
    private TrackableBehaviour mTrackableBehaviour;
    private GameObject[] colors=new GameObject[4];

    void Start() {
      // Buttons
      btn_CC=GameObject.Find("btn_CC");
      btn_CC.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);

      colors[0]=GameObject.Find("Ghost_Lv1");
      colors[1]=GameObject.Find("Bat_Level_1");
      colors[2]=GameObject.Find("Slime_Level_1");
      colors[3]=GameObject.Find("Rabbit_Level_1");

      cleanColors();

      Debug.Log("Ready Start!!");

    }

    public void OnButtonPressed(VirtualButtonAbstractBehaviour vb){
      Debug.Log("****************Button Down!*********************!");

      cleanColors();

      int iterator =Random.Range(0, 3);
      (colors[iterator].GetComponent("Float") as MonoBehaviour).enabled = true;

    }

    public void OnButtonReleased(VirtualButtonAbstractBehaviour vb){

    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus,TrackableBehaviour.Status newStatus){
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
        }
        else
        {
            OnTrackingLost();
        }
    }

    public void cleanColors(){
      (colors[0].GetComponent("Float") as MonoBehaviour).enabled = false;
      (colors[1].GetComponent("Float") as MonoBehaviour).enabled = false;
      (colors[2].GetComponent("Float") as MonoBehaviour).enabled = false;
      (colors[3].GetComponent("Float") as MonoBehaviour).enabled = false;
    }

    private void OnTrackingFound(){
      Debug.Log("**************** FOUND ************");
      cleanColors();
    }

    private void OnTrackingLost(){
      Debug.Log("*************** LOST *************");
      cleanColors();
    }
    
}
