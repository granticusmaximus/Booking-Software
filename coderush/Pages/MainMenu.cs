using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Pages
{
    public static class MainModule
    {
        public const string UserRole = "USER & ROLE";
        public const string Booking = "BOOKING";
        public const string Config = "CONFIG";
        public const string Agenda = "AGENDA";
        public const string Roster = "ROSTER";
        public const string Dashboard = "DASHBOARD";
    }

    public static class MainMenu
    {
        

        public static class User
        {
            public const string PageName = "User";
            public const string RoleName = "User";
            public const string Path = "/UserRole/Index";
            public const string ControllerName = "UserRole";
            public const string ActionName = "Index";
            public const string Module = MainModule.UserRole;
        }

        public static class ChangePassword
        {
            public const string PageName = "Change Password";
            public const string RoleName = "Change Password";
            public const string Path = "/UserRole/ChangePassword";
            public const string ControllerName = "UserRole";
            public const string ActionName = "ChangePassword";
            public const string Module = MainModule.UserRole;
        }

        public static class Role
        {
            public const string PageName = "Role";
            public const string RoleName = "Role";
            public const string Path = "/UserRole/Role";
            public const string ControllerName = "UserRole";
            public const string ActionName = "Role";
            public const string Module = MainModule.UserRole;
        }

        public static class ChangeRole
        {
            public const string PageName = "Change Role";
            public const string RoleName = "Change Role";
            public const string Path = "/UserRole/ChangeRole";
            public const string ControllerName = "UserRole";
            public const string ActionName = "ChangeRole";
            public const string Module = MainModule.UserRole;
        }

        public static class BookGeneral
        {
            public const string PageName = "Book General";
            public const string RoleName = "Book General";
            public const string Path = "/BookGeneral/Index";
            public const string ControllerName = "BookGeneral";
            public const string ActionName = "Index";
            public const string Module = MainModule.Booking;
        }

        public static class BookMeetingRoom
        {
            public const string PageName = "Book Meeting Room";
            public const string RoleName = "Book Meeting Room";
            public const string Path = "/BookMeetingRoom/Index";
            public const string ControllerName = "BookMeetingRoom";
            public const string ActionName = "Index";
            public const string Module = MainModule.Booking;
        }

        public static class BookCar
        {
            public const string PageName = "Book Car";
            public const string RoleName = "Book Car";
            public const string Path = "/BookCar/Index";
            public const string ControllerName = "BookCar";
            public const string ActionName = "Index";
            public const string Module = MainModule.Booking;
        }

        public static class MeetingRoom
        {
            public const string PageName = "Meeting Room";
            public const string RoleName = "Meeting Room";
            public const string Path = "/MeetingRoom/Index";
            public const string ControllerName = "MeetingRoom";
            public const string ActionName = "Index";
            public const string Module = MainModule.Config;
        }

        public static class Car
        {
            public const string PageName = "Car";
            public const string RoleName = "Car";
            public const string Path = "/Car/Index";
            public const string ControllerName = "Car";
            public const string ActionName = "Index";
            public const string Module = MainModule.Config;
        }

        public static class General
        {
            public const string PageName = "General";
            public const string RoleName = "General";
            public const string Path = "/General/Index";
            public const string ControllerName = "General";
            public const string ActionName = "Index";
            public const string Module = MainModule.Config;
        }

        public static class MyAgenda
        {
            public const string PageName = "My Personal Agenda";
            public const string RoleName = "MyAgenda";
            public const string Path = "/MyAgenda/Index";
            public const string ControllerName = "MyAgenda";
            public const string ActionName = "Index";
            public const string Module = MainModule.Agenda;
        }

        public static class RosterMeetingRoom
        {
            public const string PageName = "Roster Meeting Room";
            public const string RoleName = "Roster Meeting Room";
            public const string Path = "/RosterMeetingRoom/Index";
            public const string ControllerName = "RosterMeetingRoom";
            public const string ActionName = "Index";
            public const string Module = MainModule.Roster;
        }

        public static class RosterCar
        {
            public const string PageName = "Roster Car";
            public const string RoleName = "Roster Car";
            public const string Path = "/RosterCar/Index";
            public const string ControllerName = "RosterCar";
            public const string ActionName = "Index";
            public const string Module = MainModule.Roster;
        }

        public static class RosterGeneral
        {
            public const string PageName = "Roster General";
            public const string RoleName = "Roster General";
            public const string Path = "/RosterGeneral/Index";
            public const string ControllerName = "RosterGeneral";
            public const string ActionName = "Index";
            public const string Module = MainModule.Roster;
        }

        public static class DashboardMain
        {
            public const string PageName = "Dashboard Main";
            public const string RoleName = "Dashboard Main";
            public const string Path = "/Dashboard/Index";
            public const string ControllerName = "Dashboard";
            public const string ActionName = "Index";
            public const string Module = MainModule.Dashboard;
        }

    }
}
