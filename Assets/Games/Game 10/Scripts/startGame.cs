using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startGame : MonoBehaviour
{

    [SerializeField] private GameObject[] bubbleList; // Чистый исходный список всех бубликов
    [SerializeField] private static GameObject[] bubbleListFromSettings; // Рабочий список бубликов
    [SerializeField] private GameObject[] positionsList; // Список доступных точек спавна в виде GameObject-ов
    [SerializeField] private static bool[] allowedPos; // Список bool переменных для проверки доступности каждой точки спавна
    [SerializeField] private GameObject[] exampleParts; // Список GameObject-ов бубликов в примере
    [SerializeField] private Slider settingsSlider; // Список GameObject-ов бубликов в примере
    [SerializeField] private GameObject pyramidStartPoint; // Точка начала пирамиды
    [SerializeField] private static float nextYCoordinate; // Точка спавна следующего элемента
    [SerializeField] private int shiftRatio; // Коэффициент перемещения
    [SerializeField] private int secondsForRestart = 5;
    [SerializeField] private static bool restartTimer = false;
    bool timerOn;
    int seconds;
    int minutes;

    [SerializeField] private static int currentBubble; // Временная переменная какой следующий бублик

    [SerializeField] private GameObject[] timer;

    [SerializeField] private AudioSource trueBubble;
    [SerializeField] private AudioSource falseBubble;
    [SerializeField] private AudioSource victoryBubble;

    [SerializeField] private GameObject fanfars;



    private void OnEnable()
    {
        timer[1].GetComponent<Text>().text = "0";
        timer[0].GetComponent<Text>().text = "0";
        fanfars.SetActive(false); // Выключаем фанфары на всякий случай
	settingsSlider.value = 4;
        SettingsPartsChanged(settingsSlider.value);
        RandomizeParts(); // Вызов метода рандомизации бублеччччков
    }

    private void Start()
    {
        bubbleListFromSettings = bubbleList; // На старте присваивание рабочего списка GameObject из чистого
	settingsSlider.value = 4;
        SettingsPartsChanged(settingsSlider.value);
        RandomizeParts(); // Продублировано потому что без нее в собранной чистой игре изначально нет бубликов
    }

    public void SetObject(GameObject bubble)
    {
        if (bubbleList[currentBubble] == bubble)
        {
            if (currentBubble == (bubbleListFromSettings.Length - 1))
            {
                bubbleListFromSettings[currentBubble].transform.GetChild(0).gameObject.SetActive(false);
                bubbleListFromSettings[currentBubble].transform.position = new Vector2(pyramidStartPoint.transform.position.x, nextYCoordinate);
                victoryBubble.Play(); // Включить звук победы
                fanfars.SetActive(true); // Включаем обьект фанфар
                fanfars.GetComponent<Animation>().Play(); // Запускаем анимацию фанфар
                currentBubble++;
                restartTimer = true; // Переменная таймера = true
                secondsForRestart = 5; // Установка таймера на 5 секунд
                StartCoroutine("startNewGame"); // Начало функции таймера нового раунда
            }
            else if (currentBubble < bubbleListFromSettings.Length)
            {
                bubbleListFromSettings[currentBubble].transform.GetChild(0).gameObject.SetActive(false);
                bubbleListFromSettings[currentBubble].transform.position = new Vector2(pyramidStartPoint.transform.position.x, nextYCoordinate);
                var rectTrans = bubbleListFromSettings[currentBubble].transform as RectTransform;
                float shift = 5;
                if (rectTrans != null)
                {
                    shift = rectTrans.rect.height;
                }
                nextYCoordinate = nextYCoordinate + (shift * shiftRatio);
                trueBubble.Play(); // Звук правильного выбора
                currentBubble++;
            }
        } else {
            if (bubble.transform.GetChild(0).gameObject.activeSelf) // Проверяем что навелись на бублик который не на финальной пирамиде
            {
                falseBubble.Play(); // Запускаем неправильный звук выбора
                bubble.GetComponent<Animation>().Play(); // Запускаем анимацию тряски бублика
            }
        }
    }

    public void SettingsPartsChanged(float value)
    {
        StopCoroutine("gameTimer");
        timerOn = false;
        timer[1].GetComponent<Text>().text = "0";
        timer[0].GetComponent<Text>().text = "0";
        if (restartTimer) // Если переменная таймера == true то
        {
            StopCoroutine("startNewGame"); // Останавливаем поток таймера до нового раунда
            restartTimer = false; // Показатель того, запущен ли таймер раунда = false
        }
        fanfars.SetActive(false); // Выключаем фанфары
        if (fanfars.GetComponent<Animation>().isPlaying) // Проверяем идет ли анимация фанфары
        {
            fanfars.GetComponent<Animation>().Stop(); // Выключаем анимацию фанфар
        }

        int valueInt = (int)value;
        bubbleListFromSettings = new GameObject[valueInt]; // Создание списка gameObject с размером выбранным в Slider
        for (int i = 0; i < bubbleListFromSettings.Length; i++) // Заполнение списка элементами из чистого списка gameObject
        {
            bubbleListFromSettings[i] = bubbleList[i];
            bubbleListFromSettings[i].transform.GetChild(0).gameObject.SetActive(true);
        }

        // Сюда нужно будет добавить обнуление таймера

        RandomizeParts(); // Вызов метода рандомизации бублеччччков


    }

    private void RandomizeParts()
    {
        StopCoroutine("gameTimer");
        timerOn = false;
        timer[1].GetComponent<Text>().text = "0";
        timer[0].GetComponent<Text>().text = "0";
        for (int i = 0; i < bubbleList.Length; i++) // Перенос всех бубликов за экран, чтобы если в слайдере выбрано меньше 6, то лишние не остались на экране
        {
            bubbleList[i].transform.position = new Vector2(2630f, 14f);
        }
        allowedPos = new bool[positionsList.Length]; // Инициализация массива доступных позиций с размером = количеству gameObject position
        for (int i = 0; i < allowedPos.Length; i++) // Заполнение значениями списка доступных позиций
        {
            allowedPos[i] = true;
        }
        foreach (GameObject bubble in bubbleListFromSettings) // Спавн каждого бублика в случайных позициях
        {
            int i = Random.Range(0, positionsList.Length); // Получение первичной случайной позиции
            while (allowedPos[i].Equals(false)) // Пока не попадется доступная позиция делаем:
            {
                i = Random.Range(0, positionsList.Length); // Получение случайной позиции
            }
            allowedPos[i] = false; // Указание false для текущей позиции
            bubble.transform.position = positionsList[i].transform.position; // Перенос на позицию
        }
        nextYCoordinate = pyramidStartPoint.transform.position.y; // Установка начальной позиции для первого бублика
        currentBubble = 0;
        ShowCurrentExample();
        if (!timerOn) StartCoroutine("gameTimer");
    }

    private void ShowCurrentExample()
    {
        for (int i = 0; i < bubbleList.Length; i++) // Скрываем все бублики из примера
        {
            exampleParts[i].SetActive(false);
        }
        for (int i = 0; i < bubbleListFromSettings.Length; i++) // Показываем все бублики исходя из выбранного количества частей
        {
            exampleParts[i].SetActive(true);
        }
    }

    private void OnDisable()
    {
        StopCoroutine("gameTimer");
        timerOn = false;
        timer[1].GetComponent<Text>().text = "0";
        timer[0].GetComponent<Text>().text = "0";
        if (restartTimer) // Если переменная таймера == true то
        {
            StopCoroutine("startNewGame"); // Останавливаем поток таймера до нового раунда
            restartTimer = false; // Показатель того, запущен ли таймер раунда = false
        }
        fanfars.SetActive(false); // Выключаем фанфары
        if(fanfars.GetComponent<Animation>().isPlaying) // Проверяем идет ли анимация фанфары
        {
            fanfars.GetComponent<Animation>().Stop(); // Выключаем анимацию фанфар
        }
        
        for(int i=0; i<bubbleList.Length; i++) // Возобновляем у всех бубликов работу глазных кнопок
        {
            bubbleList[i].transform.GetChild(0).gameObject.SetActive(true);
        }

        // Сюда нужно будет добавить обнуление таймера

    }

    private void RestartGame()
    {
        StopCoroutine("gameTimer");
        timerOn = false;
        timer[1].GetComponent<Text>().text = "0";
        timer[0].GetComponent<Text>().text = "0";
        StopCoroutine("startNewGame"); // Останавливаем поток таймера до нового раунда
        secondsForRestart = 5; // Устанавливаем исходное время ожидания
        restartTimer = false; // Показатель того, запущен ли таймер раунда = false
        fanfars.SetActive(false); // Выключаем фанфары
        if (fanfars.GetComponent<Animation>().isPlaying) // Проверяем идет ли анимация фанфары
        {
            fanfars.GetComponent<Animation>().Stop(); // Останавливаем анимацию фанфар
        }
        settingsSlider.value = settingsSlider.value == 10 ? 4 : settingsSlider.value + 1;
        SettingsPartsChanged(settingsSlider.value); // Вызываем обновление списка бубликов

        // Сюда нужно будет добавить обнуление таймера

    }

    IEnumerator startNewGame()
    {
        for (; ; ) // Бесконечный цикл в отдельном потоке
        {
            StopCoroutine("gameTimer");
            timerOn = false;
            secondsForRestart--; // Уменьшаем время
            Debug.Log("Coroutine seconds to restart: " + secondsForRestart);
            if (secondsForRestart == 0) // Если время == 0 то:
            {
                RestartGame(); // Перезапускаем раунд игры
            }
            yield return new WaitForSeconds(1f); 
        }
    }

    IEnumerator gameTimer()
    {
        for (; ; ) // Бесконечный цикл в отдельном потоке
        {
            seconds = int.Parse(timer[1].GetComponent<Text>().text);
            minutes = int.Parse(timer[0].GetComponent<Text>().text);
            seconds = seconds + 1;
            if (seconds == 60)
            {
                minutes++;
                seconds = 0;
                timer[1].GetComponent<Text>().text = seconds.ToString();
                timer[0].GetComponent<Text>().text = minutes.ToString();
            }
            timer[1].GetComponent<Text>().text = seconds.ToString();

            yield return new WaitForSeconds(1f);
        }
    }

}
