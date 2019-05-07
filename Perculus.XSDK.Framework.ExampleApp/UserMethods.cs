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
            UserView user = perculus.Users.GetUserById(userId, out ApiErrorResponse error);

            if (error != null)
            {
                Common.HandleErrorResponse(error);
            }

            return user;
        }

        public static UserView GetUserByUsername(string username)
        {
            var perculus = Common.CreatePerculusClient();
            UserView user = perculus.Users.GetUserByUsername(username, out ApiErrorResponse error);

            if (error != null)
            {
                Common.HandleErrorResponse(error);
            }

            return user;
        }

        public static List<UserView> SearchUsers(UserFilter filter)
        {
            var perculus = Common.CreatePerculusClient();
            List<UserView> users = perculus.Users.SearchUsers(filter, out ApiErrorResponse error);

            if (error != null)
            {
                Common.HandleErrorResponse(error);
            }

            return users;
        }


        public static string CreateUser(string email, string username)
        {
            var perculus = Common.CreatePerculusClient();

            Models.UserView user = new Models.UserView
            {
                email = email,
                username = username,
                lang = "tr-TR",
                name = "Test",
                surname = "User",
                expires_at = DateTime.Now.AddDays(5),
                password = "password",
                role = "u",
                status = Models.Enum.ActiveStatus.Active
            };

            user = perculus.Users.CreateUser(user, out ApiErrorResponse error);

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


        public static string UpdateUser(string userId, UserView user = null)
        {
            var perculus = Common.CreatePerculusClient();
            if (user is null)
            {
                user = new Models.UserView
                {
                    user_id = userId,
                    name = "New Name",
                    email = "user-" + DateTimeOffset.UtcNow.ToUnixTimeSeconds() + "@address.com",
                    lang = "tr-TR",
                    surname = "surname",
                    username = "user-" + DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    expires_at = DateTime.Now.AddDays(5),
                    password = "password",
                    role = "u",
                    status = Models.Enum.ActiveStatus.Active
                };
            }
            else
            {
                user.name = "Test New";
                user.surname = "Newsurname";
                user.role = "a";
            }

            user = perculus.Users.UpdateUser(user, out ApiErrorResponse error);

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
            var success = perculus.Users.DeleteByUserId(userId, out ApiErrorResponse error);

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
            var success = perculus.Users.ChangeUserPassword(userId, password, out ApiErrorResponse error);

            if (!success && error != null)
            {
                Common.HandleErrorResponse(error);
            }
            
            return success;
        }
    }
}
