using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    public int speed = 1, dawntime = 330, dusktime = 150, daytime = 20;
    public float time = 330f, blendValue; //time = 330f because we start with dawn
    private static float t = 0f, u = 0f;
    public Light Sun;
    public Color dawn, noon, dusk;
    public Material DayNightSkies;
    private float phase;
    public string what;

    // Start is called before the first frame update
    void Start()
    {  
        RenderSettings.skybox = DayNightSkies;      
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * speed;
        if (time > 360f) time = 0f;

        Sun.transform.rotation = Quaternion.Euler(new Vector3(time, 0, 0));

        if (time >= 150f && time < 200f) //dusk
        {
            what = "dusk";
            phase = dusktime;
            //360 because that is cycle of day/night, and 14% of it goes on dusk/dawn so it is enough time for lerp
            Sun.color = Color.Lerp(Sun.color, dusk, (time-phase)/(360f*0.14f));

            //before lerping skyboxes, reset t
            if (t < 1f) //when t = 1f lerp will return 0.75f and it will stay that way as long as it is dusktime
            {
                blendValue = Mathf.Lerp(0f, 0.75f, t); //min value is 0f because it is previously daytime, max value 0.75 for dusk
                t += 0.1f * Time.deltaTime;
            }
            DayNightSkies.SetFloat("_Blend", blendValue);

            //reset u during dusktime when it is not used
            if (u != 0f) u = 0f;
        }
        else if (time >= 200f && time < 330f) //night
        {
            what = "night";
            if (u < 1f)
            {
                blendValue = Mathf.Lerp(0.75f, 1f, u);
                u += 0.1f * Time.deltaTime;
            }
            DayNightSkies.SetFloat("_Blend", blendValue);

            //reset t between dawntime and dusktime when it is not used
            if (t != 0f) t = 0f;
        }
        else if (time >= 0f && time < 20f || time >= 330f && time <= 360f) //dawn
        {
            what = "dawn";
            phase = dawntime;
            Sun.color = Color.Lerp(Sun.color, dawn, (time-phase)/(360f*0.14f));
          
            if (t < 1f)
            {
                blendValue = Mathf.Lerp(1f, 0.25f, t); //min value is 1f because it is previously nightime, max value 0.25 for dawntime
                t += 0.1f * Time.deltaTime;
            }
            DayNightSkies.SetFloat("_Blend", blendValue);

            //reset u during dawntime when it is not used
            if (u != 0f) u = 0f;
        } 
        else //day
        {
            what = "day";
            phase = daytime;
            Sun.color = Color.Lerp(Sun.color, noon, (time-phase)/(360f*0.25f));

            if (u < 1f)
            {
                blendValue = Mathf.Lerp(0.25f, 0f, u);
                u += 0.1f * Time.deltaTime;
            }
            DayNightSkies.SetFloat("_Blend", blendValue);

            //reset t between dawntime and dusktime when it is not used
            if (t != 0f) t = 0f;
        }
        
    }
}