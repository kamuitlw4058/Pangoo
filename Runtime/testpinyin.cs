using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo
{


    public class testpinyin : MonoBehaviour
    {
        [Button("Button")]
        void button()
        {
            string s = "yinpig测试是否可以正常转化拼音Eng";
            Debug.Log($"未转换{s}");
            Debug.Log($"测试中文转化：{s.ToPinyin()}");
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}