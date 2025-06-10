using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace MyFps
{
    //메인 메뉴 씬을 관리하는 클래스
    public class MainMenu : MonoBehaviour
    {
        #region Variables
        //참조
        private AudioManager audioManager;

        //씬 변경
        public SceneFader fader;
        [SerializeField]private string sceneToLoad = "MainScene01";

        //메뉴
        public GameObject mainMenuUI;
        public GameObject optionsUI;
        public GameObject creditCanvas;

        private bool isShowOption = false;
        private bool isShowCredit = false;

        //볼륨 조절
        public AudioMixer audioMixer;

        public Slider bgmSlider;
        public Slider sfxSlider;
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //옵션 저장값 가져오기
            LoadOptions();

            //참조
            audioManager = AudioManager.Instance;

            //씬 시작 시 페이드 인 효과
            fader.FadeStart();

            //메뉴 배경음 플레이
            audioManager.Play("MenuMusic");

            //초기화
            isShowOption = false;
            isShowCredit = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isShowOption)
                {
                    OptionsExit();
                }
                else if(isShowCredit)
                {
                    HideCreditUI();
                }
            }
        }
        #endregion

        #region Custom Method
        public void NewGame()
        {
            //메뉴 선택 사운드
            audioManager.Play("MenuSelect");
            //새 게임 하러가기
            fader.FadeTo(sceneToLoad);
        }
        
        public void LoadGame()
        {
            //메뉴 선택 사운드
            audioManager.Play("MenuSelect");
            Debug.Log("LOAD GAME!!!");
        }

        public void Options()
        {
            //메뉴 선택 사운드
            audioManager.Play("MenuSelect");

            mainMenuUI.SetActive(false);
            optionsUI.SetActive(true);

            isShowOption = true;
        }

        public void OptionsExit()
        {
            mainMenuUI.SetActive(true);
            optionsUI.SetActive(false);

            isShowOption = false;
        }

        public void Credits()
        {
            //메뉴 선택 사운드
            audioManager.Play("MenuSelect");

            //크레딧 UI 보여주기
            StartCoroutine(ShowCreditUI());

            isShowCredit = true;
        }
        //크레딧 UI 보여주기
        IEnumerator ShowCreditUI()
        {
            mainMenuUI.SetActive(false);
            creditCanvas.SetActive(true);

            yield return new WaitForSeconds(6f);

            HideCreditUI();
        }
        //크레딧 UI 나가기
        public void HideCreditUI()
        {
            mainMenuUI.SetActive(true);
            creditCanvas.SetActive(false);

            isShowCredit = false;
        }
        public void QuitGame()
        {
            //ToDo: Cheating
            PlayerPrefs.DeleteAll();
            //메뉴 선택 사운드
            audioManager.Play("MenuSelect");
            Debug.Log("QUIT GAME");
            Application.Quit();
        }

        //Bgm 볼륨 조절
        public void SetBgmVolume(float value)
        {
            //볼륨값 저장
            PlayerPrefs.SetFloat("Bgm", value);
            
            //볼륨 조절
            audioMixer.SetFloat("Bgm", value);
        }

        //Sfx 볼륨 조절
        public void SetSfxVolume(float value)
        {
            //볼륨값 저장
            PlayerPrefs.SetFloat("Sfx", value);

            //볼륨 조절
            audioMixer.SetFloat("Sfx", value);
        }

        //옵션 저장값들을 가져와서 게임에 적용한다
        private void LoadOptions()
        {
            //배경음 볼륨값 가져오기
            float bgmVolume = PlayerPrefs.GetFloat("Bgm", 0f);
            SetBgmVolume(bgmVolume);
            //UI에 적용
            bgmSlider.value = bgmVolume;

            //효과음 볼륨값 가져오기
            float sfxVolume = PlayerPrefs.GetFloat("Sfx", 0f);
            SetSfxVolume(sfxVolume);
            //UI에 적용
            sfxSlider.value = sfxVolume;
        }
        #endregion
    }
}
