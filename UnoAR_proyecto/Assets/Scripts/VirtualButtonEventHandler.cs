using UnityEngine;
using System.Collections.Generic;
using Vuforia;
using System.Linq;

public class VirtualButtonEventHandler : MonoBehaviour, IVirtualButtonEventHandler,ITrackableEventHandler {

    private GameObject vbButtonObject;
    private GameObject[] models;
    private int location=0;
    private IEnumerator<UnityEngine.WaitForSeconds> coroutine;
    public string tag;
    public string button;
    private TrackableBehaviour mTrackableBehaviour;

    void Start() {
      // Button
      vbButtonObject=GameObject.Find(button);
      vbButtonObject.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);

      // Models
      models = GameObject.FindGameObjectsWithTag(tag);

      // SetAtive false models
      for (int i=0 ; i<models.Length ; i++ ){
        models[i].SetActive(false);
      }

      // Change models
      coroutine=changeModelForTime();
      StartCoroutine(coroutine);

      mTrackableBehaviour = GetComponent<TrackableBehaviour>();
      if (mTrackableBehaviour)
      {
          mTrackableBehaviour.RegisterTrackableEventHandler(this);
      }

    }

    IEnumerator<UnityEngine.WaitForSeconds> changeModelForTime() {
        Debug.Log("time change");
        for(;;) {
          changeModel();
          yield return new WaitForSeconds(.1f);
      }
    }

    public void changeModel(){
      models[location].SetActive(false);
      try{
        models[location+1].SetActive(true);
        location=location+1;
      }catch (System.IndexOutOfRangeException e){
        models[0].SetActive(true);
        location=0;
      }

    }

    public void OnButtonPressed(VirtualButtonAbstractBehaviour vb){
      Debug.Log("Button Down!!");
      StopCoroutine(coroutine);
    }

    public void OnButtonReleased(VirtualButtonAbstractBehaviour vb){

    }

    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
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

    private void OnTrackingFound(){
      Debug.Log("**************** FOUND ************");
      StartCoroutine(coroutine);
    }

    private void OnTrackingLost(){
      Debug.Log("*************** LOST *************");
    }
}
