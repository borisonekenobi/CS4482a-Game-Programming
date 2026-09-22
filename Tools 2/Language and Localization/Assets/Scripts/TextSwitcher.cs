using TMPro;
using UnityEngine;

public class TextSwitcher : MonoBehaviour
{
	[SerializeField]
	private TMP_Text text;
	[SerializeField]
	private float switchInterval = 2f;

	private float _timer;
	private int _index;
	private readonly string[] _keys = { "key_ok", "key_cancel", "key_save", "key_back", "key_next" };

	private void Update()
	{
		_timer += Time.deltaTime;
		if (!(_timer >= switchInterval)) return;

		_index = ++_index % _keys.Length;
		text.text = Localization.Get(_keys[_index]);
		_timer = 0f;
	}
}
