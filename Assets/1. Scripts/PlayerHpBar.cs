// using System.Collections;
// using TMPro;
// using UnityEngine;
// using UnityEngine.UI;

// // [ExecuteAlways]
// [RequireComponent(typeof(Image))]
// public class PlayerHpBar : MonoBehaviour
// {
//     private const string STEP = "_Steps";
//     private const string RATIO = "_HSRatio";
//     private const string WIDTH = "_Width";
//     private const string THICKNESS = "_Thickness";

//     private static readonly int floatSteps = Shader.PropertyToID(STEP);
//     private static readonly int floatRatio = Shader.PropertyToID(RATIO);
//     private static readonly int floatWidth = Shader.PropertyToID(WIDTH);
//     private static readonly int floatThickness = Shader.PropertyToID(THICKNESS);

//     public float Hp = 200f;
//     public float MaxHp = 200f;
//     public float Sp = 0f;

//     public float speed = 3f;
//     private float steps = 100f;
//     public float hpShieldRatio;
//     public float RectWidth = 100f;

//     [Range(0, 5f)]
//     public float Thickness = 2f;

//     public Image hp;
//     public Image damaged;
//     public Image sp;
//     public Image separator;

//     public TextMeshProUGUI text;

//     public TopDownCharacterController _player;

//     [ContextMenu("Create Material")]
//     private void CreateMaterial()
//     {
//         {
//             separator.material = new Material(Shader.Find("UI/Health Separator"));
//         }
//     }

//     private void Awake()
//     {
//         CreateMaterial();
//     }

//     private void Update()
//     {
//         MaxHp = _player.MaxHp;
//         Hp = _player.Hp;
//         Sp = _player.Sp;

//         if (Sp > 0)
//         {
//             text.SetText(Hp + "+ (" + Sp + ")" + " / " + MaxHp);
//         }
//         else
//             text.SetText(Hp + " / " + MaxHp);

//         if (MaxHp < Hp)
//         {
//             MaxHp = Hp;
//         }

//         float step;

//         // 쉴드가 존재 할 때
//         if (Sp > 0)
//         {
//             // 현재체력 + 쉴드 > 최대 체력
//             if (Hp + Sp > MaxHp)
//             {
//                 hpShieldRatio = Hp / (Hp + Sp);
//                 sp.fillAmount = 1f;
//                 step = (Hp) / steps;
//                 hp.fillAmount = Hp / (Hp + Sp);
//             }
//             else
//             {
//                 sp.fillAmount = (Hp + Sp) / MaxHp;
//                 hpShieldRatio = Hp / MaxHp;
//                 step = Hp / steps;

//                 hp.fillAmount = Hp / MaxHp;
//             }

//             damaged.fillAmount = hp.fillAmount;
//         }
//         else
//         {
//             sp.fillAmount = 0f;
//             step = MaxHp / steps;
//             hpShieldRatio = 1f;

//             hp.fillAmount = Hp / MaxHp;
//         }

//         damaged.fillAmount = Mathf.Lerp(damaged.fillAmount, hp.fillAmount, Time.deltaTime * speed);

//         separator.material.SetFloat(floatSteps, step);
//         separator.material.SetFloat(floatRatio, hpShieldRatio);
//         separator.material.SetFloat(floatWidth, RectWidth);
//         separator.material.SetFloat(floatThickness, Thickness);
//     }
// }
