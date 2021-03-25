using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Sockets;

public class ScoreTable : MonoBehaviour
{
    private Transform entries;
    private Transform entryTemplate;

    private void Awake() {
        // результат игры, отправляется на сервер
        string[] userScoreArr = new[] { "2421", "User" }; 

        int cols = 2;

        // передаем на сервер статистику пользователя и получаем в ответ общую статистику
        string[,] scoreArr = someDBStuff(userScoreArr);

        // шаблон для записи
        entries = transform.Find("bg").Find("entries");
        entryTemplate = entries.Find("entryTemplate");

        entryTemplate.gameObject.SetActive(false);

        // заполнение таблицы
        float templateHeight = 50f;
        for (int i = 0; i < scoreArr.Length/cols; i++){
            Transform entryTransform = Instantiate(entryTemplate, entries);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight*i);
            entryTransform.gameObject.SetActive(true);

            entryTransform.Find("TextEntryPos").GetComponent<Text>().text = (i+1).ToString();
            entryTransform.Find("TextEntryScore").GetComponent<Text>().text = scoreArr[i, 0];
            entryTransform.Find("TextEntryName").GetComponent<Text>().text = scoreArr[i, 1];
        }
    }

    // для обработки полученного массива
    private string[,] deserialize(MemoryStream ms) {
        ms.Position = 0;
        BinaryFormatter bf = new BinaryFormatter();
        string[,] scoreArray = (string[,])bf.Deserialize(ms);
        return scoreArray;
    }

    // передает на сервер статистику пользователя и возвращает массив с общей статистикой
    private string[,] someDBStuff(string[] userScore) {
        // Для подключения
        const int port = 8888;
        const string address = "127.0.0.1";

        TcpClient client = null;
        BinaryFormatter bf = new BinaryFormatter();
        MemoryStream ms = new MemoryStream();

        try
        {
            client = new TcpClient(address, port);
            NetworkStream stream = client.GetStream();

            bf.Serialize(ms, userScore);
            byte[] data = ms.ToArray();

            //for (int i = 0; i < data.Length; i++) { Debug.Log(data[i] + "[" + i + "]"); }

            // отправка сообщения
            stream.Write(data, 0, data.Length);

            ms.Position = 0;
            string[] uScore = (string[])bf.Deserialize(ms);

            // получаем ответ
            data = new byte[256];
            ms = new MemoryStream();

            int bytes = 0;
            do
            {
                bytes = stream.Read(data, 0, data.Length);
                ms.Write(data, 0, bytes);
            }
            while (stream.DataAvailable);
        }
        catch (Exception ex) {
            Debug.Log(ex.Message);
        }
        finally {
            client.Close();
        }

        byte[] bArr = ms.ToArray();
        //for (int i = 0; i < bArr.Length; i++) { Debug.Log(bArr[i]+"["+i+"]"); }

        ms.Position = 0;
        string[,] globalScoreArr = (string[,])bf.Deserialize(ms);
        // массив со всеми результатами с сервера
        return globalScoreArr; 
    }

        // Start is called before the first frame update
        // void Start(){ }

        // Update is called once per frame
        // void Update(){ }
    }
