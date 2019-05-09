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
            (AttendeeView attendee, ApiErrorResponse error) = perculus.Attendees.AddByUserId(sessionId, userId, "a");

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

            (AttendeesPostResult result, ApiErrorResponse error) = perculus.Attendees.AddMultipleByUserId(sessionId, userIdsWithRoles);

            if (error != null)
            {
                Common.HandleErrorResponse(error);
            }

            return result;
        }

        public static AttendeeView AddAttendee(string sessionId, PostAttendeeView newAttendee)
        {
            Perculus perculus = Common.CreatePerculusClient();

            (AttendeeView attendee, ApiErrorResponse error) = perculus.Attendees.AddAttendee(sessionId, newAttendee);

            if (error != null)
            {
                Common.HandleErrorResponse(error);
            }

            return attendee;
        }

        public static bool DeleteAttendee(string sessionId, string userIdOrAttendanceCode)
        {
            Perculus perculus = Common.CreatePerculusClient();
            (bool result, ApiErrorResponse error) = perculus.Attendees.DeleteByAttendanceCodeOrUserId(sessionId, userIdOrAttendanceCode);

            if (error != null)
            {
                Common.HandleErrorResponse(error);
            }

            return result;
        }

        public static List<AttendeeView> SearchAttendees(string sessionId, AttendeeFilter filter)
        {
            Perculus perculus = Common.CreatePerculusClient();
            (List<AttendeeView> result, ApiErrorResponse error) = perculus.Attendees.SearchAttendees(sessionId, filter);

            if (error != null)
            {
                Common.HandleErrorResponse(error);
            }

            return result;
        }
    }
}
