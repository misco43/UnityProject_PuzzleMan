using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoseTools
{
    namespace Utlis
    {
        public static class UtlisClass
        {

            public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = 5000)
            {
                if (color == null) color = Color.white;
                return CreateWorldText(parent, text, localPosition, fontSize, color.Value, textAnchor, textAlignment, sortingOrder);
            }

            public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
            {
                GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
                Transform transform = gameObject.transform;
                transform.SetParent(parent);
                transform.localPosition = localPosition;
                TextMesh textMesh = gameObject.GetComponent<TextMesh>();
                textMesh.text = text;
                textMesh.anchor = textAnchor;
                textMesh.alignment = textAlignment;
                textMesh.text = text;
                textMesh.fontSize = fontSize;
                textMesh.color = color;
                textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
                return textMesh;

            }


        }
    }  
}

