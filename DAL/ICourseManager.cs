using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DAL
{
    public interface ICourseManager
    {
        void AddCourse(string id, string name, List<string> models);
        void RemoveCourse(Course course);
        ObservableCollection<Course> GetCourses();
        bool AlreadyExists(string id);
    }
}
