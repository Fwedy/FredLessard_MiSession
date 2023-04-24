using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

public static class PersistentData { 
    
    public static void Serialize(int coins, string codeType, string language)
    {
        SaveData saveData = new SaveData(0, "", "en");
        if (File.Exists(Path.Combine(Application.persistentDataPath, "Save.data")))
        {
            saveData = Deserialize();
            saveData.coins += coins;
            saveData.codeTypes += codeType;

           if (language != null)
              saveData.language = language;
        }
     
        Hashtable serializedValues = new Hashtable();

        serializedValues.Add("coins", saveData.coins.ToString());
        serializedValues.Add("codeTypes", saveData.codeTypes.ToString());
        serializedValues.Add("language", saveData.language.ToString());
       
        
        FileStream fs = new FileStream(Path.Combine(Application.persistentDataPath, "Save.data"), FileMode.Create);

        
        BinaryFormatter formatter = new BinaryFormatter();
        try
        {
            formatter.Serialize(fs, serializedValues);
        }
        catch (SerializationException e)
        {
            Debug.LogError("Failed: " + e.Message);
            throw;
        }
        finally
        {
            fs.Close();
        }
    }

   

    public static SaveData Deserialize()
    {
        Hashtable dataTable = null;
        SaveData saveData = null;

        if (File.Exists(Path.Combine(Application.persistentDataPath, "Save.data")))
        {
            
            FileStream fs = new FileStream(Path.Combine(Application.persistentDataPath, "Save.data"), FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

               
                dataTable = (Hashtable)formatter.Deserialize(fs);

                saveData = new SaveData(System.Convert.ToInt32(dataTable["coins"]), (string)dataTable["codeTypes"], (string)dataTable["language"]);
                
                Debug.Log("Coins:" + saveData.coins + " :: Codes:" + saveData.codeTypes + " :: Language:" + saveData.language);
            }
            catch (SerializationException e)
            {
                Debug.LogError("Failed : " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }
        else
        {
            saveData = new SaveData(0, "", "en");
            
        }
        return saveData;

    }

    public static void DeleteSaveFile()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "Save.data");
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("Save file deleted.");
        }
        else
        {
            Debug.LogWarning("Save file not found.");
        }
    }

}
