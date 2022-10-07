using BussinessLogic;
using System;
using System.Windows;
using System.Windows.Controls;

namespace QuizManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Quizzez quizzez = new Quizzez();
        private readonly Courses courses = new Courses();
        public MainWindow()
        {
            InitializeComponent();
        }

        //Add new quiz.
        private void AddQuizItemBtn_Click(object sender, RoutedEventArgs e)
        {
            quizzez.AddQuizItem(QuizItemTextBox, QuizListBox);
            ClearTextBox(QuizItemTextBox);
        }

        //Remove quiz.
        private void RemoveQuiz(object sender, RoutedEventArgs e)
        {
            quizzez.RemoveQuizItem(QuizListBox);
        }

        //Edit quiz descriptions.
        private void EditDecriptions(object sender, RoutedEventArgs e)
        {
            quizzez.EditDescriptions(QuizItemTextBox, QuizListBox);
            ClearTextBox(QuizItemTextBox);
        }

        //Add new course.
        private void AddCourseBtnClick(object sender, RoutedEventArgs e)
        {
            courses.AddCourse(CourseIdTxtBox, CourseNameTxtBox, CourseModelsTxtBox, CourseListBox);
            ClearTextBox(CourseIdTxtBox);
            ClearTextBox(CourseNameTxtBox);
            ClearTextBox(CourseModelsTxtBox);
        }

        //CourseListBox selection changed.
        private void CourseListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            courses.CourseSelectionChanged(CourseListBox, ModelsListBox);
            quizzez.GetLinkedByCourse(CourseListBox, CategoryQuizListBox);
            IsItemSelected(CourseListBox, DeleteCourseBtn);
            CanLink(LinkModeCheckBox, CourseListBox, ModelsListBox, QuizListBox, LinkBtn);
        }

        //Delete course.
        private void DeleteCourseBtnClick(object sender, RoutedEventArgs e)
        {
            courses.RemoveCourse(CourseListBox, ModelsListBox);
        }

        //Link quiz item to course moments.
        private void LinkItemBtnClick(object sender, RoutedEventArgs e)
        {
            quizzez.Link(CourseListBox, QuizListBox, ModelsListBox);
        }

        //ModelListBox selection changed.
        private void ModelListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            quizzez.GetLinkedByModel(SubListBox, ModelsListBox, CourseListBox);
            CanLink(LinkModeCheckBox, CourseListBox, ModelsListBox, QuizListBox, LinkBtn);
        }

        //CourseIdTextBox Text Chnaged.
        private void CourseIdTextChnaged(object sender, TextChangedEventArgs e)
        {
            EnableButton(CourseIdTxtBox, CourseNameTxtBox, CourseModelsTxtBox, AddCourseBtn);
        }

        //CourseNameTextBox Text Chnaged.
        private void CourseNameTextChanged(object sender, TextChangedEventArgs e)
        {
            EnableButton(CourseIdTxtBox, CourseNameTxtBox, CourseModelsTxtBox, AddCourseBtn);
        }

        //CourseModelsTextBox Text Chnaged.
        private void CourseModelsTextChanged(object sender, TextChangedEventArgs e)
        {
            EnableButton(CourseIdTxtBox, CourseNameTxtBox, CourseModelsTxtBox, AddCourseBtn);
        }

        //QuizListBox Selection Changed.
        private void QuizListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CanLink(LinkModeCheckBox, CourseListBox, ModelsListBox, QuizListBox, LinkBtn);
            IsItemSelected(QuizListBox, RemoveQuizBtn);
            CanUpdate(QuizListBox, QuizItemTextBox, UpdateTextBtn);
        }

        //LinkModeCheckBox Checked.
        private void LinkModeChecked(object sender, RoutedEventArgs e)
        {
            CanLink(LinkModeCheckBox, CourseListBox, ModelsListBox, QuizListBox, LinkBtn);
        }

        //LinkModeCheckBox UnChecked.
        private void LinkModeUnchecked(object sender, RoutedEventArgs e)
        {
            CanLink(LinkModeCheckBox, CourseListBox, ModelsListBox, QuizListBox, LinkBtn);
        }

        //QuizItemTextBox Text Changed
        private void QuizItemTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            EnableButton(QuizItemTextBox, QuizItemTextBox, QuizItemTextBox, AddQuizItemBtn);
            CanUpdate(QuizListBox, QuizItemTextBox, UpdateTextBtn);
        }

        //Save quizitems and courses.
        private void SaveBtnClicked(object sender, RoutedEventArgs e)
        {
            quizzez.Save();
        }

        //Open a saved file.
        private void Open(object sender, RoutedEventArgs e)
        {
            quizzez.Open(QuizListBox, CourseListBox);
        }

        private readonly Action<TextBox> ClearTextBox = (textbox) => textbox.Clear();

        private readonly Action<TextBox, TextBox, TextBox, Button> EnableButton = (txt, txt1, txt2, btn) => { if (txt.Text != "" && txt1.Text != "" && txt2.Text != "") { btn.IsEnabled = true; } else { btn.IsEnabled = false; } };

        private readonly Action<ListBox, Button> IsItemSelected = (lstbox, btn) => { if (lstbox.SelectedItems.Count > 0) { btn.IsEnabled = true; } else { btn.IsEnabled = false; } };

        private readonly Action<CheckBox, ListBox, ListBox, ListBox, Button> CanLink = (checkBox, listBox, listBox1, listBox2, btn) => { if (checkBox.IsChecked == true && listBox.SelectedItems.Count > 0 && listBox1.SelectedItems.Count > 0 && listBox2.SelectedItems.Count > 0) { btn.IsEnabled = true; } else { btn.IsEnabled = false; } };

        private readonly Action<ListBox, TextBox, Button> CanUpdate = (listBox, textBox, btn) => { if (listBox.SelectedItems.Count > 0 && textBox.Text != "") { btn.IsEnabled = true; } else { btn.IsEnabled = false; } };
    }
}
