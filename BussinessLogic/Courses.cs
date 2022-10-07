using DAL;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BussinessLogic
{
    public class Courses
    {
        private readonly ICourseManager _courseManager = new CourseManager();

        //Add Course
        public void AddCourse(TextBox idTextBox, TextBox nameTextBox, TextBox modelsTextBox, ListBox listBox)
        {
            if (_courseManager.AlreadyExists(idTextBox.Text) == false)
            {
                ShowMessage("Course id already exits");
            }
            else
            {
                _courseManager.AddCourse(idTextBox.Text, nameTextBox.Text, modelsTextBox.Text.Split(new[] { ';' }).ToList().Where(x => !string.IsNullOrWhiteSpace(x)).ToList());
                ShowMessage("Added");
                FillListBox(listBox, _courseManager.GetCourses());
            }
        }

        //Course selection changed
        public void CourseSelectionChanged(ListBox courseListBox, ListBox modelsListBox)
        {
            Course course = courseListBox.SelectedItem as Course;
            if (course != null)
            {
                FillListBox(modelsListBox, course.Modules);
            }
        }

        //Remove Course
        public void RemoveCourse(ListBox courseListBox, ListBox modelListBox)
        {
            _courseManager.RemoveCourse(courseListBox.SelectedItem as Course);
            FillListBox(courseListBox, _courseManager.GetCourses());
            FillListBox(modelListBox, null);
            ShowMessage("Removed");
        }

        public Action<ListBox, dynamic> FillListBox = (listBox, itemSource) => listBox.ItemsSource = itemSource;
        public Action<string> ShowMessage = (msg) => MessageBox.Show(msg);
    }
}
