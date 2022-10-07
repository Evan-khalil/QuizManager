using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DAL
{

    public class CourseManager : ICourseManager
    {


        public static ObservableCollection<Course> courses = new ObservableCollection<Course>();

        public CourseManager()
        {
        }

        public bool AlreadyExists(string id)
        {
            bool Added = false;

            if (courses.Count > 0)
            {
                foreach (Course item in courses)
                {
                    if (item.Id.Equals(id))
                    {
                        Added = false;
                    }
                    else
                    {
                        Added = true;
                    }
                }
            }
            else
            {
                Added = true;
            }
            return Added;
        }

        //Add a new Course.
        public void AddCourse(string id, string name, List<string> models)
        {
            Course Course = new Course
            {
                Id = id,
                Name = name,
                Modules = models
            };
            courses.Add(Course);
        }

        //Get all courses
        public ObservableCollection<Course> GetCourses()
        {
            return courses;
        }

        //Remove course
        public void RemoveCourse(Course course)
        {
            courses.Remove(courses.FirstOrDefault(x => x == course));
        }
    }
}
