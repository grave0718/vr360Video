using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class VideoFrame : MonoBehaviour
{
    VideoPlayer vp;
    // Start is called before the first frame update
    void Start()
    {
        //비디오 플레이어 컴포넌트를 받아오기
        vp = GetComponent<VideoPlayer>();

        //자동재생 막기, 해당 코드가 적용된 객체가 필요함, 그게 vp
        vp.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S)){
            vp.Stop();
        }
        if(Input.GetKeyDown("space")){
            if(vp.isPlaying){
                //영상 일시정지
                vp.Pause();
            }
            else{
                //영상 재생코드
                vp.Play();
            }

        }



    }
}
