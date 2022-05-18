using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CutsceneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _cutScenePanel;
    [SerializeField]
    private Image _cutSceneImage;

    #region IntroCutsceneVariables
    [SerializeField]
    private bool _playIntroCutscene;
    [SerializeField]
    private TextMeshProUGUI _nameHeader;
    [SerializeField]
    private TextMeshProUGUI _name;
    [SerializeField]
    private TextMeshProUGUI _objectiveHeader;
    [SerializeField]
    private TextMeshProUGUI _objective;
    private WaitForSeconds _introWaitTime = new WaitForSeconds(1f);
    #endregion

    #region OutroCutsceneVariables
    [SerializeField]
    private TextMeshProUGUI _slideText;
    [SerializeField]
    private TextMeshProUGUI _credits;
    [SerializeField]
    private TextMeshProUGUI _theEnd;
    [SerializeField]
    private TextMeshProUGUI _questionMark;
    private WaitForSeconds _outroSlidesWait = new WaitForSeconds(7f);
    private WaitForSeconds _2secondPause = new WaitForSeconds(2f);
    private string[] _stolenSlides;

    [SerializeField]
    private Transform _itemsPanel;
    [SerializeField]
    private TextMeshProUGUI _itemTextPrefab;
    [SerializeField]
    private TextMeshProUGUI _itemHeader;
    [SerializeField]
    private TextMeshProUGUI _priceTotal;

    private float _scrollSpeed = 110f;
    private float _fadeSpeed = 0.005f;

    [SerializeField]
    private OutroSequence _stealSequence;
    [SerializeField]
    private OutroSequence _buySequence;
    #endregion

    [SerializeField]
    private AudioSource _sfxSource;
    [SerializeField]
    private AudioSource _bgmSource;

    [SerializeField]
    private AudioClip _boomSfx;
    [SerializeField]
    private AudioClip _carSfx;
    [SerializeField]
    private AudioClip _outroMusic;


    [SerializeField]
    private PlayerMovement _playerMovement;
    [SerializeField]
    private MouseLook _mouseLook;
    void Start()
    {
        _slideText.enabled = false;
        _nameHeader.alpha = 0f;
        _name.alpha = 0f;
        _objectiveHeader.alpha = 0f;
        _objective.alpha = 0f;
        _theEnd.alpha = 0f;
        _questionMark.alpha = 0f;
        _itemHeader.alpha = 0f;
        _priceTotal.alpha = 0f;

        TextMeshProUGUI[] textMeshProUGUIs = new TextMeshProUGUI[] { _nameHeader, _name, _objectiveHeader, _objective };

        if (!_playIntroCutscene)
        {
            var newColor = _cutSceneImage.color;
            newColor.a = 0f;
            _cutSceneImage.color = newColor;
            return;
        }
        StartCoroutine(PlayIntroCutscene(textMeshProUGUIs));
    }


    private IEnumerator PlayIntroCutscene(TextMeshProUGUI[] textToPrint)
    {
        _mouseLook.state = MouseLook.State.Menu;
        _playerMovement.remainStationary = true;
        UIScript.ChangeUI(UIEnum.MENU);
        yield return _introWaitTime;

        for (int i = 0; i < textToPrint.Length; i++)
        {
            textToPrint[i].alpha = 1f;
            _sfxSource.PlayOneShot(_boomSfx);

            yield return _introWaitTime;
        }
        yield return _introWaitTime;
        StartCoroutine(FadeIntroText());
        StartCoroutine(FadeIntroBackGround());
        _mouseLook.state = MouseLook.State.Movement;
        _playerMovement.remainStationary = false;
        UIScript.ChangeUI(UIEnum.CROSSHAIR);
    }

    private IEnumerator FadeIntroText()
    {
        while (_nameHeader.alpha >= 0f)
        {
            _nameHeader.alpha -= 0.001f;
            _name.alpha -= 0.001f;
            _objectiveHeader.alpha -= 0.001f;
            _objective.alpha -= 0.001f;

            yield return null;
        }
    }

    private IEnumerator FadeIntroBackGround()
    {
        while (_cutSceneImage.color.a >= 0f)
        {
            var newColor = _cutSceneImage.color;
            newColor.a = _cutSceneImage.color.a - 0.005f;
            _cutSceneImage.color = newColor;

            yield return null;
        }
    }

    private IEnumerator OutroCutscene(bool stolen)
    {
        var newColor = _cutSceneImage.color;
        newColor.a = 1f;
        _cutSceneImage.color = newColor;
        _sfxSource.PlayOneShot(_carSfx);
        yield return new WaitForSeconds(6f);

        _bgmSource.Play();

        if (stolen)
        {
            for (int i = 0; i < _stealSequence.outroSlides.Length - 1; i++)
            {          
                yield return _2secondPause;
                _slideText.text = _stealSequence.outroSlides[i].slideText;
                _slideText.enabled = true;
                StartCoroutine(FadeTextIn(_slideText, _fadeSpeed));
                yield return _outroSlidesWait;
                _slideText.alpha = 0f;
            }

            yield return StartCoroutine(totalItems(stolen));


            for (int i = _stealSequence.outroSlides.Length - 1; i < _stealSequence.outroSlides.Length; i++)
            {
                yield return _2secondPause;
                _slideText.text = _stealSequence.outroSlides[i].slideText;
                _slideText.enabled = true;
                StartCoroutine(FadeTextIn(_slideText, _fadeSpeed));
                yield return _outroSlidesWait;
                _slideText.alpha = 0f;
            }

        }
        else
        {
            for (int i = 0; i < 1; i++)
            {            
                yield return _2secondPause;
                _slideText.text = _buySequence.outroSlides[i].slideText;
                _slideText.enabled = true;
                StartCoroutine(FadeTextIn(_slideText, _fadeSpeed));
                yield return _outroSlidesWait;
                _slideText.alpha = 0f;
            }

            yield return StartCoroutine(totalItems(stolen));


            for (int i = 1; i < _buySequence.outroSlides.Length; i++)
            {
                yield return _2secondPause;
                _slideText.text = _buySequence.outroSlides[i].slideText;
                _slideText.enabled = true;
                StartCoroutine(FadeTextIn(_slideText, _fadeSpeed));
                yield return _outroSlidesWait;
                _slideText.alpha = 0f;
            }

        }
        _slideText.alpha = 0f;

        

        StartCoroutine(ScrollText());
        yield return new WaitForSeconds(20f);


        _slideText.alpha = 0f;
        yield return _2secondPause;
        StartCoroutine(FadeTextIn(_theEnd, _fadeSpeed));
        yield return new WaitForSeconds(4f);
        StartCoroutine(FadeTextIn(_questionMark, 0.0005f));
    }

    public void BeginOutro(bool stolen)
    {
        _mouseLook.state = MouseLook.State.Menu;
        _playerMovement.remainStationary = true;
        UIScript.ChangeUI(UIEnum.MENU);
        //Debug.Log("beginning outro cutscene! Stolen = " + stolen);
        StartCoroutine(OutroCutscene(stolen));
    }

    private IEnumerator FadeTextIn(TextMeshProUGUI  textToFade, float fadeSpeed)
    {
        //Debug.Log("startingFadeIn");
        while (textToFade.alpha <= 1f)
        {
            textToFade.alpha += fadeSpeed;

            //Debug.Log(_slideText.alpha);
            yield return null;
        }
    }

    private IEnumerator ScrollText()
    {
        Camera mainCamera = Camera.main;

        while (true)
        {
            _credits.GetComponentInParent<Transform>().Translate(Vector3.up * _scrollSpeed * Time.deltaTime);

            yield return null;
        }
        
    }

    private IEnumerator totalItems(bool stolen)
    {
        WaitForSeconds oneSecondWait = new WaitForSeconds(1f);

        if (stolen)
        {
            _itemHeader.text = "You stole:";
        }
        else
        {
            _itemHeader.text = "You bought:";
        }
        StartCoroutine(FadeTextIn(_itemHeader, _fadeSpeed));
        yield return _2secondPause;
        List<string> uniqueItems = Inventory.Instance.Items.Select(t => t.itemName).Distinct().ToList();

        foreach (string itemName in uniqueItems)
        {
            int itemCount = 0;

            foreach (ItemDataSO item  in Inventory.Instance.Items)
            {
                if (item.itemName == itemName)
                {
                    itemCount++;
                }
            }          
            TextMeshProUGUI newItemText = Instantiate(_itemTextPrefab, _itemsPanel);
            newItemText.text = itemCount + "x " + itemName;
            _sfxSource.PlayOneShot(_boomSfx, 0.7f);
            yield return oneSecondWait;
        }

        _priceTotal.alpha = 1f;
        _sfxSource.PlayOneShot(_boomSfx, 2f);
        yield return _2secondPause;

        _priceTotal.text = _priceTotal.text + Inventory.Instance.PriceTotal();
        _sfxSource.PlayOneShot(_boomSfx, 3f);

        yield return new WaitForSeconds(5f);

        _itemsPanel.gameObject.SetActive(false);
        _itemHeader.alpha = 0f;
        _priceTotal.alpha = 0f;
    }
}
