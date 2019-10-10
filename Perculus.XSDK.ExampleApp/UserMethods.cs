using Perculus.XSDK.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Perculus.XSDK.ExampleApp
{
    internal class UserMethods
    {
        public static UserView GetUserById(string userId)
        {
            var perculus = Common.CreatePerculusClient();
            (UserView user, ApiErrorResponse error) = perculus.Users.GetUserById(userId);

            if (error != null)
            {
                Common.HandleErrorResponse(error);
            }

            return user;
        }

        public static UserView GetUserByUsername(string username)
        {
            var perculus = Common.CreatePerculusClient();
            (UserView user, ApiErrorResponse error) = perculus.Users.GetUserByUsername(username);

            if (error != null)
            {
                Common.HandleErrorResponse(error);
            }

            return user;
        }

        public static List<UserView> SearchUsers(UserFilter filter)
        {
            var perculus = Common.CreatePerculusClient();
            (List<UserView> users, ApiErrorResponse error) = perculus.Users.SearchUsers(filter);

            if (error != null)
            {
                Common.HandleErrorResponse(error);
            }

            return users;
        }


        public static string CreateUser(string email, string username)
        {
            var perculus = Common.CreatePerculusClient();

            Models.PostUserView model = new Models.PostUserView
            {
                email = email,
                username = username,
                lang = "tr-TR",
                name = "Test",
                surname = "User",
                expires_at = DateTime.Now.AddDays(5),
                password = "password",
                role = "u",
                active = true
            };

            ApiErrorResponse error = null;
            UserView user = null;
            (user, error) = perculus.Users.CreateUser(model);
        
            if (error != null)
            {
                Common.HandleErrorResponse(error);
            }

            if (user != null && !String.IsNullOrEmpty(user.user_id))
            {
                return user.user_id;
            }

            return null;
        }


        public static string UpdateUser(string userId, PostUserView model = null)
        {
            var perculus = Common.CreatePerculusClient();
            if (model is null)
            {
                model = new Models.PostUserView
                {
                    name = "New Name",
                    email = "user-" + DateTimeOffset.UtcNow.ToUnixTimeSeconds() + "@address.com",
                    lang = "tr-TR",
                    surname = "surname",
                    username = "user-" + DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    expires_at = DateTime.Now.AddDays(5),
                    password = "password",
                    role = "u",
                    active = true
                };
            }
            else
            {
                model.name = "Test New";
                model.surname = "Newsurname";
                model.role = "a";
            }

            ApiErrorResponse error = null;
            UserView user = null;
            (user, error) = perculus.Users.UpdateUser(userId, model);

            if (error != null)
            {
                Common.HandleErrorResponse(error);
            }

            if (user != null && !String.IsNullOrEmpty(user.user_id))
            {
                return user.user_id;
            }

            return null;
        }

        public static bool DeleteUser(string userId)
        {
            var perculus = Common.CreatePerculusClient();
            var (success, error) = perculus.Users.DeleteByUserId(userId);

            if (success)
            {
                return true;
            }
            if (error != null)
            {
                Common.HandleErrorResponse(error);
            }

            return false;
        }

        public static bool ChangeUserPassword(string userId, string password)
        {
            var perculus = Common.CreatePerculusClient();
            var (success, error) = perculus.Users.ChangeUserPassword(userId, password);

            if (!success && error != null)
            {
                Common.HandleErrorResponse(error);
            }

            return success;
        }
    }
}
