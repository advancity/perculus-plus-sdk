using Newtonsoft.Json;
using Perculus.XSDK.Models;
using Perculus.XSDK.Models.PostViews;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Perculus.XSDK.ExampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = Config.GetInstance();
            var userEmail = "test-user-" + Guid.NewGuid() + "@advancity.com.tr";
            var username = "test-user-" + Guid.NewGuid();
            var userId = String.Empty;
            var sessionId = String.Empty;

            try
            {
                #region USERS
                HEADER("Creating a user");
                userId = UserMethods.CreateUser(userEmail, username);

                if (!String.IsNullOrEmpty(userId))
                {
                    OK("Created user {0}", userId);
                }
                else
                {
                    ERROR("Could not create user");
                }

                if (!String.IsNullOrEmpty(userId))
                {
                    HEADER("Getting the user");
                    var user = UserMethods.GetUserById(userId);
                    if (user != null)
                    {
                        OK("Fetched user information by user id: {0}", JsonConvert.SerializeObject(user));
                        user = UserMethods.GetUserByUsername(user.username);
                        if (user != null)
                        {
                            OK("Fetched user information by username: {0}", JsonConvert.SerializeObject(user));
                        }

                        HEADER("Searching users");
                        List<UserView> users = UserMethods.SearchUsers(new UserFilter()
                        {
                            Role = "u",
                            PageNumber = 1,
                            PageSize = 3,
                            UserName = username,
                        });
                        OK("{0} Users Found {1}", users.Count.ToString(), JsonConvert.SerializeObject(users));

                        HEADER("Updating user");
                        PostUserView updatingUser = new PostUserView()
                        {
                            email = user.email,
                            expires_at = user.expires_at,
                            lang = user.lang,
                            login_allowed = user.login_allowed,
                            mobile = user.mobile,
                            name = user.name,
                            role = user.role,
                            surname = user.surname,
                            timezone = user.timezone,
                            timezone_offset = user.timezone_offset,
                            username = user.username
                        };
                        string updatedUserId = UserMethods.UpdateUser(userId, updatingUser);
                        OK("Updated user {0}", updatedUserId);
                    }
                    else
                    {
                        ERROR("User could not be fetched.");
                    }

                    HEADER("Changing password");
                    if (UserMethods.ChangeUserPassword(userId, "123"))
                    {
                        OK("Changed password");
                    }
                    else
                    {
                        ERROR("Password could not be changed.");
                    }

                }
                #endregion USERS

                #region SESSIONS & Attendees
                HEADER("Creating a session");
                sessionId = SessionMethods.CreateSession();

                if (!String.IsNullOrEmpty(sessionId))
                {
                    OK("Created session {0}", sessionId);
                }
                else
                {
                    ERROR("Could not create session");
                }

                if (!String.IsNullOrEmpty(sessionId))
                {
                    HEADER("Getting the session");
                    var session = SessionMethods.GetSession(sessionId);
                    if (session != null)
                    {
                        OK("Fetched session {0}. {1}{2}", session.session_id, Environment.NewLine, JsonConvert.SerializeObject(session));
                    }
                    else
                    {
                        ERROR("Session could not be updated");
                    }
                }

                if (!String.IsNullOrEmpty(sessionId))
                {
                    HEADER("Updating the session");
                    var session_id_updated = SessionMethods.UpdateSession(sessionId);
                    if (!String.IsNullOrEmpty(session_id_updated))
                    {
                        OK("Updated session {0}", session_id_updated);
                    }
                    else
                    {
                        ERROR("Session could not be updated");
                    }
                }

                HEADER("Search Sessions");

                var sessionFilter = new SessionFilter()
                {
                    //SessionName = "SessionName",
                    BeginDate = DateTime.Now.AddMinutes(-10),
                    PageNumber = 1,
                    PageSize = 10
                };

                var sessionsList = SessionMethods.ListSessions(sessionFilter);

                if (sessionsList != null && sessionsList.Count > 0)
                {
                    OK("{0} Sessions Found {1}", sessionsList.Count.ToString(), JsonConvert.SerializeObject(sessionsList));
                }
                else
                {
                    ERROR("Sessions could not be found");
                }
                if (!String.IsNullOrEmpty(userId) && !String.IsNullOrEmpty(sessionId))
                {
                    HEADER("Adding attendee by user id {0} to session {1}", userId, sessionId);
                    if (AttendeeMethods.AddAttendeeByUserId(sessionId, userId) != null)
                    {
                        OK("Created attendee");
                    }
                    else
                    {
                        ERROR("Could not create attendee");
                    }

                    HEADER("Searching for an attendee in session {0}", sessionId);
                    var attendeeSearchFilter = new AttendeeFilter()
                    {
                        UserId = userId,
                        Role = "a",
                        PageSize = 10,
                        PageNumber = 1
                    };

                    var attendees = AttendeeMethods.SearchAttendees(sessionId, attendeeSearchFilter);
                    if (attendees != null && attendees.Count > 0)
                    {
                        OK("{0} Attendees Found: {1}", attendees.Count.ToString(), JsonConvert.SerializeObject(attendees));
                    }
                    else
                    {
                        ERROR("Attendee Not Found");
                    }

                    HEADER("Deleting the attendee by user id");
                    if (AttendeeMethods.DeleteAttendee(sessionId, userId))
                    {
                        OK("Deleted attendee");
                    }
                    else
                    {
                        ERROR("Could not delete attendee");
                    }

                    HEADER("Adding attendee by user id {0} to session {1} using multiple adding method", userId, sessionId);
                    AttendeesPostResult testMultipleAttendeesByUserId = AttendeeMethods.AddMultipleAttendeesByUserId(sessionId, userId);
                    if (testMultipleAttendeesByUserId.approved != null && testMultipleAttendeesByUserId.approved.Count == 1)
                    {
                        OK("Created attendee");

                        var attendanceCode = testMultipleAttendeesByUserId.approved[0].attendance_code;
                        HEADER("Deleting the newly created attendee {0}", attendanceCode);
                        if (AttendeeMethods.DeleteAttendee(sessionId, attendanceCode))
                        {
                            OK("Deleted attendee");
                        }
                        else
                        {
                            ERROR("Could not delete attendee");
                        }

                    }
                    else
                    {
                        ERROR("Could not create attendee");
                    }
                }
                if (!String.IsNullOrEmpty(sessionId))
                {
                    HEADER("Adding external attendee (without user id)");
                    var newAttendee = new PostAttendeeView()
                    {
                        name = "Test Attendee Name",
                        surname = "Test Attendee Surname",
                        email = userEmail,
                        mobile = "05412345678",
                        role = "u",
                    };
                    AttendeeView testAddAttendee = AttendeeMethods.AddAttendee(sessionId, newAttendee);

                    if (testAddAttendee != null)
                    {

                        string joiningAddress = string.Format(config.APP_JOIN_URL_FORMAT, testAddAttendee.attendance_code);
                        OK("Created attendee -> Join address: {0}", joiningAddress);

                        HEADER("Deleting newly created attendee {0}", testAddAttendee.attendance_code);
                        if (AttendeeMethods.DeleteAttendee(sessionId, testAddAttendee.attendance_code))
                        {
                            OK("Deleted attendee");
                        }
                        else
                        {
                            ERROR("Could not delete attendee");
                        }
                    }
                }
                #endregion SESSIONS
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occured. Details: " + Environment.NewLine + ex.ToString());
            }

            try
            {
                #region Clean-UP
                HEADER("CLEAN-UP");
                if (!String.IsNullOrEmpty(userId))
                {
                    if (UserMethods.DeleteUser(userId))
                    {
                        OK("Deleted user {0}", userId);
                    }
                    else
                    {
                        ERROR("Could not delete user {0}", userId);
                    }
                }

                if (!String.IsNullOrEmpty(sessionId))
                {
                    if (SessionMethods.DeleteSession(sessionId))
                    {
                        OK("Deleted session {0}", sessionId);
                    }
                    else
                    {
                        ERROR("Could not delete session {0}", sessionId);
                    }
                }
                #endregion Clean-UP
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occured. Details: " + Environment.NewLine + ex.ToString());
            }

            HEADER("THE END");

            Console.ReadKey();

        }

        private static void WriteToConsole(string message, ConsoleColor foregroundColor, params string[] args)
        {
            var prevFColor = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(message, args);
            Console.ForegroundColor = prevFColor;
        }

        private static void OK(string message, params string[] args)
        {
            WriteToConsole("  OK: " + message, ConsoleColor.Green, args);
        }

        private static void ERROR(string message, params string[] args)
        {
            WriteToConsole("  ERROR: " + message, ConsoleColor.Yellow, args);
        }

        private static void HEADER(string message, params string[] args)
        {
            WriteToConsole(Environment.NewLine + Environment.NewLine + "*** " + message + " ***", ConsoleColor.Gray, args);
        }
    }
}