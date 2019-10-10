using Perculus.XSDK.Extensions;
using Perculus.XSDK.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using Perculus.XSDK.Models.PostViews;

namespace Perculus.XSDK.Components
{
    public class Attendees : PerculusEndpoint
    {
        public Attendees(PerculusOptions options) : base(options) { }

        public AttendeeView AddByUserId(string sessionId, string userId, string role, out ApiErrorResponse error)
        {
            if (string.IsNullOrEmpty(role)) role = "u";

            if (String.IsNullOrEmpty(sessionId))
            {
                throw new ArgumentNullException(nameof(sessionId));
            }

            if (String.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var attendee = new PostAttendeeView
            {
                user_id = userId,
                role = role
            };
            return AddAttendee(sessionId, attendee, out error);
        }

        /// <summary>
        /// Adds multiple users by userId and role data.
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="userIdWithRoles">List of users' userIds and roles</param>
        /// <returns></returns>
        public AttendeesPostResult AddMultipleByUserId(string sessionId, IEnumerable<UserIdRoleAttendee> userIdWithRoles, out ApiErrorResponse error)
        {
            return AddMultiple(sessionId, userIdWithRoles.Select(u => new PostAttendeeView { user_id = u.UserId, role = u.Role }), out error);
        }

        /// <summary>
        /// Add one attendee to a session
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="attendee"></param>
        /// <returns></returns>
        public AttendeeView AddAttendee(string sessionId, PostAttendeeView attendee, out ApiErrorResponse responseView)
        {
            if (String.IsNullOrEmpty(sessionId))
            {
                throw new ArgumentNullException(nameof(sessionId));
            }

            if (attendee is null)
            {
                throw new ArgumentNullException(nameof(attendee));
            }

            AttendeesPostResult result = AddMultiple(sessionId, new PostAttendeeView[] { attendee }, out ApiErrorResponse response);

            AttendeeView attendeeView = null;
            responseView = response;
            if (result != null)
            {
                attendeeView = result.approved != null && result.approved.Count > 0 ? result.approved.FirstOrDefault() : null;
                responseView = result.rejected != null && result.rejected.Count > 0 ? result.rejected.FirstOrDefault().State : null;
            }

            return attendeeView;
        }

        /// <summary>
        /// Adds multiple attendees to a session
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="attendees"></param>
        /// <returns></returns>
        public AttendeesPostResult AddMultiple(string sessionId, IEnumerable<PostAttendeeView> attendees, out ApiErrorResponse error)
        {
            if (String.IsNullOrEmpty(sessionId))
            {
                throw new ArgumentNullException(nameof(sessionId));
            }

            if (attendees is null || attendees.Count() == 0)
            {
                throw new ArgumentException(nameof(attendees));
            }

            var request = HttpWebClient.CreateWebRequest("POST", BuildRoute($"session/{sessionId}/attendees"));
            var response = HttpWebClient.SendWebRequest(request, attendees);

            AttendeesPostResult resultView = new AttendeesPostResult();
            error = null;

            if (response != null)
            {
                string result = HttpWebClient.GetResponseBody(response);
                if (response.StatusCode == HttpStatusCode.OK)
                    resultView = result.ToObject<AttendeesPostResult>();
                else
                    error = response.ToErrorResponse();

                if (error != null && error.Code == ApiErrorCode.MultipleErrors && error.Details != null)
                {
                    resultView.rejected = JsonConvert.DeserializeObject<List<MultipleResponseView>>(JsonConvert.SerializeObject(error.Details));
                }
            }

            return resultView;
        }

        /// <summary>
        /// Updates an attendee record by sessionId and attendance code
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="attendance_code"></param>
        /// <param name="attendee"></param>
        /// <returns></returns>
        public AttendeeView UpdateByAttendanceCode(string sessionId, string attendanceCode, PostAttendeeView attendee, out ApiErrorResponse error)
        {
            if (String.IsNullOrEmpty(sessionId))
            {
                throw new ArgumentNullException(nameof(sessionId));
            }

            if (attendee == null)
            {
                throw new ArgumentNullException(nameof(attendee));
            }

            if (String.IsNullOrEmpty(attendanceCode))
            {
                throw new ArgumentNullException(nameof(attendanceCode));
            }

            var request = HttpWebClient.CreateWebRequest("PUT", BuildRoute($"session/{sessionId}/attendee/{attendanceCode}"));
            var response = HttpWebClient.SendWebRequest(request, attendee);
            AttendeeView attendeeView = null;
            error = null;

            if (response != null)
            {
                string result = HttpWebClient.GetResponseBody(response);
                if (response.StatusCode == HttpStatusCode.OK)
                    attendeeView = result.ToObject<AttendeeView>();
                else
                    error = response.ToErrorResponse();
            }

            return attendeeView;
        }

        /// <summary>
        /// Updates an attendee record by sessionId and email
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="email"></param>
        /// <param name="attendee"></param>
        /// <returns></returns>
        public AttendeeView UpdateByEmail(string sessionId, string email, PostAttendeeView attendee, out ApiErrorResponse error)
        {
            if (String.IsNullOrEmpty(sessionId))
            {
                throw new ArgumentNullException(nameof(sessionId));
            }

            if (String.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            if (attendee is null)
            {
                throw new ArgumentNullException(nameof(attendee));
            }

            var request = HttpWebClient.CreateWebRequest("PUT", BuildRoute($"session/{sessionId}/attendee/{email}"));
            var response = HttpWebClient.SendWebRequest(request, attendee);
            AttendeeView attendeeView = null;
            error = null;

            if (response != null)
            {
                string result = HttpWebClient.GetResponseBody(response);
                if (response.StatusCode == HttpStatusCode.OK)
                    attendeeView = result.ToObject<AttendeeView>();
                else
                    error = response.ToErrorResponse();
            }

            return attendeeView;
        }

        /// <summary>
        /// Get attendee by session id and attendance code
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="attendanceCode"></param>
        /// <returns></returns>
        public AttendeeView GetByAttendanceCode(string sessionId, string attendanceCode, out ApiErrorResponse error)
        {
            if (String.IsNullOrEmpty(sessionId))
            {
                throw new ArgumentNullException(nameof(sessionId));
            }

            if (String.IsNullOrEmpty(attendanceCode))
            {
                throw new ArgumentNullException(nameof(attendanceCode));
            }

            var request = HttpWebClient.CreateWebRequest("GET", BuildRoute($"session/{sessionId}/attendee/{attendanceCode}"));
            var response = HttpWebClient.SendWebRequest(request);
            AttendeeView attendeeView = null;
            error = null;

            if (response != null)
            {
                string result = HttpWebClient.GetResponseBody(response);
                if (response.StatusCode == HttpStatusCode.OK)
                    attendeeView = result.ToObject<AttendeeView>();
                else
                    error = response.ToErrorResponse();
            }

            return attendeeView;
        }

        /// <summary>
        /// Search for attendees in a specefic session
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="attendanceCode"></param>
        /// <returns></returns>
        public List<AttendeeView> SearchAttendees(string sessionId, AttendeeFilter filter, out ApiErrorResponse error)
        {
            if (String.IsNullOrEmpty(sessionId))
            {
                throw new ArgumentNullException(nameof(sessionId));
            }

            var filterQuery = filter.ToQueryString();
            var request = HttpWebClient.CreateWebRequest("GET", BuildRoute($"session/{sessionId}/attendees?{filterQuery}"));
            var response = HttpWebClient.SendWebRequest(request);
            List<AttendeeView> attendeeViews = null;
            error = null;

            if (response != null)
            {
                string result = HttpWebClient.GetResponseBody(response);
                if (response.StatusCode == HttpStatusCode.OK)
                    attendeeViews = result.ToObject<List<AttendeeView>>();
                else
                    error = response.ToErrorResponse();
            }

            return attendeeViews;
        }

        /// <summary>
        /// Get attendee by session id and email
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public AttendeeView GetByEmail(string sessionId, string email, out ApiErrorResponse error)
        {
            if (String.IsNullOrEmpty(sessionId))
            {
                throw new ArgumentNullException(nameof(sessionId));
            }

            if (String.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            var request = HttpWebClient.CreateWebRequest("GET", BuildRoute($"session/{sessionId}/attendee/{email}"));
            var response = HttpWebClient.SendWebRequest(request);
            AttendeeView attendeeView = null;
            error = null;

            if (response != null)
            {
                string result = HttpWebClient.GetResponseBody(response);
                if (response.StatusCode == HttpStatusCode.OK)
                    attendeeView = result.ToObject<AttendeeView>();
                else
                    error = response.ToErrorResponse();
            }

            return attendeeView;
        }

        /// <summary>
        /// Delete attendee by session id and user identifier string. User identifier is an attendance code or the user id.
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="attendanceCodeOrUserId"></param>
        /// <returns></returns>
        public bool DeleteByAttendanceCodeOrUserId(string sessionId, string attendanceCodeOrUserId, out ApiErrorResponse error)
        {
            if (String.IsNullOrEmpty(sessionId))
            {
                throw new ArgumentNullException(nameof(sessionId));
            }

            if (String.IsNullOrEmpty(attendanceCodeOrUserId))
            {
                throw new ArgumentNullException(nameof(attendanceCodeOrUserId));
            }

            var request = HttpWebClient.CreateWebRequest("DELETE", BuildRoute($"session/{sessionId}/attendee/{attendanceCodeOrUserId}"));
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
        /// Deletes and attendee by session id and email
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool DeleteByEmail(string sessionId, string email, out ApiErrorResponse error)
        {
            if (String.IsNullOrEmpty(sessionId))
            {
                throw new ArgumentNullException(nameof(sessionId));
            }

            if (String.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            var request = HttpWebClient.CreateWebRequest("DELETE", BuildRoute($"session/{sessionId}/attendee/{email}"));
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
