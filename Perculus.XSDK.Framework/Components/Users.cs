using Perculus.XSDK.Extensions;
using Perculus.XSDK.Models;
using Perculus.XSDK.Models.PostViews;
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
        public UserView CreateUser(PostUserView user, out ApiErrorResponse error)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var request = HttpWebClient.CreateWebRequest("POST", BuildRoute("user"));
            var response = HttpWebClient.SendWebRequest(request, user);
            UserView userView = null;
            error = null;

            if (response != null)
            {
                string result = HttpWebClient.GetResponseBody(response);
                if (response.StatusCode == HttpStatusCode.Created)
                    userView = result.ToObject<UserView>();
                else
                    error = response.ToErrorResponse();
            }

            return userView;
        }

        /// <summary>
        /// Updates a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public UserView UpdateUser(UserView user, out ApiErrorResponse error)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var request = HttpWebClient.CreateWebRequest("PUT", BuildRoute($"user/{user.user_id}"));
            var response = HttpWebClient.SendWebRequest(request, user);
            UserView userView = null;
            error = null;

            if (response != null)
            {
                string result = HttpWebClient.GetResponseBody(response);
                if (response.StatusCode == HttpStatusCode.OK)
                    userView = result.ToObject<UserView>();
                else
                    error = response.ToErrorResponse();
            }

            return userView;
        }

        /// <summary>
        /// Deletes a user by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool DeleteByUserId(string userId, out ApiErrorResponse error)
        {
            if (String.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var request = HttpWebClient.CreateWebRequest("DELETE", BuildRoute($"user/{userId}"));
            var response = HttpWebClient.SendWebRequest(request);
            error = null;
            bool success = false;

            if (response != null)
            {
                success = response.StatusCode == HttpStatusCode.NoContent;

                if (!success)
                {
                    error = response.ToErrorResponse();
                }
            }

            return success;
        }

        /// <summary>
        /// Changes user's password.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ChangeUserPassword(string userId, string password, out ApiErrorResponse error)
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
            var response = HttpWebClient.SendWebRequest(request, password);
            error = null;
            bool success = false;

            if (response != null)
            {
                success = response.StatusCode == HttpStatusCode.NoContent;

                if (!success)
                {
                    error = response.ToErrorResponse();
                }
            }

            return success;
        }

        /// <summary>
        /// Search Users using user filter
        /// </summary>
        /// <param name="filter">UserFilter Model</param>
        /// <returns>A user view list object and error info if error occurs</returns>
        public List<UserView> SearchUsers(UserFilter filter, out ApiErrorResponse error)
        {
            var request = HttpWebClient.CreateWebRequest("GET", BuildRoute($"user?{filter.ToQueryString()}"));
            var response = HttpWebClient.SendWebRequest(request);
            List<UserView> userViews = null;
            error = null;

            if (response != null)
            {
                string result = HttpWebClient.GetResponseBody(response);
                if (response.StatusCode == HttpStatusCode.OK)
                    userViews = result.ToObject<List<UserView>>();
                else
                    error = response.ToErrorResponse();
            }

            return userViews;
        }

        /// <summary>
        /// Gets a user by user id.
        /// </summary>
        /// <param name="userId">User id is globally unique identifier (GUID).</param>
        /// <returns>A user view object and error info if error occurs</returns>
        public UserView GetUserById(string userId, out ApiErrorResponse error)
        {
            if (String.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var request = HttpWebClient.CreateWebRequest("GET", BuildRoute($"user/{userId}"));
            var response = HttpWebClient.SendWebRequest(request);
            UserView userView = null;
            error = null;

            if (response != null)
            {
                string result = HttpWebClient.GetResponseBody(response);
                if (response.StatusCode == HttpStatusCode.OK)
                    userView = result.ToObject<UserView>();
                else
                    error = response.ToErrorResponse();
            }

            return userView;
        }

        /// <summary>
        /// Gets a user by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public UserView GetUserByUsername(string username, out ApiErrorResponse error)
        {
            if (String.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(nameof(username));
            }

            var request = HttpWebClient.CreateWebRequest("GET", BuildRoute($"user/{username}"));
            var response = HttpWebClient.SendWebRequest(request);
            UserView userView = null;
            error = null;

            if (response != null)
            {
                string result = HttpWebClient.GetResponseBody(response);
                if (response.StatusCode == HttpStatusCode.OK)
                    userView = result.ToObject<UserView>();
                else
                    error = response.ToErrorResponse();
            }

            return userView;
        }
    }
}
