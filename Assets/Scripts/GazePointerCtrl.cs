using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GazePointerCtrl : MonoBehaviour
{

    public Transform uiCanvas;
    public Image gazeImg;

    Vector3 defaultScale;
    public float uiScaleVal = 1f;

    public Video360Play vp360;


    bool isHitObj;
    GameObject prevHitObj;
    GameObject curHitObj;
    float curGazeTime = 0;
    public float gazeChargeTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        defaultScale = uiCanvas.localScale;
        curGazeTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //카메라기준 전방방향의 좌표를 구한다.
        Vector3 dir = transform.TransformPoint(Vector3.forward);
        
        //카메라 기준 전방의 레이를 설정한다.
        Ray ray = new Ray(transform.position, dir);
        RaycastHit hitInfo;

        //레이에 부딪힌 경우, 거리값을 이용해 uiCanvas 의 크기 조절
        if(Physics.Raycast(ray, out hitInfo)){
            uiCanvas.localScale = defaultScale * uiScaleVal * hitInfo.distance;
            uiCanvas.position = transform.forward * hitInfo.distance;


            if(hitInfo.transform.tag == "GazeObj"){
                isHitObj = true;
            
            }
            curHitObj = hitInfo.transform.gameObject;
        }

        //아무것도 부딪히지 않으면 기본 스케일 값으로 uiCanvs 의 크기 조절
        else{
            uiCanvas.localScale = defaultScale * uiScaleVal;
            uiCanvas.position = transform.position + dir;

        }

        //uiCanvas 가 항상 카메라를 바라보도록 설정
       uiCanvas.forward = transform.forward * 1;
    
    if(isHitObj){
        if(curHitObj == prevHitObj){
            curGazeTime += Time.deltaTime;
        }
        else{
            prevHitObj = curHitObj;
        }

        HitObjChecker(curHitObj,true);
    }
    else{

        if(prevHitObj != null){
            HitObjChecker(prevHitObj, false);
            prevHitObj = null;
        }
        curGazeTime = 0;

    }

    curGazeTime = Mathf.Clamp(curGazeTime, 0, gazeChargeTime);
    gazeImg.fillAmount = curGazeTime / gazeChargeTime;

    isHitObj = false;
    curHitObj = null;


    }

    void HitObjChecker(GameObject hitObj, bool isActive){
        if(hitObj.GetComponent<VideoPlayer>()){
            if(isActive){
                hitObj.GetComponent<VideoFrame>().CheckVideoFrame(true);
            }
            else{
                hitObj.GetComponent<VideoFrame>().CheckVideoFrame(false);
            }
        }

        if(gazeImg.fillAmount >=1){
            vp360.SetVideoPlay(hitObj.transform.GetSiblingIndex());
        }
    }


}
