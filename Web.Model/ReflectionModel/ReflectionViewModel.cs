using System;
using System.Collections.Generic;
using System.Reflection;

namespace Web.Model.ReflectionModel
{
    public class ReflectionSingleString
    {
        public string FirstName = "George";
    }

    /// <summary>
    /// This example will return a single string from ReflectionSingleString and will return George
    /// </summary>
    public static class ReturnSingleString
    {
        public static string ReturnReflectionSingleString(object className)
        {
            Type type = typeof(ReflectionSingleString);

            FieldInfo[] info = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            string firstName = "";
            foreach (var name in info)
            {
                firstName = (string)name.GetValue(className);
            }

            return firstName;
        }
    }

    public class UserDetails
    {
        public UserDetails(string name, int age)
        {
            Name = name;
            Age = age;
        }
        public string Name  { get; }
        public int Age      { get; }
    }

    public class ListOfUsers
    {
        public List<UserDetails> ReturnUserDetails()
        {
            List<UserDetails> users = new List<UserDetails>
            {
                new UserDetails("George",57),
                new UserDetails("Paul",44)
            };

            return users;
        }

        /// <summary>
        /// This example will return the list of users, from the ListOfUsers class
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public static string GetListOfUsers(UserDetails users)
        {
            Type type = users.GetType();
            PropertyInfo[] propertyInfos = type.GetProperties();
            string str = "";
            foreach (var prop in propertyInfos)
            {
                str += prop.Name + ": " + prop.GetValue(users) + ", ";
            }
            return str.Remove(str.Length - 2);
        }
    }
}
