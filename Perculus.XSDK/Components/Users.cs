using Perculus.XSDK.Extensions;
using Perculus.XSDK.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Perculus.XSDK.Components
{
    public class Users : PerculusEndpoint
    {
        public Users(PerculusOptions options) : base(options) { }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public (UserView user, ApiErrorResponse error) CreateUser(UserView user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var request = HttpWebClient.CreateWebRequest("POST", BuildRoute("user"));
            var response = HttpWebClient.SendWebRequest(request, user);
            UserView userView = null;
            ApiErrorResponse error = null;

            if (response != null)
            {
                string result = HttpWebClient.GetResponseBody(response);
                if (response.StatusCode == HttpStatusCode.Created)
                    userView = result.ToObject<UserView>();
                else
                    error = response.ToErrorResponse();
            }

            return (userView, error);
        }

        /// <summary>
        /// Updates a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public (UserView user, ApiErrorResponse error) UpdateUser(UserView user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var request = HttpWebClient.CreateWebRequest("PUT", BuildRoute($"user/{user.user_id}"));
            var response = HttpWebClient.SendWebRequest(request, user);
            UserView userView = null;
            ApiErrorResponse error = null;

            if (response != null)
            {
                string result = HttpWebClient.GetResponseBody(response);
                if (response.StatusCode == HttpStatusCode.OK)
                    userView = result.ToObject<UserView>();
                else
                    error = response.ToErrorResponse();
            }

            return (userView, error);
        }

        /// <summary>
        /// Deletes a user by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public (bool success, ApiErrorResponse error) DeleteByUserId(string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var request = HttpWebClient.CreateWebRequest("DELETE", BuildRoute($"user/{userId}"));
            var response = HttpWebClient.SendWebRequest(request);
            ApiErrorResponse error = null;
            bool success = false;

            if (response != null)
            {
                success = response.StatusCode == HttpStatusCode.NoContent;

                if (!success)
                {
                    error = response.ToErrorResponse();
                }
            }

            return (success, error);
        }

        /// <summary>
        /// Changes user's password.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public (bool success, ApiErrorResponse error) ChangeUserPassword(string userId, string password)
        {
            if (String.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (String.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }

            var request = HttpWebClient.CreateWebRequest("PUT", BuildRoute($"user/{userId}/password?password={password}"));
            var response = HttpWebClient.SendWebRequest(request);
            ApiErrorResponse error = null;
            bool success = false;

            if (response != null)
            {
                success = response.StatusCode == HttpStatusCode.NoContent;

                if (!success)
                {
                    error = response.ToErrorResponse();
                }
            }

            return (success, error);
        }

        /// <summary>
        /// Gets a user by user id.
        /// </summary>
        /// <param name="userId">User id is globally unique identifier (GUID).</param>
        /// <returns>A user view object and error info if error occurs</returns>
        public (UserView user, ApiErrorResponse error) GetUserById(string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var request = HttpWebClient.CreateWebRequest("GET", BuildRoute($"user/{userId}"));
            var response = HttpWebClient.SendWebRequest(request);
            UserView userView = null;
            ApiErrorResponse error = null;

            if (response != null)
            {
                string result = HttpWebClient.GetResponseBody(response);
                if (response.StatusCode == HttpStatusCode.OK)
                    userView = result.ToObject<UserView>();
                else
                    error = response.ToErrorResponse();
            }

            return (userView, error);
        }

        /// <summary>
        /// Gets a user by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public (UserView user, ApiErrorResponse error) GetUserByUsername(string username)
        {
            if (String.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(nameof(username));
            }

            var request = HttpWebClient.CreateWebRequest("GET", BuildRoute($"user/{username}"));
            var response = HttpWebClient.SendWebRequest(request);
            UserView userView = null;
            ApiErrorResponse error = null;

            if (response != null)
            {
                string result = HttpWebClient.GetResponseBody(response);
                if (response.StatusCode == HttpStatusCode.OK)
                    userView = result.ToObject<UserView>();
                else
                    error = response.ToErrorResponse();
            }

            return (userView, error);
        }
    }
}
