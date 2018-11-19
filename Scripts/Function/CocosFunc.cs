using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

struct CocosInfo
{
	public Vector2 dis_pos;
	public Vector2 end_pos;

	public GameObject obj;

	public float time;
	public float save_time;

	public float SpeedX;
	public float SpeedY;

	public bool check1;
}

struct FadeInfo
{
	public float time;
	public float save_time;
	public float rate;
	public Image img;
}

struct FadeInfoTxt
{
	public float time;
	public float save_time;
	public float rate;
	public Text txt;
}

struct FadeInfoSp
{
	public float time;
	public float save_time;
	public float rate;
	public SpriteRenderer spr;
}

struct ImageFillInfo
{
	public float time;
	public float save_time;
	public float speed;

	public Image img;
}

struct ScaleInfo
{
	public float time;
	public float save_time;
	public Vector2 value;
	public Vector2 speed;
	public GameObject obj;
}

struct TypeInfo
{
	public float time;
	public string value;
	public Text txt;
}
public class CocosFunc : MonoBehaviour
{
	// Inspector ////////////////////////////////////////////////////////////////

	// Function /////////////////////////////////////////////////////////////////
	private void Awake()
	{
		CoinInfo.Instance.m_CocosFunc = this;

	}

	// time = 이동시간, MoveValue = 이동값, check = 체크여부, obj = GameObject
	public void MoveTo(float  time, Vector2 MoveValue, bool check,ref GameObject obj)
	{
		Vector2 distance = MoveValue;
		CocosInfo value;
		value.obj = obj;
		value.dis_pos = distance;
		value.end_pos = (Vector2)obj.transform.position + MoveValue;
		value.time = time;
		value.check1 = check;
		value.save_time = 0.0f;
		value.SpeedX = value.dis_pos.x / value.time;
		value.SpeedY = value.dis_pos.y / value.time;
		StartCoroutine("MoveToC", value);
	}
	public void MoveToR(float time, Vector2 MoveValue, bool check, ref GameObject obj)
	{
		Vector2 distance = MoveValue;
		CocosInfo value;
		value.obj = obj;
		value.dis_pos = distance;
		value.end_pos = (Vector2)obj.transform.position + MoveValue;
		value.time = time;
		value.check1 = check;
		value.save_time = 0.0f;
		value.SpeedX = value.dis_pos.x / value.time;
		value.SpeedY = value.dis_pos.y / value.time;
		StartCoroutine("MoveToRC", value);
	}
	IEnumerator MoveToC(CocosInfo info)
	{
		info.save_time += Time.deltaTime;
		if (info.obj != null) // 대상이 Destroy 될경우 예외처리
		{
			Vector3 pos = info.obj.transform.position;
			if (info.save_time < info.time)
			{
				info.obj.transform.position = new Vector3(pos.x + Time.deltaTime * info.SpeedX, pos.y + Time.deltaTime * info.SpeedY, pos.z);
				yield return null;
				StartCoroutine("MoveToC", info);
			}
			else if (info.check1)
			{
				info.obj.transform.position = new Vector3(info.end_pos.x, info.end_pos.y, pos.z);
			}
		}
	}

	IEnumerator MoveToRC(CocosInfo info)
	{
		info.save_time += Time.unscaledDeltaTime;
		if (info.obj != null) // 대상이 Destroy 될경우 예외처리
		{
			Vector3 pos = info.obj.transform.position;
			if (info.save_time < info.time)
			{
				info.obj.transform.position = new Vector3(pos.x + Time.unscaledDeltaTime * info.SpeedX, pos.y + Time.unscaledDeltaTime * info.SpeedY, pos.z);
				yield return null;
				StartCoroutine("MoveToRC", info);
			}
			else if (info.check1)
			{
				info.obj.transform.position = new Vector3(info.end_pos.x, info.end_pos.y, pos.z);
			}
		}
	}
	public void MoveToX(float time, Vector2 MoveValue, bool check, ref GameObject obj)
	{
		Vector2 distance = MoveValue;
		CocosInfo value;
		value.obj = obj;
		value.dis_pos = distance;
		value.end_pos = (Vector2)obj.transform.position + MoveValue;
		value.time = time;
		value.check1 = check;
		value.save_time = 0.0f;
		value.SpeedX = value.dis_pos.x / value.time;
		value.SpeedY = 0;
		StartCoroutine("MoveToC", value);
	}

	public void MoveByX(float time, Vector2 MoveValue, bool check, ref GameObject obj)
	{
		Vector2 objpos = obj.transform.position;
		Vector2 distance = MoveValue - objpos;
		CocosInfo value;
		value.obj = obj;
		value.dis_pos = distance;
		value.end_pos = MoveValue;
		value.time = time;
		value.check1 = check;
		value.save_time = 0.0f;
		value.SpeedX = value.dis_pos.x / value.time;
		value.SpeedY = 0;
		StartCoroutine("MoveToC", value);
	}


	// 이미지 FadeIn
	//public void ImageFadeIn(float time, float rate, ref Image img)
	//{
	//	FadeInfo info;
	//	info.time = time;
	//	info.img = img;
	//	info.save_time = 0.0f;
	//	info.rate = 1.0f;
	//	StartCoroutine("ImageFadeInC", info);
	//}
	public void ImageFadeIn(float time, ref Image img)
	{
		FadeInfo info;
		info.time = time;
		info.img = img;
		info.save_time = 0.0f;
		info.rate = 1.0f;
		StartCoroutine("ImageFadeInC", info);
	}

	IEnumerator ImageFadeInC(FadeInfo info)
	{
		info.save_time += Time.unscaledDeltaTime;
		if(info.img != null)
		{
			if (info.save_time < info.time)
			{
				float rate = (info.save_time / info.time) * info.rate;
				info.img.color = new Color(info.img.color.r, info.img.color.g, info.img.color.b, rate);
				yield return null;
				StartCoroutine("ImageFadeInC", info);
			}
			else
				info.img.color = new Color(info.img.color.r, info.img.color.g, info.img.color.b, info.rate);
		}
	}

	public void ImageFadeIn(float time, float rate, ref Image img)
	{
		FadeInfo info;
		info.time = time;
		info.img = img;
		info.save_time = 0.0f;
		info.rate = rate;
		if (rate != img.color.a)
			StartCoroutine("ImageFadeInC2", info);
	}

	IEnumerator ImageFadeInC2(FadeInfo info)
	{
		info.save_time += Time.unscaledDeltaTime;
		if (info.img != null)
		{
			if (info.save_time < info.time)
			{
				float rate = info.save_time / info.time;
				info.img.color = new Color(info.img.color.r, info.img.color.g, info.img.color.b, info.rate*rate);
				yield return null;
				StartCoroutine("ImageFadeInC2", info);
			}
			else
				info.img.color = new Color(info.img.color.r, info.img.color.g, info.img.color.b, info.rate);
		}
	}

	public void ImageFadeOut(float time, ref Image img)
	{
		FadeInfo info;
		info.time = time;
		info.img = img;
		info.save_time = 0.0f;
		info.rate = img.color.a;
		StartCoroutine("ImageFadeOutC", info);
	}
	IEnumerator ImageFadeOutC(FadeInfo info)
	{
		info.save_time += Time.unscaledDeltaTime;
		if (info.img != null)
		{
			if (info.save_time < info.time)
			{
				float rate = (info.save_time / info.time) * info.rate;
				info.img.color = new Color(info.img.color.r, info.img.color.g, info.img.color.b, info.rate - rate);
				yield return null;
				StartCoroutine("ImageFadeOutC", info);
			}
			else
				info.img.color = new Color(info.img.color.r, info.img.color.g, info.img.color.b, 0.0f);
		}
	}

	// 텍스트 FadeIn
	public void TextFadeIn(float time, float rate, ref Text txt)
	{
		FadeInfoTxt info;
		info.time = time;
		info.save_time = 0.0f;
		info.txt = txt;
		info.rate = rate;
		StartCoroutine("TextFadeInC", info);
	}

	IEnumerator TextFadeInC(FadeInfoTxt info)
	{
		info.save_time += Time.unscaledDeltaTime;
		if(info.txt != null)
		{
			if(info.save_time < info.time)
			{
				float rate = info.save_time / info.time;
				info.txt.color = new Color(info.txt.color.r, info.txt.color.g, info.txt.color.b, info.rate * rate);
				yield return null;
				StartCoroutine("TextFadeInC", info);
			}
			else
				info.txt.color = new Color(info.txt.color.r, info.txt.color.g, info.txt.color.b, info.rate);
		}
	}
	
	// 텍스트 FadeOut
	public void TextFadeOut(float time, ref Text txt)
	{
		FadeInfoTxt info = new FadeInfoTxt();
		info.time = time;
		info.save_time = 0.0f;
		info.txt = txt;
		info.rate = txt.color.a;
		StartCoroutine("TextFadeOutC", info);
	}

	IEnumerator TextFadeOutC(FadeInfoTxt info)
	{
		info.save_time += Time.unscaledDeltaTime;
		if(info.txt != null)
		{
			if (info.save_time < info.time)
			{
				float rate = (info.save_time / info.time) * info.rate;
				info.txt.color = new Color(info.txt.color.r, info.txt.color.g, info.txt.color.b, 1- rate);
				yield return null;
				StartCoroutine("TextFadeOutC", info);
			}
			else
				info.txt.color = new Color(info.txt.color.r, info.txt.color.g, info.txt.color.b, 0);
		}
		
	}
	public void SpriteFadeOut(float time, float rate, ref SpriteRenderer spr)
	{
		FadeInfoSp info;
		info.time = time;
		info.save_time = 0.0f;
		info.spr = spr;
		info.rate = rate;
		StartCoroutine("SpriteFadeOutC", info);
	}

	IEnumerator SpriteFadeOutC(FadeInfoSp info)
	{
		info.save_time += Time.unscaledDeltaTime;
		if (info.spr != null)
		{
			if (info.save_time < info.time)
			{
				float rate = info.save_time / info.time;
				info.spr.color = new Color(info.spr.color.r, info.spr.color.g, info.spr.color.b, 1 - info.rate * rate);
				yield return null;
				StartCoroutine("SpriteFadeOutC", info);
			}
		}
	}

	public void ImageFillAmount(float time, float rate, ref Image img)
	{
		ImageFillInfo info;
		info.time = time;
		info.save_time = 0.0f;
		info.img = img;
		info.speed = (rate - img.fillAmount) / time;
		StartCoroutine("ImageFillAmountC", info);
	}

	IEnumerator ImageFillAmount(ImageFillInfo info)
	{
		info.save_time += Time.deltaTime;
		if(info.img != null)
		{
			if(info.save_time < info.time)
			{
				info.img.fillAmount = info.img.fillAmount + info.speed * Time.deltaTime;
				yield return null;
				StartCoroutine("ImageFillAmountC", info);
			}
		}
	}

	// Scale 함수
	public void ScaleTo(float time, Vector2 value, ref GameObject obj)
	{
		ScaleInfo info = new ScaleInfo();
		info.time = time;
		info.value = value;
		info.obj = obj;
		info.speed.x = (value.x - obj.transform.localScale.x) / time;
		info.speed.y = (value.y - obj.transform.localScale.y) / time;
		StartCoroutine("ScaleToC", info);
	}

	IEnumerator ScaleToC(ScaleInfo info)
	{
		info.save_time += Time.deltaTime;
		if(info.obj != null)
		{
			if (info.save_time < info.time)
			{
				info.obj.transform.localScale = new Vector3(info.obj.transform.localScale.x + info.speed.x * Time.deltaTime, info.obj.transform.localScale.y + info.speed.y * Time.deltaTime, info.obj.transform.localScale.z);
				yield return null;
				StartCoroutine("ScaleToC", info);
			}
			else
				info.obj.transform.localScale = new Vector3(info.value.x, info.value.y, info.obj.transform.localScale.z);
		}
	}

	// 타이핑 효과
	public void TypingEffect(string value, float time, ref Text txt)
	{
		TypeInfo info;
		info.time = time;
		info.value = value;
		info.txt = txt;
		StartCoroutine("TypingEffectC", info);
	}

	IEnumerator TypingEffectC(TypeInfo info)
	{
		string colorName = "";
		string colorValue = "";
		for(int i = 0; i < info.value.Length; i++)
		{
			if (info.txt.text != null)
			{
				if (info.value[i].ToString() != "<" && colorName == "")
				{
					info.txt.text += info.value[i];
					yield return new WaitForSeconds(info.time);
				}
				else
				{
					string check = info.value[i].ToString();
					for(int j = i; check != ">"; j++)
					{
						colorName += info.value[j];
						check = info.value[j].ToString();
						i++;
					}
					check = info.value[i].ToString();
					for (int y = i; check != "<"; y++)
					{
						colorValue += info.value[y];
						check = info.value[y+1].ToString();
						i++;
					}
					for(int v = 0; v < colorValue.Length; v++)
					{
						info.txt.text += colorName + colorValue[v] + "</color>";
						yield return new WaitForSeconds(info.time);
					}
					colorName = "";
					colorValue = "";
					i += 8;
					i--;
				}
			}
		}
	}
}
