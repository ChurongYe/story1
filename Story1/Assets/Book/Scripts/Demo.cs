using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Demo : MonoBehaviour
{
    public BookController bookController;
    
    [SerializeField]
    Button nextButton;
    [SerializeField]
    Button previousButton;
    [SerializeField]
    Image bookImage;
    [SerializeField]
    Sprite bookTexture;
    [SerializeField]
    Sprite notepadTexture;

    public GameObject[] pages;

    int currentPage;
    View currentView;

    public AudioSource audioSource;
    public AudioClip book;
    public enum View
    {
        Book,
        Notepad
    }

    void Start()
    {
        currentPage = PlayerPrefs.GetInt("CurrentPage", 0);
        UpdatePage();

        nextButton.onClick.AddListener(NextPage);
        previousButton.onClick.AddListener(PreviousPage);
    }

    public void SetBook(bool value)
    {
        SetView(value ? View.Book : View.Notepad);
    }

    void SetView(View value)
    {
        if (currentView == value) return;

        currentView = value;
        bookImage.sprite = currentView == View.Book ? bookTexture : notepadTexture;
    }

    void NextPage()
    {
        bookController.NextPage();
        currentPage = Mathf.Min(++currentPage, pages.Length - 1);
        PlayerPrefs.SetInt("CurrentPage", currentPage); // 保存当前页码
        StartCoroutine(UpdatePageDelayed());
        audioSource.PlayOneShot(book);
    }

    void PreviousPage()
    {
        bookController.PreviousPage();
        currentPage = Mathf.Max(--currentPage, 0);
        PlayerPrefs.SetInt("CurrentPage", currentPage); // 保存当前页码
        StartCoroutine(UpdatePageDelayed());
        audioSource.PlayOneShot(book);
    }
    
    IEnumerator UpdatePageDelayed()
    {
        yield return new WaitForEndOfFrame();
        UpdatePage();
    }
    
    void UpdatePage()
    {
        Array.ForEach(pages, c => { c.SetActive(false);});
        pages[currentPage].SetActive(true);
        
        nextButton.gameObject.SetActive(currentPage < pages.Length - 1);
        previousButton.gameObject.SetActive(currentPage > 0);
    }
}
