using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    //beacuse score will be managed in multiple scenes we have a static score manager
    public static GameObject popUpPrefab;
    [SerializeField] GameObject prePOP;
    public static int score; //player score
    [SerializeField] Text scoreText; //player score Text
    [SerializeField] Text shadeText; //shading score Text
    AudioSource aud;
    //timer stuff
    [SerializeField] bool timerRunning;
    [SerializeField] Text timer;
    [SerializeField] Text timerShade;
    // Use this for initialization
    public static Camera Acamera;
    void Start()
    {
        Acamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        popUpPrefab = prePOP;
        //aud = GameObject.Find("Player1").GetComponent<AudioSource>();
        //TODO
        //check if in leaderboard mode
        //leaderboard shuff
    }

    public static void updateScore(int value)
    {
        score += value;
        
      GameObject pop = Instantiate(popUpPrefab, GameObject.Find("Player").transform.position, Quaternion.identity);
       pop.GetComponent<ScorePopUp>().setVal(value);
    }

    public static void updateScore(int value, GameObject refrence)
    {
        //don't count if not in the camera space
        //make sure not in camera space
        // Get the top-left corner of the camera's viewport
        Vector2 topLeft = Acamera.ViewportToWorldPoint(new Vector3(0, 1, Acamera.nearClipPlane));

        // Get the bottom-right corner of the camera's viewport
        Vector2 bottomRight = Acamera.ViewportToWorldPoint(new Vector3(1, 0, Acamera.nearClipPlane));

        // Get the position of the game object in world space
        Vector2 goPos = refrence.transform.position;

        Bounds cameraBounds = new Bounds(bottomRight, topLeft);
        //check if this gameobject is contained with in those two points
        bool cameraContains = (goPos.x > topLeft.x && goPos.y < topLeft.y && goPos.x < bottomRight.x && goPos.y > bottomRight.y);
       if(cameraContains)
        {

            score += value;

            GameObject pop = Instantiate(popUpPrefab, refrence.transform.position, Quaternion.identity);
            pop.GetComponent<ScorePopUp>().setVal(value);
        }
    }
    // Update is called once per frame
    void Update()
    {
      
       scoreText.text = "Score : " + score;
        shadeText.text = scoreText.text;
    }
    IEnumerator timeSet(float time)
    {
        while (time > 0)
        {
            timerRunning = true;
            time -= Time.deltaTime;
            float minutes = Mathf.FloorToInt(time / 60);
            float seconds = Mathf.FloorToInt(time % 60);
            string minutesString = minutes.ToString("D2");
            string secondsString = seconds.ToString("D2");
            timer.text = minutesString + ":" + secondsString;
            yield return new WaitForEndOfFrame();
           
        }
       // yield return new WaitForSeconds(3f);
      //  SceneManager.LoadScene(4);

    }
}