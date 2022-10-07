using DAL;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace BussinessLogic
{
    public class Quizzez
    {
        private readonly IQuizManager _quizManager = new QuizManager();
        private readonly ICourseManager _courseManager = new CourseManager();
        private readonly ISerializerManager _serializer = new SerializerManager();
        private readonly Courses _courses = new Courses();

        //Add QuizItem.
        public void AddQuizItem(TextBox textBox, ListBox listBox)
        {
            _quizManager.AddQuizItem(textBox.Text);
            _courses.FillListBox(listBox, _quizManager.GetQuizItems());
            _courses.ShowMessage("Added");
        }

        //Remove QuizItem.
        public void RemoveQuizItem(ListBox listBox)
        {
            _quizManager.RemoveQuizItem(_quizManager.GetById(((KeyValuePair<int, string>)listBox.SelectedItem).Key));
            _courses.FillListBox(listBox, _quizManager.GetQuizItems());
            _courses.ShowMessage("Removed");
        }

        //Edit QuizItem.
        public void EditDescriptions(TextBox textBox, ListBox listBox)
        {
            List<string> listOfStrings = new List<string>
            {
                textBox.Text
            };
            _quizManager.EditDescriptionStrings(_quizManager.GetById(((KeyValuePair<int, string>)listBox.SelectedItem).Key), listOfStrings);
            _courses.FillListBox(listBox, _quizManager.GetQuizItems());
            _courses.ShowMessage("Edited");
        }

        //Link quiz.
        public void Link(ListBox courseListBox, ListBox quizListBox, ListBox modelListBox)
        {
            Course course = courseListBox.SelectedItem as Course;
            _quizManager.Link(_quizManager.GetById(((KeyValuePair<int, string>)quizListBox.SelectedItem).Key), course.Id, modelListBox.SelectedItems.Cast<string>().ToList());
            _courses.ShowMessage("Linked");

        }

        //Get linked quizzes by course.
        public void GetLinkedByCourse(ListBox courseListBox, ListBox categoryListBox)
        {
            _courses.FillListBox(categoryListBox, _quizManager.GetLinkedByCourse(courseListBox.SelectedItem as Course));
        }

        //Get linked quiz items by model.
        public void GetLinkedByModel(ListBox categoryListBox, ListBox modelListBox, ListBox courseListBox)
        {
            _courses.FillListBox(categoryListBox, _quizManager.GetLinkedByModels(courseListBox.SelectedItem as Course, modelListBox.SelectedItems.Cast<string>().ToList()));
        }

        //Save.
        public void Save()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                bool result = _serializer.Serialize(saveFileDialog.FileName);
                string message = result ? "Saved!" : "Couldn't save";
                _courses.ShowMessage(message);
            }
        }

        //Open.
        public void Open(ListBox listBox, ListBox listBox1)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                _serializer.Deserialize(openFileDialog.FileName);
                if (!string.IsNullOrEmpty(openFileDialog.FileName))
                {
                    _courses.FillListBox(listBox1, _courseManager.GetCourses());
                    _courses.FillListBox(listBox, _quizManager.GetQuizItems());
                }
                else
                {

                }
            }
        }
    }
}
