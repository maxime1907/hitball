using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SliderMenu : MonoBehaviour {

	//Canvas Settings--------------------------------------------------------------------------------------------
	public Canvas YourCanvas;
	public int SlidesInView;
    public Text amount;
    public int choosenSkin;
    //-----------------------------------------------------------------------------------------------------------

    //ScrollBar Settings-----------------------------------------------------------------------------------------
    public bool Enable_Show_ScrollBar = false;
	public Scrollbar HorizontalScrollBar;
	public bool ShowButtons=false;
	public Sprite ButtonSprite;
	private float k=0;
	private bool ButtonClicked=false;
	//-----------------------------------------------------------------------------------------------------------

	//Content Settings-------------------------------------------------------------------------------------------
	public Sprite Background;
	public RectTransform ScrollContent;
	public List<GameObject> LevelThumbnails;
	//-----------------------------------------------------------------------------------------------------------

	//Slides Settings--------------------------------------------------------------------------------------------
	public string SlidesNamePrefix="Button 0";
	public float Element_Width;
	public float Element_Height;
	public float Element_Margin;
	public float Element_Scale;
	//-----------------------------------------------------------------------------------------------------------

	//Slides Settings--------------------------------------------------------------------------------------------
	public float Transition_In;
	public float Transition_Out;
	public Color PreviousSlideColor=new Color(1,1,1,255);
	public Color ActiveSlideColor=new Color(1,1,1,255);
	public Color NextSlideColor=new Color(1,1,1,255);
	//-----------------------------------------------------------------------------------------------------------

	//Slides Settings--------------------------------------------------------------------------------------------
	public bool DesktopPlatform;
	//-----------------------------------------------------------------------------------------------------------

	//Other Variables--------------------------------------------------------------------------------------------
	private float n;
    private string tmpName;
    private float ScrollSteps;
	//-----------------------------------------------------------------------------------------------------------

	void Start () {
		//Canvas Settings----------------------------------------------------------------------------
								//Only In Pro Version
		//-------------------------------------------------------------------------------------------



		//Set Background Of Content------------------------------------------------------------------
		GameObject.Find ("Background").GetComponent<Image>().sprite=Background;
		//-------------------------------------------------------------------------------------------



		//Next And Previous Button-------------------------------------------------------------------
								//Only In Pro Version
		//-------------------------------------------------------------------------------------------



		//Auto Find Slides And Auto Set Size And Position Of Slides
		for (int b=0; b<LevelThumbnails.Count; b++) {
			LevelThumbnails[b].GetComponent<RectTransform>().sizeDelta=new Vector2(Element_Width,Element_Height);
			LevelThumbnails[b].GetComponent<RectTransform>().localPosition=new Vector3((2*b+3)*Element_Width/2+(2*b+3)*Element_Margin,0,10);
		}
		//-------------------------------------------------------------------------------------------



		//Set Size Of ScrollContent (Auto Set)
		ScrollContent.GetComponent<RectTransform>().sizeDelta=new Vector2((LevelThumbnails.Count+2)*(Element_Width+2*Element_Margin),Element_Height);
		//-------------------------------------------------------------------------------------------



		//Calculate ScrollSteps Value----------------------------------------------------------------
		n = LevelThumbnails.Count - 1;
		ScrollSteps = 1 / n;
		//-------------------------------------------------------------------------------------------
	}

	void Update () {
		//Show Or Hide ScrollBar---------------------------------------------------------------------
		if (Enable_Show_ScrollBar == true) {
			HorizontalScrollBar.gameObject.SetActive(true);
		}else{
			HorizontalScrollBar.gameObject.SetActive(false);
		}
		//-------------------------------------------------------------------------------------------



		//Slides Magnet------------------------------------------------------------------------------
		if (DesktopPlatform == false) {
			if (Input.touchCount == 0) {
				for (int s=0; s<LevelThumbnails.Count; s++) {
					if (HorizontalScrollBar.GetComponent<Scrollbar> ().value > (ScrollSteps / 2) + (s - 1) * (ScrollSteps) && HorizontalScrollBar.GetComponent<Scrollbar> ().value <= Mathf.Clamp (ScrollSteps / 2 + s * ScrollSteps, 0, 1)) {
						HorizontalScrollBar.GetComponent<Scrollbar> ().value = Mathf.Lerp (HorizontalScrollBar.GetComponent<Scrollbar> ().value, s * ScrollSteps, 0.1f);
					}
				}
			}
		} 
		
        //When Use Next And Previous Buttons
		for (int s=0; s<LevelThumbnails.Count; s++) {
			if (k > (ScrollSteps / 2) + (s - 1) * (ScrollSteps) && k <= Mathf.Clamp (ScrollSteps / 2 + s * ScrollSteps, 0, 1)) {
				k = Mathf.Lerp (k, s * ScrollSteps, 0.1f);
            }
		}
		//-------------------------------------------------------------------------------------------

		//Slides Scale, Slides Transition And Slides Color Transition-------------------------------
		for (int s=0; s<LevelThumbnails.Count; s++) {
			for (int t=0; t<LevelThumbnails.Count; t++) {
				if (HorizontalScrollBar.GetComponent<Scrollbar> ().value > (ScrollSteps / 2) + (s - 1) * (ScrollSteps) && HorizontalScrollBar.GetComponent<Scrollbar> ().value <= Mathf.Clamp (ScrollSteps / 2 + s * ScrollSteps, 0, 1)) {
					if(t!=s){
						LevelThumbnails [t].transform.localScale = Vector2.Lerp (LevelThumbnails [t].transform.localScale, new Vector2 (1, 1), Transition_Out);
					}
					if(t==s){
						//In Pro Version Change Color Of Active Slide
						LevelThumbnails [s].transform.localScale = Vector2.Lerp (LevelThumbnails [s].transform.localScale, new Vector2 (Element_Scale, Element_Scale), Transition_In);
						LevelThumbnails [s].gameObject.transform.SetAsLastSibling();

                        if (s < 10)
                            tmpName = "0" + s.ToString();
                        else
                            tmpName = s.ToString();

                        if (PlayerPrefs.GetInt(tmpName) != 1)
                            amount.text = (52 * (s + 1)).ToString();
                        else
                            amount.text = "OWNED";
                        choosenSkin = s;
                    }
				}
			}
		}

		//Next Or Previous Button Is Clicked---------------------------------------------------------
		if (ButtonClicked == true) {
			HorizontalScrollBar.GetComponent<Scrollbar> ().value=Mathf.Lerp(HorizontalScrollBar.GetComponent<Scrollbar> ().value,k,0.1f);
        }
		//-------------------------------------------------------------------------------------------
	}

	public void NextButton(){
		k = Mathf.Clamp (k+ScrollSteps,0,1);
		ButtonClicked = true;
	}

	public void PreviousButton(){
		k = Mathf.Clamp (k-ScrollSteps,0,1);
		ButtonClicked = true;
	}

	public void ContentDrag(){
		ButtonClicked = false;
		k=Mathf.Clamp (HorizontalScrollBar.GetComponent<Scrollbar> ().value,0,1);

	}
}
