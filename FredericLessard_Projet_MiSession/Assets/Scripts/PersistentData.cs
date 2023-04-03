using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

public static class PersistentData { 
    
    public static void Serialize(int coins)
    {
        SaveData saveData = new SaveData(0);
        if (File.Exists(Path.Combine(Application.persistentDataPath, "Save.data")))
        {
            saveData = Deserialize();
            saveData.coins += coins;
        }
     
        // Create a hashtable of values that will eventually be serialized.
        Hashtable serializedValues = new Hashtable();

        serializedValues.Add("coins", saveData.coins.ToString());
        
        // To serialize the hashtable and its key/value pairs,
        // you must first open a stream for writing.
        // In this case, use a file stream.
        FileStream fs = new FileStream(Path.Combine(Application.persistentDataPath, "Save.data"), FileMode.Create);

        // Construct a BinaryFormatter and use it to serialize the data to the stream.
        BinaryFormatter formatter = new BinaryFormatter();
        try
        {
            formatter.Serialize(fs, serializedValues);
        }
        catch (SerializationException e)
        {
            Debug.LogError("Failed to serialize. Reason: " + e.Message);
            throw;
        }
        finally
        {
            fs.Close();
        }
    }

    public static SaveData Deserialize()
    {
        // Declare the hashtable reference.
        Hashtable dataTable= null;
        SaveData saveData = null;

        if (File.Exists(Path.Combine(Application.persistentDataPath, "Save.data")))
        {
            // Open the file containing the data that you want to deserialize.
            FileStream fs = new FileStream(Path.Combine(Application.persistentDataPath, "Save.data"), FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                // Deserialize the hashtable from the file and
                // assign the reference to the local variable.
                dataTable = (Hashtable)formatter.Deserialize(fs);
               
                saveData = new SaveData(System.Convert.ToInt32(dataTable["coins"]));
            }
            catch (SerializationException e)
            {
                Debug.LogError("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {

                fs.Close();
            }
        }
        else
        {
            saveData = new SaveData(0);
        }
        return saveData;

    }
}
