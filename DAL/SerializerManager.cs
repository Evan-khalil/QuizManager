using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace DAL
{
    public class SerializerManager : ISerializerManager
    {
        public static Dictionary<List<QuizItem>, ObservableCollection<Course>> keyValuePairs;

        //Serialize a dictionary of list<quizItem> and observalbleCollection<course> to a file.
        public bool Serialize(string targetFile)
        {
            keyValuePairs = new Dictionary<List<QuizItem>, ObservableCollection<Course>>
            {
                { QuizManager.QuizItems, CourseManager.courses }
            };
            FileStream fileStream = null;
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                fileStream = new FileStream(targetFile, FileMode.Create);
                binaryFormatter.Serialize(fileStream, keyValuePairs);
            }
            catch (SerializationException)
            {
                return false;
            }
            finally
            {
                fileStream.Close();
            }
            return true;
        }

        //Deserialize a file to dictionary of list<quizItem> and observalbleCollection<course>.
        public void Deserialize(string targetFile)
        {
            keyValuePairs = new Dictionary<List<QuizItem>, ObservableCollection<Course>>();
            FileStream fileStream = null;
            try
            {
                if (File.Exists(targetFile))
                {
                    fileStream = new FileStream(targetFile, FileMode.Open);
                    BinaryFormatter b = new BinaryFormatter();
                    if (new FileInfo(targetFile).Length > 0)
                    {
                        keyValuePairs = (Dictionary<List<QuizItem>, ObservableCollection<Course>>)b.Deserialize(fileStream);
                        foreach (KeyValuePair<List<QuizItem>, ObservableCollection<Course>> item in keyValuePairs)
                        {
                            QuizManager.QuizItems = item.Key;
                            CourseManager.courses = item.Value;
                        }

                    }
                }
            }
            catch (SerializationException)
            {
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
        }
    }
}
