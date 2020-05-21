using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TranslatorToJOMI : MonoBehaviour
{

    public UI_ServerDropdownJavaOMI m_targets;
    public Dropdown m_fromLanguage;
    public Dropdown m_toLanguage;
    public Dropdown m_translatorType;
    public InputField m_textToSend;
     string m_googleFormat = "https://translate.google.com/?hl={0}#view=home&op=translate&sl={0}&tl={1}&text={2}";
     string m_deeplFormat = "https://www.deepl.com/translator#{0}/{1}/{2}";

    public void SwitchLanguage() {
        int tmp = m_fromLanguage.value;
        m_fromLanguage.value  = m_toLanguage.value;
        m_toLanguage.value = tmp;
    }
    public void TranslateInputFieldText() {
        TranslateFromLangToLang(m_textToSend.text);
    }
    public void  TranslateFromLangToLang(string word) {


        foreach (var item in m_targets.GetJavaOMISelected())
        {
            if (m_translatorType.value == 0)
            {
                string from =GetPrefixGoogle( GetLanguageSelected(m_fromLanguage)),
                    to = GetPrefixGoogle(GetLanguageSelected(m_toLanguage));
                item.SendRawCommand("url:" +
                    string.Format(m_googleFormat, from, to, EncodeForURL(word)));
            }
            if (m_translatorType.value == 1)
            {
                string from = GetPrefixDeepl(GetLanguageSelected(m_fromLanguage)),
                    to = GetPrefixDeepl(GetLanguageSelected(m_toLanguage));
                item.SendRawCommand("url:" +
                    string.Format(m_deeplFormat, from, to, EncodeForURL(word)));
            }
        }
    }




    public string GetLanguageSelected(Dropdown drop) {
        return drop.options[drop.value].text;
    }

    public string EncodeForURL(string url) {
        url = url.Replace(" ", "%20");
        url = url.Replace("?", "%3F");
        url = url.Replace(",", "%2C");
        url = url.Replace(";", "%3B");
        url = url.Replace(":", "%3A");
        url = url.Replace("é", "%C3%A9");
        url = url.Replace("è", "%C3%A8");
        return url;
    }

    public void OnValidate()
    { List<string> ls= new List<string>();
        ls.Add("English");
        ls.Add("French");
        ls.Add("Italian");
        ls.Add("Spanish");
        ls.Add("Russian");
        ls.Add("Japanese");
        ls.Add("Chinese(Simplified)");
        ls.Add("Chinese(Traditional)");
        ls.Add("Portuguese");
        ls.Add("Dutch");
        ls.Add("German");
        m_fromLanguage.options.Clear();
        m_fromLanguage.AddOptions(ls);
        m_toLanguage.options.Clear();
        m_toLanguage.AddOptions(ls);
        List<string> translate = new List<string>();
        translate.Add("Google");
        translate.Add("Deepl");
        m_translatorType.options.Clear();
        m_translatorType.AddOptions(translate);
    }

    public string GetPrefixDeepl(string language)
    {
        if (language == "English") return "en";
        if (language == "French") return "fr";
        if (language == "Italian") return "it";
        if (language == "Spanish") return "es";
        if (language == "Russian") return "ru";
        if (language == "Japanese") return "ja";
        if (language == "Chinese(Simplified)") return "zh";
        if (language == "Portuguese") return "pt";
        if (language == "Dutch") return "nl";
        if (language == "German") return "de";
        if (language == "Albanian") return "sq";
        if (language == "Afrikaans") return "af";
        return "";
    }

        public string GetPrefixGoogle(string language)
    {
        if (language == "English") return "en";
        if (language == "French") return "fr";
        if (language == "Italian") return "it";
        if (language == "Spanish") return "es";
        if (language == "Russian") return "ru";
        if (language == "Japanese") return "ja";
        if (language == "Chinese(Simplified)") return "zh-CN";
        if (language == "Chinese(Traditional)") return "zh-TW";
        if (language == "Portuguese") return "pt";
        if (language == "Albanian") return "sq";
        if (language == "Afrikaans") return "af";
if (language == "Amharic") return "am";
if (language == "Arabic") return "ar";
if (language == "Armenian") return "hy";
if (language == "Azerbaijani") return "az";
if (language == "Basque") return "";
if (language == "Belarusian") return "";
if (language == "Bengali") return "";
if (language == "Bosnian") return "";
if (language == "Bulgarian") return "";
if (language == "Catalan") return "";
if (language == "Cebuano") return "";
if (language == "Chichewa") return "";
if (language == "Corsican") return "";
if (language == "Croatian") return "";
if (language == "Czech") return "";
if (language == "Danish") return "";
if (language == "Dutch") return "";
if (language == "Esperanto") return "";
if (language == "Estonian") return "";
if (language == "Filipino") return "";
if (language == "Finnish") return "";
if (language == "Frisian") return "";
if (language == "Galician") return "";
if (language == "Georgian") return "";
if (language == "German") return "";
if (language == "Greek") return "";
if (language == "Gujarati") return "";
if (language == "Haitian Creole") return "";
if (language == "Hausa") return "";
if (language == "Hawaiian") return "";
if (language == "Hebrew") return "";
if (language == "Hindi") return "";
if (language == "Hmong") return "";
if (language == "Hungarian") return "";
if (language == "Icelandic") return "";
if (language == "Igbo") return "";
if (language == "Indonesian") return "";
if (language == "Irish") return "";
if (language == "Javanese") return "";
if (language == "Kannada") return "";
if (language == "Kazakh") return "";
if (language == "Khmer") return "";
if (language == "Kinyarwanda") return "";
if (language == "Korean") return "";
if (language == "Kurdish(Kurmanji)") return "";
if (language == "Kyrgyz") return "";
if (language == "Lao") return "";
if (language == "Latin") return "";
if (language == "Latvian") return "";
if (language == "Lithuanian") return "";
if (language == "Luxembourgish") return "";
if (language == "Macedonian") return "";
if (language == "Malagasy") return "";
        if (language == "Malay") return "";
if (language == "Malayalam") return "";
if (language == "Maltese") return "";
if (language == "Maori") return "";
if (language == "Marathi") return "";
if (language == "Mongolian") return "";
if (language == "Myanmar") return "";
if (language == "Nepali") return "";
if (language == "Norwegian") return "";
if (language == "Odia") return "";
if (language == "Pashto") return "";
if (language == "Persian") return "";
if (language == "Polish") return "";
if (language == "Punjabi") return "";
if (language == "Romanian") return "";
if (language == "Samoan") return "";
if (language == "Scots Gaelic") return "";
if (language == "Serbian") return "";
if (language == "Sesotho") return "";
if (language == "Shona") return "";
if (language == "Sindhi") return "";
if (language == "Sinhala") return "";
if (language == "Slovak") return "";
if (language == "Slovenian") return "";
if (language == "Somali") return "";
        if (language == "Sundanese") return "";
if (language == "Swahili") return "";
if (language == "Swedish") return "";
if (language == "Tajik") return "";
if (language == "Tamil") return "";
if (language == "Tatar") return "";
if (language == "Telugu") return "";
if (language == "Thai") return "";
if (language == "Turkish") return "";
if (language == "Turkmen") return "";
if (language == "Ukrainian") return "";
if (language == "Urdu") return "";
if (language == "Uyghur") return "";
if (language == "Uzbek") return "";
if (language == "Vietnamese") return "";
if (language == "Welsh") return "";
if (language == "Xhosa") return "";
if (language == "Yiddish") return "";
if (language == "Yoruba") return "";
if (language == "Zulu") return "";
        return "";


    }

}
