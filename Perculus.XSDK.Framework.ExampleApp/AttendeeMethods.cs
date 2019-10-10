using Perculus.XSDK.Models;
using Perculus.XSDK.Models.PostViews;
using System;
using System.Collections.Generic;
using System.Text;

namespace Perculus.XSDK.ExampleApp
{
    internal class AttendeeMethods
    {
        public static AttendeeView AddAttendeeByUserId(string sessionId, string userId)
        {
            Perculus perculus = Common.CreatePerculusClient();
            AttendeeView attendee = perculus.Attendees.AddByUserId(sessionId, userId, "a", out ApiErrorResponse error);

            if (error != null)
            {
                Common.HandleErrorResponse(error);
            }

            return attendee;
        }

        public static AttendeesPostResult AddMultipleAttendeesByUserId(string sessionId, string userId)
        {
            Perculus perculus = Common.CreatePerculusClient();
            List<UserIdRoleAttendee> userIdsWithRoles = new List<UserIdRoleAttendee>();

            userIdsWithRoles.Add(new UserIdRoleAttendee
            {
                UserId = userId,
                Role = "" //Default is "u". Possible values: e a u e+
            });

            AttendeesPostResult result = perculus.Attendees.AddMultipleByUserId(sessionId, userIdsWithRoles, out ApiErrorResponse error);

            if (error != null)
            {
                Common.HandleErrorResponse(error);
            }

            return result;
        }

        public static AttendeeView AddAttendee(string sessionId, PostAttendeeView newAttendee)
        {
            Perculus perculus = Common.CreatePerculusClient();

            AttendeeView attendee = perculus.Attendees.AddAttendee(sessionId, newAttendee, out ApiErrorResponse error);

            if (error != null)
            {
                Common.HandleErrorResponse(error);
            }

            return attendee;
        }

        public static bool DeleteAttendee(string sessionId, string userIdOrAttendanceCode)
        {
            Perculus perculus = Common.CreatePerculusClient();
            bool result = perculus.Attendees.DeleteByAttendanceCodeOrUserId(sessionId, userIdOrAttendanceCode, out ApiErrorResponse error);

            if (error != null)
            {
                Common.HandleErrorResponse(error);
            }

            return result;
        }

        public static List<AttendeeView> SearchAttendees(string sessionId, AttendeeFilter filter)
        {
            Perculus perculus = Common.CreatePerculusClient();
            List<AttendeeView> result = perculus.Attendees.SearchAttendees(sessionId, filter, out ApiErrorResponse error);

            if (error != null)
            {
                Common.HandleErrorResponse(error);
            }

            return result;
        }
    }
}
