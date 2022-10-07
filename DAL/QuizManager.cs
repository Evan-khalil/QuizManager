using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{

    public class QuizManager : IQuizManager
    {
        public QuizItem QuizItem = new QuizItem();//QuizItem.
        public static List<QuizItem> QuizItems = new List<QuizItem>();//List of quizzes.
        private int id = 0;

        //Edit list of descriptions in a QuizItem.
        public void EditDescriptionStrings(QuizItem quizItem, List<string> listOfStrings)
        {
            quizItem.DescriptionStrings.Clear();
            quizItem.DescriptionStrings.AddRange(listOfStrings);
        }

        //Add a new QuizItem.
        public void AddQuizItem(string description)
        {
            foreach (QuizItem item in QuizItems)
            {
                if (item.Id == id)
                {
                    id++;
                }
            }
            QuizItem = new QuizItem
            {
                Id = id,
                DescriptionStrings = new List<string>(),
                LinkedTo = new Dictionary<string, List<string>>()
            };
            QuizItem.DescriptionStrings.Add(description);
            QuizItems.Add(QuizItem);
            id++;
        }

        //Remove QuizItem.
        public void RemoveQuizItem(QuizItem quizItem)
        {
            QuizItems.Remove(QuizItems.FirstOrDefault(x => x.Id == quizItem.Id));
        }

        //Get all quizzes.
        public Dictionary<int, string> GetQuizItems()
        {
            Dictionary<int, string> Strings = new Dictionary<int, string>();
            QuizItems.ForEach(x => Strings.Add(x.Id, string.Join(" ", x.DescriptionStrings)));
            return Strings;
        }

        //Get QuizItem by id.
        public QuizItem GetById(int id)
        {
            return QuizItems.Where(x => x.Id == id).FirstOrDefault();
        }

        //Link course and modules to QuizItem.
        public void Link(QuizItem quizItem, string course, List<string> listOfModules)
        {
            if (quizItem.LinkedTo.ContainsKey(course))
            {
                quizItem.LinkedTo[course].AddRange(listOfModules.Except(quizItem.LinkedTo[course]).ToList());
            }
            else
            {
                quizItem.LinkedTo.Add(course, listOfModules);
            }
        }

        //Get linked quiz items by course
        public Dictionary<int, string> GetLinkedByCourse(Course course)
        {
            if (GetLinkedItemsByCourse(course) != null)
            {
                Dictionary<int, string> quizItems = new Dictionary<int, string>();
                GetLinkedItemsByCourse(course).ForEach(x => quizItems.Add(x.Id, string.Join("", x.DescriptionStrings)));
                return quizItems;
            }
            else
            {
                return null;
            }
        }

        //Get linked quiz items by models
        public Dictionary<int, string> GetLinkedByModels(Course course, List<string> listOfModels)
        {
            Dictionary<int, string> quizItems = new Dictionary<int, string>();
            try
            {

                List<QuizItem> ModelList = GetLinkedItemsByCourse(course).Where(x => listOfModels.All(y => x.LinkedTo[course.Id].Contains(y))).ToList();
                ModelList.ForEach(x => quizItems.Add(x.Id, string.Join("", x.DescriptionStrings)));

            }
            catch (Exception)
            {

            }
            return quizItems;
        }

        //Get all quiz items by give course.
        public List<QuizItem> GetLinkedItemsByCourse(Course course)
        {
            if (course != null)
            {
                return QuizItems.Where(x => x.LinkedTo.ContainsKey(course.Id)).ToList();
            }
            else
            {
                return null;
            }
        }

        //Get all Quiz items
        public List<QuizItem> GetAll()
        {
            return QuizItems;
        }
    }
}
