using RTLTMPro;
using UnityEngine;

namespace I2.Loc
{
	public class CallbackNotification : MonoBehaviour
	{

		[SerializeField] private RTLTextMeshPro _playersCountText;

		public LocalizedString _MyLocalizedString;                    

		public void OnModifyLocalization()
		{
			_playersCountText.text =  _MyLocalizedString;
		}
	}
}