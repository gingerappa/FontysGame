using UnityEngine;

public class WaveUpdate : MonoBehaviour
{
    [SerializeField] private UpdateText waveAmount;
    [SerializeField] private Animation waveAnimation;
    public void updateWave(int wave)
    {
        waveAmount.ChangeText(wave.ToString());
        waveAnimation.Play();
    }
}
