using Perculus.XSDK.Extensions;
using Perculus.XSDK.Models;
using Perculus.XSDK.Models.PostViews;
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
        public SessionView GetSession(string sessionId, out ApiErrorResponse error)
        {
            if (String.IsNullOrEmpty(sessionId))
            {
                throw new ArgumentNullException(nameof(sessionId));
            }

            var request = HttpWebClient.CreateWebRequest("GET", BuildRoute($"session/{sessionId}"));
            var response = HttpWebClient.SendWebRequest(request);
            SessionView sessionView = null;
            error = null;
            if (response != null)
            {
                string result = HttpWebClient.GetResponseBody(response);
                if (response.StatusCode == HttpStatusCode.OK)
                    sessionView = result.ToObject<SessionView>();
                else
                    error = response.ToErrorResponse();
            }

            return sessionView;
        }

        /// <summary>
        /// Search Sessions using session filter
        /// </summary>
        /// <param name="filter">SessionFilter Model</param>
        /// <returns>A session view list object and error info if error occurs</returns>
        public List<SessionView> SearchSessions(SessionFilter filter, out ApiErrorResponse error)
        {
            string query = filter.ToQueryString();
            var request = HttpWebClient.CreateWebRequest("GET", BuildRoute($"session?{query}"));
            var response = HttpWebClient.SendWebRequest(request);
            List<SessionView> sessionsViews = null;
            error = null;

            if (response != null)
            {
                string result = HttpWebClient.GetResponseBody(response);
                if (response.StatusCode == HttpStatusCode.OK)
                    sessionsViews = result.ToObject<List<SessionView>>();
                else
                    error = response.ToErrorResponse();
            }

            return sessionsViews;
        }

        /// <summary>
        /// Creates a session 
        /// </summary>
        /// <param name="session">A session view object which holds session data</param>
        /// <returns>The created session object is returned. If an error occurs, error information is returned as well.</returns>
        public SessionView CreateSession(PostSessionView session, out ApiErrorResponse error)
        {
            if (session is null)
            {
                throw new ArgumentNullException(nameof(session));
            }

            var request = HttpWebClient.CreateWebRequest("POST", BuildRoute("session"));
            var response = HttpWebClient.SendWebRequest(request, session);
            SessionView sessionView = null;
            error = null;

            if (response != null)
            {
                string result = HttpWebClient.GetResponseBody(response);
                if (response.StatusCode == HttpStatusCode.Created)
                    sessionView = result.ToObject<SessionView>();
                else
                    error = response.ToErrorResponse();
            }

            return sessionView;
        }

        /// <summary>
        /// Updates a session
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public SessionView UpdateSession(SessionView session, out ApiErrorResponse error)
        {
            if (session is null)
            {
                throw new ArgumentNullException(nameof(session));
            }

            var request = HttpWebClient.CreateWebRequest("PUT", BuildRoute($"session/{session.session_id}"));
            var response = HttpWebClient.SendWebRequest(request, session);
            SessionView sessionView = null;
            error = null;

            if (response != null)
            {
                string result = HttpWebClient.GetResponseBody(response);
                if (response.StatusCode == HttpStatusCode.OK)
                    sessionView = result.ToObject<SessionView>();
                else
                    error = response.ToErrorResponse();
            }

            return sessionView;
        }

        /// <summary>
        /// Deletes a session by session id
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public bool DeleteBySessionId(string sessionId, out ApiErrorResponse error)
        {
            if (String.IsNullOrEmpty(sessionId))
            {
                throw new ArgumentNullException(nameof(sessionId));
            }

            var request = HttpWebClient.CreateWebRequest("DELETE", BuildRoute($"session/{sessionId}"));
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
    }
}
