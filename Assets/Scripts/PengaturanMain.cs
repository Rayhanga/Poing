using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PengaturanMain : MonoBehaviour{
    void Start(){
        GameObject.Find("P1Label").GetComponent<Text>().text = StaticValues.P1NAME;
        GameObject.Find("P2Label").GetComponent<Text>().text = StaticValues.P2NAME;
        GameObject.Find("MaxScoreLabel").GetComponent<Text>().text = StaticValues.MAXSCORE + "";
        GameObject.Find("VolumeSlider").GetComponent<Slider>().value = StaticValues.VOLUME_BGM;
        GameObject.Find("BotToggle").GetComponent<Toggle>().isOn = StaticValues.P2ISBOT;
        GameObject.Find("DiffDropdown").GetComponent<Dropdown>().interactable = StaticValues.P2ISBOT;
        GameObject.Find("P2Input").GetComponent<InputField>().interactable = !StaticValues.P2ISBOT;

        Dropdown dropdown = GameObject.Find("DiffDropdown").GetComponent<Dropdown>();
        if (StaticValues.DIFFICULTY == 1.25f){
            dropdown.value = 0;
        }
        if (StaticValues.DIFFICULTY == 2.5f){
            dropdown.value = 1;
        }
        if (StaticValues.DIFFICULTY == 3.75f){
            dropdown.value = 2;
        }
        if (StaticValues.DIFFICULTY == 5f){
            dropdown.value = 3;
        }
    }

    public void ChangeBGMVolume(){
        float sliderValue = GameObject.Find("VolumeSlider").GetComponent<Slider>().value;

        StaticValues.VOLUME_BGM = sliderValue;
        GameObject.Find("BGM").GetComponent<AudioSource>().volume = sliderValue;
        if (sliderValue == 0){
            GameObject.Find("VolumePercent").GetComponent<Text>().text = "Muted";
        } else {
            GameObject.Find("VolumePercent").GetComponent<Text>().text = (sliderValue*100).ToString("#") + "%";
        }
    }

    public void ChangeName(string player){
        Text txt = GameObject.Find(player+"Text").GetComponent<Text>();
        if (!string.IsNullOrEmpty(txt.text)) {
            if (player == "P1"){
                StaticValues.P1NAME = txt.text;
            }
            if (player == "P2"){
                StaticValues.P2NAME = txt.text.Replace("BOT", "").Replace("(", "").Replace(")", "").Replace("N00B", "").Replace("EASY", "").Replace("MEDIUM", "").Replace("HARD", "");
            }
        }
    }

    public void ChangeMaxScore(string opt){
        Text txt = GameObject.Find("MaxScoreLabel").GetComponent<Text>();
        if (opt == "+"){
            StaticValues.MAXSCORE = StaticValues.MAXSCORE + 1;
        }
        if (StaticValues.MAXSCORE > 1 && opt == "-"){
            StaticValues.MAXSCORE = StaticValues.MAXSCORE - 1;
        }
        txt.text = StaticValues.MAXSCORE + "";
    }

    public void ToggleP2Bot(){
        Toggle toggle = GameObject.Find("BotToggle").GetComponent<Toggle>();
        Dropdown dropdown = GameObject.Find("DiffDropdown").GetComponent<Dropdown>();
        string diff = GameObject.Find("DropdownVal").GetComponent<Text>().text;
        GameObject.Find("P2Input").GetComponent<InputField>().interactable = !toggle.isOn;
        if (toggle.isOn){
            GameObject.Find("P2Label").GetComponent<Text>().text = "BOT (" + diff.ToUpper() + ")";
            StaticValues.P2NAME = "BOT (" + diff.ToUpper() + ")";
        } else {
            GameObject.Find("P2Label").GetComponent<Text>().text = "Merah";
            StaticValues.P2NAME = "Merah";
        }
        StaticValues.P2ISBOT = toggle.isOn;
        dropdown.interactable = toggle.isOn;
    }

    public void ChangeDiff(){
        string value = GameObject.Find("DropdownVal").GetComponent<Text>().text;
        if (value == "N00b"){
            StaticValues.DIFFICULTY = 1.25f;
        }
        if (value == "Easy"){
            StaticValues.DIFFICULTY = 2.5f;
        }
        if (value == "Medium"){
            StaticValues.DIFFICULTY = 3.75f;
        }
        if (value == "Hard"){
            StaticValues.DIFFICULTY = 5f;
        }
        StaticValues.P2NAME = "BOT (" + value.ToUpper() + ")";
    }
}
