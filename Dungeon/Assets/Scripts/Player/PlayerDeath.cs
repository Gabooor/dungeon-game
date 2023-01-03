using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public float cooldown;
    public Camera cam;

    public enum TimeState
    {
        full,
        eight,
        six,
        four,
        two,
        stopped
    }

    public TimeState ts = TimeState.full;
    // Start is called before the first frame update
    void Start()
    {
        cooldown = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(!Player.isAlive){
            //Debug.Log(ts);
            cooldown -= 1f * Time.deltaTime;
            if(ts != TimeState.stopped){
                cam.orthographicSize -= 2.5f * Time.deltaTime;
            }
            if(cooldown < 0){
                switch(ts){
                    case TimeState.full:
                        {
                            Time.timeScale = 0.8f;
                            ts = TimeState.eight;
                            cooldown = 0.1f;
                            break;
                        }
                    case TimeState.eight:
                        {
                            Time.timeScale = 0.6f;
                            ts = TimeState.six;
                            cooldown = 0.1f;
                            break;
                        }
                    case TimeState.six:
                        {
                            Time.timeScale = 0.4f;
                            ts = TimeState.four;
                            cooldown = 0.1f;
                            break;
                        }
                    case TimeState.four:
                        {
                            Time.timeScale = 0.2f;
                            ts = TimeState.two;
                            cooldown = 0.1f;
                            break;
                        }
                    case TimeState.two:
                        {
                            Time.timeScale = 0.1f;
                            ts = TimeState.stopped;
                            cooldown = 0.1f;
                            break;
                        }
                }
            }
        }
    }
}
