using Perculus.XSDK.Extensions;
using Perculus.XSDK.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Perculus.XSDK.Components
{
    public class Sessions : PerculusEndpoint
    {
        public Sessions(PerculusOptions options) : base(options) { }

        /// <summary>
        /// Get session by session id
        /// </summary>
        /// <param name="sessionId">Session id is a globally unique identifier (GUID).</param>
        /// <returns>A session view object and error info if error occurs</returns>
        public (SessionView session, ApiErrorResponse error) GetSession(string sessionId)
        {
            if (String.IsNullOrEmpty(sessionId))
            {
                throw new ArgumentNullException(nameof(sessionId));
            }

            var request = HttpWebClient.CreateWebRequest("GET", BuildRoute($"session/{sessionId}"));
            var response = HttpWebClient.SendWebRequest(request);
            SessionView sessionView = null;
            ApiErrorResponse error = null;

            if (response != null)
            {
                string result = HttpWebClient.GetResponseBody(response);
                if (response.StatusCode == HttpStatusCode.OK)
                    sessionView = result.ToObject<SessionView>();
                else
                    error = response.ToErrorResponse();
            }

            return (sessionView, error);
        }

        /// <summary>
        /// Creates a session 
        /// </summary>
        /// <param name="session">A session view object which holds session data</param>
        /// <returns>The created session object is returned. If an error occurs, error information is returned as well.</returns>
        public (SessionView session, ApiErrorResponse error) CreateSession(SessionView session)
        {
            if (session is null)
            {
                throw new ArgumentNullException(nameof(session));
            }

            var request = HttpWebClient.CreateWebRequest("POST", BuildRoute("session"));
            var response = HttpWebClient.SendWebRequest(request, session);
            SessionView sessionView = null;
            ApiErrorResponse error = null;

            if (response != null)
            {
                string result = HttpWebClient.GetResponseBody(response);
                if (response.StatusCode == HttpStatusCode.Created)
                    sessionView = result.ToObject<SessionView>();
                else
                    error = response.ToErrorResponse();
            }

            return (sessionView, error);
        }

        /// <summary>
        /// Updates a session
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public (SessionView session, ApiErrorResponse error) UpdateSession(SessionView session)
        {
            if (session is null)
            {
                throw new ArgumentNullException(nameof(session));
            }

            var request = HttpWebClient.CreateWebRequest("PUT", BuildRoute($"session/{session.session_id}"));
            var response = HttpWebClient.SendWebRequest(request, session);
            SessionView sessionView = null;
            ApiErrorResponse error = null;

            if (response != null)
            {
                string result = HttpWebClient.GetResponseBody(response);
                if (response.StatusCode == HttpStatusCode.OK)
                    sessionView = result.ToObject<SessionView>();
                else
                    error = response.ToErrorResponse();
            }

            return (sessionView, error);
        }

        /// <summary>
        /// Deletes a session by session id
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public (bool success, ApiErrorResponse error) DeleteBySessionId(string sessionId)
        {
            if (String.IsNullOrEmpty(sessionId))
            {
                throw new ArgumentNullException(nameof(sessionId));
            }

            var request = HttpWebClient.CreateWebRequest("DELETE", BuildRoute($"session/{sessionId}"));
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
    }
}
