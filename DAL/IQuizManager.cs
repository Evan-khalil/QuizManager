using System.Collections.Generic;

namespace DAL
{
    public interface IQuizManager
    {
        void EditDescriptionStrings(QuizItem quizItem, List<string> listOfStrings);
        void AddQuizItem(string text);
        void RemoveQuizItem(QuizItem quizItem);
        void Link(QuizItem quizItem, string course, List<string> listOfModules);
        Dictionary<int, string> GetQuizItems();
        QuizItem GetById(int id);
        Dictionary<int, string> GetLinkedByCourse(Course course);
        Dictionary<int, string> GetLinkedByModels(Course course, List<string> listOfModels);
    }
}
