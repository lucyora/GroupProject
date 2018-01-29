using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.IO;
using System.Text;

public class Language : MonoBehaviour
{
    public int m_iLanguage;

    enum lan
    {
        ENGLISH = 1,
        FRENCH,
        HINDI,
        KOREAN
    };

    // Use this for initialization
    void Start ()
    {
        m_iLanguage = (int)lan.ENGLISH;
	}
	
    void LanguageUpdate(int _language)
    {
        m_iLanguage = _language;
    }
}
