using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";
    private bool useEncryption = false;
    private readonly string encryptionCodeWord = "Kupi";

    public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
        this.useEncryption = useEncryption;
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath,dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToload = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream)) 
                    { 
                        dataToload= reader.ReadToEnd();
                    }
                }
                if (useEncryption)
                {
                    dataToload = EncryptDecrypt(dataToload);
                }
                loadedData = JsonUtility.FromJson<GameData>(dataToload);
            }
            catch (Exception e)
            {
                Debug.LogError("เกิดเออเรอร์ตอนพยายามโหลดข้อมูล" + fullPath + "\n" + e);
            }

        }
        return loadedData;
    }
    public void Save(GameData data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
            if(useEncryption) 
            {
                dataToStore = EncryptDecrypt(dataToStore); 
            }
        }
        catch (Exception e)
        {
            Debug.LogError("เกิดเออเรอร์ตอนพยามจะเซฟเกม" + fullPath + "\n" + e);
        }
    }
    
    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++) 
        {
            modifiedData += (char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]); 
        }
        return modifiedData;
    }
}
