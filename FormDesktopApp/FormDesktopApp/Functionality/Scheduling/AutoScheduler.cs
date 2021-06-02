using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;
using FormDesktopApp.Functionality.Administration;
using FormDesktopApp.Objects.Departments;
using FormDesktopApp.Objects.Persons;
using System.ComponentModel;

namespace FormDesktopApp.Functionality.Scheduling
{
    public class AutoScheduler
    {
        #region Fields
        private readonly ListCreator _listCreator = new ListCreator();
        private readonly DataAccess _dbAccess = new DataAccess();
        private readonly CultureInfo _curCultInfo = CultureInfo.CurrentCulture;

        private List<Person> forcedEmployeeList = new List<Person>();
        private List<int> weekNumList = new List<int>();
        private List<Schedule> scheduleList = new List<Schedule>();


        #region Email Required Fields
        private string txtTo, txtSubject, txtUserName, txtPassword, txtSmtp, txtMessage;
        private int txtPort;
        private bool sslValue = true;
        private NetworkCredential login;
        private SmtpClient client;
        private MailMessage msg;
        #endregion
        #endregion

        public void AutomateSchedule(Department dept, DateTime date)
        {
            //forcedEmployeeList.Clear();
            //weekNumList.Clear();
            //scheduleList.Clear();

            var rdm = new Random();
            var shiftList = _listCreator.ReturnShiftList(dept, date);
            var counter = shiftList.Count;
            for (var i = 0; i < counter; i++)
            {
                if (_dbAccess.GetScheduleWorkingEmployees(shiftList[i].ScheduleID, dept.Department_id) ==
                    _dbAccess.GetScheduleRequiredEmployees(shiftList[i].ScheduleID, dept.Department_id))
                {
                    shiftList.RemoveAt(i);
                    CounterReset(ref counter, ref i, shiftList.Count);
                }
                else
                {
                    var shift = shiftList[i].ShiftName;
                    var availableEmployees = _listCreator.ReturnContractedEmployees(dept, date, shift,shiftList[i]);
                    if (availableEmployees.Count == 0)
                    {
                        if (shiftList[i].WorkingEmployeeNumber == 0)
                        {
                            var forcedEmp = _listCreator.ReturnForcedContractedEmployees(dept, date, shift, shiftList[i]);
                            if (forcedEmp.Count > 0)
                            {
                                var weekNum = _curCultInfo.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                                var rdmChosen = rdm.Next(0, forcedEmp.Count - 1);
                                var tempEmp = forcedEmp[rdmChosen];
                                List<EmployeeSchedule> employeeSchedule = new List<EmployeeSchedule>();
                                forcedEmployeeList.Add(tempEmp);
                                weekNumList.Add(weekNum);
                                scheduleList.Add(shiftList[i]);

                                _dbAccess.AssignShiftToEmployee(tempEmp.EmployeeId, shiftList[i].ScheduleID, dept.Department_id,
                                    weekNum, shiftList[i].date, shiftList[i].ShiftName, shiftList[i].DayName.ToString().ToUpper());

                            }
                            else
                            {
                                var zeroHrEmp = _listCreator.ReturnZeroHourEmployees(dept, date, shift, shiftList[i]);
                                var weekNum = _curCultInfo.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                                var rdmChosen = rdm.Next(0, zeroHrEmp.Count - 1);
                                var tempEmp = zeroHrEmp[rdmChosen];
                                _dbAccess.AssignShiftToEmployee(tempEmp.EmployeeId, shiftList[i].ScheduleID, dept.Department_id,
                                    weekNum, shiftList[i].date, shiftList[i].ShiftName, shiftList[i].DayName.ToString().ToUpper());
                            }
                        }
                        else
                        {
                            var zeroHrEmp = _listCreator.ReturnZeroHourEmployees(dept, date, shift, shiftList[i]);
                            var weekNum = _curCultInfo.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                            var rdmChosen = rdm.Next(0, zeroHrEmp.Count - 1);
                            var tempEmp = zeroHrEmp[rdmChosen];
                            _dbAccess.AssignShiftToEmployee(tempEmp.EmployeeId, shiftList[i].ScheduleID, dept.Department_id,
                                weekNum, shiftList[i].date, shiftList[i].ShiftName, shiftList[i].DayName.ToString().ToUpper());
                        }
                    }
                    else
                    {
                        var weekNum = _curCultInfo.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                        var rdmChosen = rdm.Next(0, availableEmployees.Count - 1);
                        var tempEmp = availableEmployees[rdmChosen];
                        _dbAccess.AssignShiftToEmployee(tempEmp.EmployeeId, shiftList[i].ScheduleID, dept.Department_id,
                            weekNum, shiftList[i].date, shiftList[i].ShiftName, shiftList[i].DayName.ToString().ToUpper());
                    }
                }
                if(i+1 == counter)
                {
                    i = -1;
                }
            }
            SendEmailMethod();
        }


        public void SendEmailMethod()
        {
            for (var i = 0; i < forcedEmployeeList.Count; i++)
            {
                #region Mail Setup
                txtPort = 587;
                txtSmtp = "smtp.gmail.com";
                txtUserName = "goodheart.n994@gmail.com";
                txtPassword = "Gr3nz3n_2o2o";

                txtSubject = "New Work Day Added to Schedule";
                login = new NetworkCredential(txtUserName, txtPassword);
                client = new SmtpClient(txtSmtp);
                client.Port = txtPort;
                client.EnableSsl = sslValue;
                client.Credentials = login;

                msg = new MailMessage { From = new MailAddress(txtUserName) };
                msg.Subject = txtSubject;
                msg.BodyEncoding = Encoding.UTF8;
                msg.IsBodyHtml = true;
                msg.Priority = MailPriority.High;
                #endregion
                txtMessage = string.Format("<p>Dear {0} {1}</p>" +
                                           "<p>Please be advised that you have been assigned to a shift where you have opted out from or do not wish to work.</p>" +
                                           "<p>Please let the Admin know as soon as possible if you will be available for the shift, you have 48 hours.</p>" +
                                           "<p>Your shift Schedule looks something similar to:</p>" +
                                           "<p>Week Number: {2}, Date: {3}, Shift Name: {4}, Day: {5}</p>",
                    forcedEmployeeList[i].FamilyName, forcedEmployeeList[i].FirstName, weekNumList[i], scheduleList[i].date,
                    scheduleList[i].ShiftName, scheduleList[i].DayName.ToString());
                if (!string.IsNullOrWhiteSpace(forcedEmployeeList[i].Email))
                {
                    msg.To.Add(new MailAddress(forcedEmployeeList[i].Email));
                    msg.Body = txtMessage;
                    var userstate = "Sending...";
                    msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                    client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                    client.Send(msg);
                    //client.SendAsync(msg, userstate);
                }
            }
        }

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show(string.Format($"{e.UserState} send canceled"));
            }
            if (e.Error != null)
            {
                MessageBox.Show(string.Format($"{e.UserState} {e.Error}"));
            }
            else
            {
                MessageBox.Show("Your message has been sent");
            }
        }

        //Counter Reset is used for resetting the counter in for loops
        //Further it resets the index to the previous number to not skip over items
        #region Counter Reset
        private static void CounterReset(ref int counter, ref int index, int newCountValue)
        {
            if (counter < 0) throw new ArgumentOutOfRangeException(nameof(counter));
            if (index <= 0)
            {
                index = -1;
            }
            else
            {
                index -= 2;
            }
            counter = newCountValue;
        }
        #endregion
    }
}
