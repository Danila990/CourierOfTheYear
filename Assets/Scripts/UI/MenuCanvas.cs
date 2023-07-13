using TMPro;
using UnityEngine;

public class MenuCanvas : MonoBehaviour
{
    const string KEY_RECORD = "record";

    [SerializeField]
    private TMP_Text _recordValue;

    private void Start()
    {
        _recordValue.text = SaveLoaderRecord.Load(KEY_RECORD).ToString();
    }
}
