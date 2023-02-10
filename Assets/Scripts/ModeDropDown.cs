using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ModeDropDown : MonoBehaviour
{
    TMP_Dropdown m_Dropdown;
    // Start is called before the first frame update
    void Start()
    {
        //Fetch the Dropdown GameObject
        m_Dropdown = GetComponent<TMP_Dropdown>();
        //print(m_Dropdown);
        //Add listener for when the value of the Dropdown changes, to take action
        m_Dropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(m_Dropdown);
        });

        //Initial the Text to say the first value of the Dropdown
        //m_Text.text = "First Value : " + m_Dropdown.value;
    }

    //Output the new value of the Dropdown into Text
    void DropdownValueChanged(TMP_Dropdown change)
    {
        
        GlobalSetting.currentMesh = (MeshType)change.value;
        print(GlobalSetting.currentMesh);
    }
}
