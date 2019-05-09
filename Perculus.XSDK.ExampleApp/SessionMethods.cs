﻿using Perculus.XSDK.Models;
using Perculus.XSDK.Models.PostViews;
using System;
using System.Collections.Generic;
using System.Text;

namespace Perculus.XSDK.ExampleApp
{
    internal class SessionMethods
    {
        public static string CreateSession()
        {
            var perculus = Common.CreatePerculusClient();

            PostSessionView session = new PostSessionView
            {
                name = "test session",
                lang = "tr-TR",
                description = "description",
                duration = 15,
                start_date = DateTimeOffset.Now,
                status = Models.Enum.SessionActiveStatus.Active
            };

            SessionView createdSession = null;
            ApiErrorResponse error = null;

            (createdSession, error) = perculus.Sessions.CreateSession(session);

            if (error != null)
            {
                Common.HandleErrorResponse(error);
            }

            if (createdSession != null && !String.IsNullOrEmpty(createdSession.session_id))
            {
                return createdSession.session_id;
            }

            return null;
        }

        public static SessionView GetSession(string sessionId)
        {
            var perculus = Common.CreatePerculusClient();

            (SessionView session, ApiErrorResponse error) = perculus.Sessions.GetSession(sessionId);

            if (error != null)
            {
                Common.HandleErrorResponse(error);
            }

            return session;
        }

        public static List<SessionView> ListSessions(SessionFilter filter)
        {
            var perculus = Common.CreatePerculusClient();

            (List<SessionView> sessions, ApiErrorResponse error) = perculus.Sessions.SearchSessions(filter);

            if (error != null)
            {
                Common.HandleErrorResponse(error);
            }

            return sessions;
        }

        public static string UpdateSession(string session_id)
        {
            var perculus = Common.CreatePerculusClient();

            Models.SessionView session = new Models.SessionView
            {
                name = "test updated session",
                lang = "tr-TR",
                session_id = session_id,
                description = "description 1",
                duration = 15,
                start_date = DateTimeOffset.Now,
                status = Models.Enum.ActiveStatus.Active
            };

            ApiErrorResponse error = null;
            (session, error) = perculus.Sessions.UpdateSession(session);

            if (error != null)
            {
                Common.HandleErrorResponse(error);
            }

            if (session != null && !String.IsNullOrEmpty(session.session_id))
            {
                return session.session_id;
            }

            return null;
        }

        public static bool DeleteSession(string sessionId)
        {
            var perculus = Common.CreatePerculusClient();
            var (result, error) = perculus.Sessions.DeleteBySessionId(sessionId);

            if (!result && error != null)
            {
                Common.HandleErrorResponse(error);
            }

            return result;
        }
    }
}
