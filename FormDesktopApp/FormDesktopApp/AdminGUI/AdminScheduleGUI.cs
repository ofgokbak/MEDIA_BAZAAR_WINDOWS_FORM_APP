using FormDesktopApp.Functionality.Administration;
using FormDesktopApp.Functionality.Scheduling;
using FormDesktopApp.Objects.Departments;
using FormDesktopApp.Objects.Enums;
using FormDesktopApp.Objects.Persons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormDesktopApp.AdminGUI
{
    public partial class AdminScheduleGUI : Form
    {
        AutoScheduler s = new AutoScheduler();
        Person currentuser = new Person();
        DataAccess db = new DataAccess();
        AppSystem appSystem = new AppSystem();
        List<Schedule> schedules = new List<Schedule>();
        List<EmployeeSchedule> employeeSchedules = new List<EmployeeSchedule>();
        Person person = new Person();
        Admission admission = new Admission();
        int count = 0;
        int toggle = 0;
        int toggle2 = 0;

        public AdminScheduleGUI(Person person)
        {
            InitializeComponent();
            currentuser = person;
            BtnSendEmail.Visible = false; //Button was used for testing purposes
        }

        private void AdminScheduleGUI_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            timetablegridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(48, 95, 147);
            timetablegridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            btnschedule.BackColor = Color.FromArgb(13, 38, 59);
            lbUsername.Text = currentuser.FirstName;
            tbupdateweek.Text = $"{weekNum.ToString()}";
            label17.Text = $"Week Number: {weekNum.ToString()}";
            //tabControl1.Appearance = TabAppearance.FlatButtons;
            //tabControl1.ItemSize = new Size(0, 1);
            //tabControl1.SizeMode = TabSizeMode.Fixed;
            timetablegridView.Visible = false;
            cbxshifts.DataSource = Enum.GetValues(typeof(Shift));

            cbxupdateshift.DataSource = Enum.GetValues(typeof(Shift));
            cbxremoveshift.DataSource = Enum.GetValues(typeof(Shift));

            cbxshifts.SelectedIndex = -1;
            employeeSchedules = db.GetAssignedEmployeeSchedulesByWeek(weekNum);
            
            LoadEmployeestotheList();
            LoadDepartments();


            cbxupdatedep.SelectedIndex = 0;
            cbxremovedep.SelectedIndex = 0;


            tbremoveweek.Text = weekNum.ToString();

            tbweek.Text = weekNum.ToString();
            lbweek.Text = $"Week Number: {weekNum.ToString()}";
            lbweek.Visible = true;
            //LoadSchedule(weekNum);
            updateCellsColor();
            cbxselectemployeedep.SelectedIndex = 0;
            cbxselectcontract.SelectedIndex = 0;
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void LoadEmployeestotheList()
        {
            foreach (var item in db.GetAllEmployees())
            {
                if(item.Status == true)
                {
                    lbemployee.Items.Add(item);
                }
                

            }
        }
        public void LoadDepartments()
        {
            List<Department> departments = db.GetDepartments();
            foreach (var item in departments)
            {
                cbxdepschedule.Items.Add(item);

            }
            foreach (var item in departments)
            {
                cbxassigndep.Items.Add(item);
            }
            foreach (var item in departments)
            {
                cbxupdatedep.Items.Add(item);
            }
            foreach (var item in departments)
            {
                cbxselectemployeedep.Items.Add(item);
            }
            cbxselectcontract.DataSource = Enum.GetValues(typeof(Contract));

            foreach (var item in departments)
            {
                cbxautodep.Items.Add(item);
            }
            foreach (var item in departments)
            {
                cbxremovedep.Items.Add(item);
            }

        }

        private void lbLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginGUI pg = new LoginGUI();
            pg.Show();
            this.Hide();
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            AdminEmployeeManagementGUI pg = new AdminEmployeeManagementGUI(currentuser);
            pg.Show();
            this.Hide();
        }

        public void LoadSchedule(int week,int depid)
        {
            timetablegridView.AutoGenerateColumns = false;
            schedules = db.GetSchedulesByWeek(week,depid);
            timetablegridView.Rows.Clear();
            timetablegridView.AutoGenerateColumns = false;

            if (schedules.Count != 0)
            {
                timetablegridView.Rows.Add
                ("Morning",
                $"{schedules[0].WorkingEmployeeNumber}/{schedules[0].RequiredEmployeeNumber}",
                $"{schedules[3].WorkingEmployeeNumber}/{schedules[3].RequiredEmployeeNumber}",
                $"{schedules[6].WorkingEmployeeNumber}/{schedules[6].RequiredEmployeeNumber}",
                $"{schedules[9].WorkingEmployeeNumber}/{schedules[9].RequiredEmployeeNumber}",
                $"{schedules[12].WorkingEmployeeNumber}/{schedules[12].RequiredEmployeeNumber}",
                $"{schedules[15].WorkingEmployeeNumber}/{schedules[15].RequiredEmployeeNumber}",
                $"{schedules[18].WorkingEmployeeNumber}/{schedules[18].RequiredEmployeeNumber}"
                );
                timetablegridView.Rows.Add
                    ("Afternoon",
                    $"{schedules[1].WorkingEmployeeNumber}/{schedules[1].RequiredEmployeeNumber}",
                    $"{schedules[4].WorkingEmployeeNumber}/{schedules[4].RequiredEmployeeNumber}",
                    $"{schedules[7].WorkingEmployeeNumber}/{schedules[7].RequiredEmployeeNumber}",
                    $"{schedules[10].WorkingEmployeeNumber}/{schedules[10].RequiredEmployeeNumber}",
                    $"{schedules[13].WorkingEmployeeNumber}/{schedules[13].RequiredEmployeeNumber}",
                    $"{schedules[16].WorkingEmployeeNumber}/{schedules[16].RequiredEmployeeNumber}",
                    $"{schedules[19].WorkingEmployeeNumber}/{schedules[19].RequiredEmployeeNumber}"
                    );
                timetablegridView.Rows.Add
                    ("Evening",
                    $"{schedules[2].WorkingEmployeeNumber}/{schedules[2].RequiredEmployeeNumber}",
                    $"{schedules[5].WorkingEmployeeNumber}/{schedules[5].RequiredEmployeeNumber}",
                    $"{schedules[8].WorkingEmployeeNumber}/{schedules[8].RequiredEmployeeNumber}",
                    $"{schedules[11].WorkingEmployeeNumber}/{schedules[11].RequiredEmployeeNumber}",
                    $"{schedules[14].WorkingEmployeeNumber}/{schedules[14].RequiredEmployeeNumber}",
                    $"{schedules[17].WorkingEmployeeNumber}/{schedules[17].RequiredEmployeeNumber}",
                    $"{schedules[20].WorkingEmployeeNumber}/{schedules[20].RequiredEmployeeNumber}"
                    );
            }
            else
            {
                timetablegridView.Rows.Add
              ("Morning",
              $"{"Empty"}",
           $"{"Empty"}",
              $"{"Empty"}",
              $"{"Empty"}",
              $"{"Empty"}",
             $"{"Empty"}",
              $"{"Empty"}"
              );
                timetablegridView.Rows.Add
                    ("Afternoon",
                   $"{"Empty"}",
           $"{"Empty"}",
              $"{"Empty"}",
              $"{"Empty"}",
              $"{"Empty"}",
             $"{"Empty"}",
              $"{"Empty"}"
                    );
                timetablegridView.Rows.Add
                    ("Evening",
                     $"{"Empty"}",
           $"{"Empty"}",
              $"{"Empty"}",
              $"{"Empty"}",
              $"{"Empty"}",
             $"{"Empty"}",
              $"{"Empty"}"
                    );
            }
            updateCellsColor();

            timetablegridView.AutoGenerateColumns = false;


            foreach (DataGridViewColumn column in timetablegridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }


            DataGridViewColumn columnwed = timetablegridView.Columns[3];
            DataGridViewColumn columnfri = timetablegridView.Columns[5];
            columnwed.Width = 120;
            columnfri.Width = 89;
            foreach (DataGridViewRow row in timetablegridView.Rows)
            {
                row.Height = 65;
            }
            foreach (DataGridViewColumn c in timetablegridView.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Arial", 16F, GraphicsUnit.Pixel);
            }

            timetablegridView.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 15F, FontStyle.Regular);
            int height = 0;
            foreach (DataGridViewRow row in timetablegridView.Rows)
            {
                height += row.Height;
            }
            height += timetablegridView.ColumnHeadersHeight;

            int width = 0;
            foreach (DataGridViewColumn col in timetablegridView.Columns)
            {
                width += col.Width;
            }
            width += timetablegridView.RowHeadersWidth;

            timetablegridView.ClientSize = new Size(width + 2, height + 2);

            timetablegridView.ClearSelection();
            timetablegridView.AutoGenerateColumns = false;
        }

        private void btndepartments_Click(object sender, EventArgs e)
        {
            AdminDepartmentsGUI pg = new AdminDepartmentsGUI(currentuser);
            pg.Show();
            this.Hide();
        }



        private void timetablegridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(dateTimePicker2.Value, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            if (timetablegridView.CurrentCell == timetablegridView[1, 0])
            {
                List<EmployeeSchedule> s = employeeSchedules.FindAll(x => (x.ScheduleWeek == weekNum) && (x.Date_of_work.DayOfWeek.ToString() == "Monday") && (x.Shift_name.ToString() == "MORNING"));
                DisplayEmployees(s);

            }
            else if (timetablegridView.CurrentCell == timetablegridView[2, 0])
            {

                List<EmployeeSchedule> s = employeeSchedules.FindAll(x => (x.ScheduleWeek == weekNum) && (x.Date_of_work.DayOfWeek.ToString() == "Tuesday") && (x.Shift_name.ToString() == "MORNING"));
                DisplayEmployees(s);
            }
            else if (timetablegridView.CurrentCell == timetablegridView[3, 0])
            {

                List<EmployeeSchedule> s = employeeSchedules.FindAll(x => (x.ScheduleWeek == weekNum) && (x.Date_of_work.DayOfWeek.ToString() == "Wednesday") && (x.Shift_name.ToString() == "MORNING"));
                DisplayEmployees(s);
            }

            else if (timetablegridView.CurrentCell == timetablegridView[4, 0])
            {

                List<EmployeeSchedule> s = employeeSchedules.FindAll(x => (x.ScheduleWeek == weekNum) && (x.Date_of_work.DayOfWeek.ToString() == "Thursday") && (x.Shift_name.ToString() == "MORNING"));
                DisplayEmployees(s);
            }

            else if (timetablegridView.CurrentCell == timetablegridView[5, 0])
            {

                List<EmployeeSchedule> s = employeeSchedules.FindAll(x => (x.ScheduleWeek == weekNum) && (x.Date_of_work.DayOfWeek.ToString() == "Friday") && (x.Shift_name.ToString() == "MORNING"));
                DisplayEmployees(s);
            }

            else if (timetablegridView.CurrentCell == timetablegridView[6, 0])
            {

                List<EmployeeSchedule> s = employeeSchedules.FindAll(x => (x.ScheduleWeek == weekNum) && (x.Date_of_work.DayOfWeek.ToString() == "Saturday") && (x.Shift_name.ToString() == "MORNING"));
                DisplayEmployees(s);
            }
            else if (timetablegridView.CurrentCell == timetablegridView[7, 0])
            {

                List<EmployeeSchedule> s = employeeSchedules.FindAll(x => (x.ScheduleWeek == weekNum) && (x.Date_of_work.DayOfWeek.ToString() == "Sunday") && (x.Shift_name.ToString() == "MORNING"));
                DisplayEmployees(s);
            }
            if (timetablegridView.CurrentCell == timetablegridView[1, 1])
            {

                List<EmployeeSchedule> s = employeeSchedules.FindAll(x => (x.ScheduleWeek == weekNum) && (x.Date_of_work.DayOfWeek.ToString() == "Monday") && (x.Shift_name.ToString() == "AFTERNOON"));
                DisplayEmployees(s);
            }
            else if (timetablegridView.CurrentCell == timetablegridView[2, 1])
            {

                List<EmployeeSchedule> s = employeeSchedules.FindAll(x => (x.ScheduleWeek == weekNum) && (x.Date_of_work.DayOfWeek.ToString() == "Tuesday") && (x.Shift_name.ToString() == "AFTERNOON"));
                DisplayEmployees(s);
            }
            else if (timetablegridView.CurrentCell == timetablegridView[3, 1])
            {

                List<EmployeeSchedule> s = employeeSchedules.FindAll(x => (x.ScheduleWeek == weekNum) && (x.Date_of_work.DayOfWeek.ToString() == "Wednesday") && (x.Shift_name.ToString() == "AFTERNOON"));
                DisplayEmployees(s);
            }

            else if (timetablegridView.CurrentCell == timetablegridView[4, 1])
            {

                List<EmployeeSchedule> s = employeeSchedules.FindAll(x => (x.ScheduleWeek == weekNum) && (x.Date_of_work.DayOfWeek.ToString() == "Thursday") && (x.Shift_name.ToString() == "AFTERNOON"));
                DisplayEmployees(s);
            }

            else if (timetablegridView.CurrentCell == timetablegridView[5, 1])
            {

                List<EmployeeSchedule> s = employeeSchedules.FindAll(x => (x.ScheduleWeek == weekNum) && (x.Date_of_work.DayOfWeek.ToString() == "Friday") && (x.Shift_name.ToString() == "AFTERNOON"));
                DisplayEmployees(s);
            }

            else if (timetablegridView.CurrentCell == timetablegridView[6, 1])
            {

                List<EmployeeSchedule> s = employeeSchedules.FindAll(x => (x.ScheduleWeek == weekNum) && (x.Date_of_work.DayOfWeek.ToString() == "Saturday") && (x.Shift_name.ToString() == "AFTERNOON"));
                DisplayEmployees(s);
            }
            else if (timetablegridView.CurrentCell == timetablegridView[7, 1])
            {
                List<EmployeeSchedule> s = employeeSchedules.FindAll(x => (x.ScheduleWeek == weekNum) && (x.Date_of_work.DayOfWeek.ToString() == "Sunday") && (x.Shift_name.ToString() == "AFTERNOON"));
                DisplayEmployees(s);
            }
            if (timetablegridView.CurrentCell == timetablegridView[1, 2])
            {
                List<EmployeeSchedule> s = employeeSchedules.FindAll(x => (x.ScheduleWeek == weekNum) && (x.Date_of_work.DayOfWeek.ToString() == "Monday") && (x.Shift_name.ToString() == "NIGHT"));
                DisplayEmployees(s);
            }
            else if (timetablegridView.CurrentCell == timetablegridView[2, 2])
            {
                List<EmployeeSchedule> s = employeeSchedules.FindAll(x => (x.ScheduleWeek == weekNum) && (x.Date_of_work.DayOfWeek.ToString() == "Tuesday") && (x.Shift_name.ToString() == "NIGHT"));
                DisplayEmployees(s);
            }
            else if (timetablegridView.CurrentCell == timetablegridView[3, 2])
            {
                List<EmployeeSchedule> s = employeeSchedules.FindAll(x => (x.ScheduleWeek == weekNum) && (x.Date_of_work.DayOfWeek.ToString() == "Wednesday") && (x.Shift_name.ToString() == "NIGHT"));
                DisplayEmployees(s);
            }

            else if (timetablegridView.CurrentCell == timetablegridView[4, 2])
            {
                List<EmployeeSchedule> s = employeeSchedules.FindAll(x => (x.ScheduleWeek == weekNum) && (x.Date_of_work.DayOfWeek.ToString() == "Thursday") && (x.Shift_name.ToString() == "NIGHT"));
                DisplayEmployees(s);
            }

            else if (timetablegridView.CurrentCell == timetablegridView[5, 2])
            {
                List<EmployeeSchedule> s = employeeSchedules.FindAll(x => (x.ScheduleWeek == weekNum) && (x.Date_of_work.DayOfWeek.ToString() == "Friday") && (x.Shift_name.ToString() == "NIGHT"));
                DisplayEmployees(s);
            }

            else if (timetablegridView.CurrentCell == timetablegridView[6, 2])
            {
                List<EmployeeSchedule> s = employeeSchedules.FindAll(x => (x.ScheduleWeek == weekNum) && (x.Date_of_work.DayOfWeek.ToString() == "Saturday") && (x.Shift_name.ToString() == "NIGHT"));
                DisplayEmployees(s);
            }
            else if (timetablegridView.CurrentCell == timetablegridView[7, 2])
            {
                List<EmployeeSchedule> s = employeeSchedules.FindAll(x => (x.ScheduleWeek == weekNum) && (x.Date_of_work.DayOfWeek.ToString() == "Sunday") && (x.Shift_name.ToString() == "NIGHT"));
                DisplayEmployees(s);
            }

            //if (timetablegridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            //{
            //    MessageBox.Show(timetablegridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            //}
        }

        private void lbassignemployee_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }





        private void btnassign_Click(object sender, EventArgs e)
        {
            var employee = (Employee)lbemployee.SelectedItem;
            List<Employee> freedaysemployees = db.GetEmployeeFreeDays();
            DateTime now = DateTime.Now;
            CultureInfo ciCurr2 = CultureInfo.CurrentCulture;
            int weekNum2 = ciCurr2.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            if (person == null)
            {
                MessageBox.Show("Select an employee");
            }
            else if (cbxshifts.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a shift");
            }
            else if (lbemployee.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a employee");
            }else if (cbxassigndep.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a department");
            }else if (employee.DepartmentID != ((Department)cbxassigndep.SelectedItem).Department_id)
            {
                MessageBox.Show("Departments did not match");
            }
            else
            {
                var dep = (Department)cbxassigndep.SelectedItem;
                CultureInfo ciCurr = CultureInfo.CurrentCulture;
                int weekNum = ciCurr.Calendar.GetWeekOfYear(dateTimePicker1.Value, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                if (weekNum2 > weekNum)
                {
                    MessageBox.Show("This date is in the past!");
                }
                else
                {
                    



                    int scheduleID = 0;
                    Shift shiftName = (Shift)cbxshifts.SelectedIndex;

                    List<Schedule> s = db.GetSchedulesByWeek(weekNum, ((Department)cbxassigndep.SelectedItem).Department_id);
                    if (s.Count == 0 || s == null)
                    {
                        db.AddSchedulesOfNewWeek(weekNum, dep.Department_id, dateTimePicker1.Value);

                    }
                    try
                    {
                        var selected = s.Find(x => (x.ShiftName == (Shift)cbxshifts.SelectedIndex) && (x.DayName.ToString() == dateTimePicker1.Value.DayOfWeek.ToString().ToUpper()));

                        if (selected.WorkingEmployeeNumber == selected.RequiredEmployeeNumber)
                        {
                            MessageBox.Show("Current Shift is full!");

                        }
                        else if (employeeSchedules.Exists(x => (x.PersonID == employee.Id) && (x.Shift_name == (Shift)cbxshifts.SelectedIndex) && (x.Date_of_work.DayOfWeek.ToString() == dateTimePicker1.Value.DayOfWeek.ToString())))
                        {
                            MessageBox.Show("This employee already assigned to this shift!");
                        }
                        else if ((Shift)cbxshifts.SelectedIndex == Shift.NIGHT && employee.NightShift == false)
                        {
                            MessageBox.Show("This employee can not work at night!");

                        }
                        else if (!admission.RunDayChecks(employee, db.GetAssignedScheduleIDsForChosenEmployee(employee.Id, weekNum), selected))
                        {
                            MessageBox.Show("This employee can not work consecutive shifts");
                        }
                        else
                        {

                            if (freedaysemployees.Exists(x => x.Id == employee.Id))
                            {
                                employee = freedaysemployees.Find(x => x.Id == employee.Id);

                                if (employee.Freedays.Contains(dateTimePicker1.Value.DayOfWeek.ToString().ToUpper()))
                                {
                                    DialogResult dialogResult = MessageBox.Show("This is the free day of this employee, Are you sure to assign?", "Assign Employee", MessageBoxButtons.YesNo);
                                    if (dialogResult == DialogResult.Yes)
                                    {


                                        db.AssignShiftToEmployee(employee.Id, selected.ScheduleID, dep.Department_id, weekNum, dateTimePicker1.Value, shiftName, dateTimePicker1.Value.DayOfWeek.ToString().ToUpper());
                                        MessageBox.Show("Done");
                                        employeeSchedules = db.GetAssignedEmployeeSchedulesByWeek(weekNum);


                                    }
                                    else if (dialogResult == DialogResult.No)
                                    {

                                    }

                                }
                                else
                                {

                                    db.AssignShiftToEmployee(employee.Id, selected.ScheduleID, dep.Department_id, weekNum, dateTimePicker1.Value, shiftName, dateTimePicker1.Value.DayOfWeek.ToString().ToUpper());
                                    MessageBox.Show("Done");
                                    employeeSchedules = db.GetAssignedEmployeeSchedulesByWeek(weekNum);
                                }


                            }
                            else
                            {
                                db.AssignShiftToEmployee(employee.Id, selected.ScheduleID, dep.Department_id, weekNum, dateTimePicker1.Value, shiftName, dateTimePicker1.Value.DayOfWeek.ToString().ToUpper());
                                MessageBox.Show("Done");
                                employeeSchedules = db.GetAssignedEmployeeSchedulesByWeek(weekNum);
                            }

                        }
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Schedule created, try again!");
                    }
                }

                
               






            }


        }

        private void DisplayEmployees(List<EmployeeSchedule> emps)
        {
            string employees = "";
            if (emps.Count != 0)
            {
                foreach (var item in emps)
                {
                    employees += $"{appSystem.GetPerson(item.PersonID)}\n";
                }
                MessageBox.Show(employees);
            }
            else
            {
                MessageBox.Show("Empty");
            }

        }



        public void updateCellsColor()
        {
            for (int i = 0; i < timetablegridView.Rows.Count; i++)
            {
                string value = "";
                int count = timetablegridView.Rows[i].Cells.Count;
                for (int j = 0; j < count; j++)
                {
                    value = timetablegridView.Rows[i].Cells[j].Value.ToString();
                    if (value.Contains("4/4") || value.Contains("1/1") || value.Contains("2/2"))
                    {
                        timetablegridView.Rows[i].Cells[j].Style.BackColor = Color.Green;
                    }

                }

            }

        }

        private void btnhelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Enter Example IDs -> 28, 31, 32, 36, 37");
        }

        private void btnclose_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnschedule_Click(object sender, EventArgs e)
        {
            AdminScheduleGUI pg = new AdminScheduleGUI(currentuser);
            pg.Show();
            this.Hide();
        }

        private void btnEmployee_Click_1(object sender, EventArgs e)
        {
            AdminEmployeeManagementGUI pg = new AdminEmployeeManagementGUI(currentuser);
            pg.Show();
            this.Hide();
        }

        private void btnproduct_Click(object sender, EventArgs e)
        {
            AdminProductGUI pg = new AdminProductGUI(currentuser);
            pg.Show();
            this.Hide();
        }

        private void lbLogout_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginGUI pg = new LoginGUI();
            pg.Show();
            this.Hide();
        }

        private void btndepartments_Click_1(object sender, EventArgs e)
        {
            AdminDepartmentsGUI pg = new AdminDepartmentsGUI(currentuser);
            pg.Show();
            this.Hide();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(dateTimePicker1.Value, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            tbweek.Text = weekNum.ToString();

            //DateTime dt = dateTimePicker1.Value;

            //bool isSunday = dt.DayOfWeek == 0;
            //var dayOfweek = isSunday == false ? (int)dt.DayOfWeek : 7;

            //DateTime startOfWeek = dt.AddDays(((int)(dayOfweek) * -1) + 1);
            //textBox1.Text = startOfWeek.ToString();

            DateTime today = DateTime.Today;
            int currentDayOfWeek = (int)today.DayOfWeek;
            DateTime sunday = today.AddDays(-currentDayOfWeek);
            DateTime monday = sunday.AddDays(1);
            // If we started on Sunday, we should actually have gone *back*
            // 6 days instead of forward 1...
            if (currentDayOfWeek == 0)
            {
                monday = monday.AddDays(-7);
            }
            var dates = Enumerable.Range(0, 7).Select(days => monday.AddDays(days)).ToList();
            


        }

        private void btnfindschedule_Click(object sender, EventArgs e)
        {
            try
            {
                
                    if (cbxdepschedule.SelectedIndex == -1)
                    {
                        MessageBox.Show("Please select a department!");
                        
                    }
                    else
                    {
                        CultureInfo ciCurr = CultureInfo.CurrentCulture;
                        int weekNum = ciCurr.Calendar.GetWeekOfYear(dateTimePicker2.Value, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                        var dep = (Department)cbxdepschedule.SelectedItem;
                        timetablegridView.Visible = true;
                        LoadSchedule(weekNum, dep.Department_id);
                        employeeSchedules = db.GetAssignedEmployeeSchedulesByWeek(weekNum);
                        lbweek.Text = $"Week Number: {weekNum.ToString()}";
                    }
                
                

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
           
        }

        private void btnupdateschedule_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbxupdateshift.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a shift");
                }
                else if (cbxupdatedep.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a department");
                }
                else
                {
                    CultureInfo ciCurr = CultureInfo.CurrentCulture;
                    int weekNum = ciCurr.Calendar.GetWeekOfYear(dateTimePicker3.Value, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                    var dep = (Department)cbxupdatedep.SelectedItem;
                    List<Schedule> s = db.GetSchedulesByWeek(weekNum, dep.Department_id);
                    var selected = s.Find(x => (x.ShiftName == (Shift)cbxupdateshift.SelectedIndex) && (x.DayName.ToString() == dateTimePicker3.Value.DayOfWeek.ToString().ToUpper()));
                    if (selected != null)
                    {


                        db.EditRequiredEmployeeNumberOfShift(dep.Department_id, selected.ShiftName.ToString(), selected.DayName.ToString(), weekNum, Convert.ToInt32(tbmaxemployee.Text));
                        MessageBox.Show("Updated!");


                    }
                    else
                    {
                        MessageBox.Show("Schedule doesn't exists");
                    }
                }
               
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(dateTimePicker3.Value, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            tbupdateweek.Text = weekNum.ToString();
        }

        private void cbxupdateshift_SelectedIndexChanged(object sender, EventArgs e)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(dateTimePicker3.Value, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            if(toggle2== 0)
            {
                toggle2 = 1;
            }
            else
            {
                if (cbxupdatedep.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a department first");
                }
                else
                {
                    List<Schedule> s = db.GetSchedulesByWeek(weekNum, ((Department)cbxupdatedep.SelectedItem).Department_id);
                    var selected = s.Find(x => (x.ShiftName == (Shift)cbxupdateshift.SelectedIndex) && (x.DayName.ToString() == dateTimePicker3.Value.DayOfWeek.ToString().ToUpper()));
                    if (selected != null)
                    {
                        tbmaxemployee.Text = selected.RequiredEmployeeNumber.ToString();
                    }
                    else
                    {
                        tbmaxemployee.Text = "No Data";
                        MessageBox.Show("Week doesn't exists");
                    }
                }
            }
           
            
        
        }

        private void cbxselectemployeedep_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dep = (Department)cbxselectemployeedep.SelectedItem;
            
            var contract = (Contract)cbxselectcontract.SelectedIndex;
            List<Employee> depemployee = db.GetAllEmployees().FindAll(x => x.DepartmentID == dep.Department_id);
            if (contract == Contract._40)
            {
                depemployee = depemployee.FindAll(x => x.Contract == contract);
            }
            else if (contract == Contract._32)
            {
                depemployee = depemployee.FindAll(x => x.Contract == contract);
            }
            else if (contract == Contract._0)
            {
                depemployee = depemployee.FindAll(x => x.Contract == contract);
            }
            lbemployee.Items.Clear();
            foreach (var item in depemployee)
            {

                lbemployee.Items.Add(item);
            }
        }

        private void cbxselectcontract_SelectedIndexChanged(object sender, EventArgs e)
        {
        
            if(count == 0)
            {
                cbxselectemployeedep.SelectedIndex = 0;
                count = 1;
            }
            
            var dep = (Department)cbxselectemployeedep.SelectedItem;

            var contract = (Contract)cbxselectcontract.SelectedIndex;
            List<Employee> depemployee = db.GetAllEmployees().FindAll(x => x.DepartmentID == dep.Department_id);
            if (contract == Contract._40)
            {
                depemployee = depemployee.FindAll(x => x.Contract == contract);
            }
            else if (contract == Contract._32)
            {
                depemployee = depemployee.FindAll(x => x.Contract == contract);
            }
            else if (contract == Contract._0)
            {
                depemployee = depemployee.FindAll(x => x.Contract == contract);
            }
            lbemployee.Items.Clear();
            foreach (var item in depemployee)
            {

                lbemployee.Items.Add(item);
            }
        }

        private void btnautoscheduler_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("This process can take a few minutes, Please don't close the application", "AutoScheduler", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
               
                
                s.AutomateSchedule((Department)cbxautodep.SelectedItem, dateautoscheduler.Value);
                MessageBox.Show("Done");
            }
        }

        private void dateautoscheduler_ValueChanged(object sender, EventArgs e)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(dateautoscheduler.Value, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            label17.Text = $"Week Number: {weekNum.ToString()}";
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(dateTimePicker4.Value, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            tbremoveweek.Text = weekNum.ToString();
        }

        private void btnremoveshift_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you Sure to Remove the shift from the Employee?", "Remove Shift", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    CultureInfo ciCurr = CultureInfo.CurrentCulture;
                    int weekNum = ciCurr.Calendar.GetWeekOfYear(dateTimePicker4.Value, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                    var dep = (Department)cbxremovedep.SelectedItem;
                    var shift = (Shift)cbxremoveshift.SelectedIndex;
                    var schedule = db.GetSchedule(weekNum, dateTimePicker4.Value.DayOfWeek
                            .ToString().ToUpper(), (Shift)cbxremoveshift.SelectedIndex, ((Department)cbxremovedep.SelectedItem).Department_id);
                    if(schedule.RequiredEmployeeNumber == 0)
                    {
                        MessageBox.Show("No Employees Found");
                    }else if (cbxremoveemp.SelectedIndex == -1 || cbxremoveemp.Text == "")
                    {
                        MessageBox.Show("Please select an employee");
                    }else if (cbxremoveemp.Text == "No Employees Found")
                    {
                        MessageBox.Show("No Employees selected");
                    }
                    else
                    {
                        int index = cbxremovedep.SelectedIndex;
                        db.DeleteEmployeeSchedule2(((Person)cbxremoveemp.SelectedItem).Id,schedule.ScheduleID);
                        cbxremoveemp.Items.RemoveAt(index);
                        cbxremoveemp.SelectedIndex = -1;
                   
                        MessageBox.Show("Done");
                    }
              
                }
                catch (Exception)
                {

                    MessageBox.Show("Error");
                }
            }
           
        }
   

        private void cbxremoveshift_SelectedIndexChanged(object sender, EventArgs e)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(dateTimePicker4.Value, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            if(toggle == 0)
            {
                toggle = 1;
            }
            else
            {
                if (cbxremovedep.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a department first");
                    

                }
                else
                {
                    string day = dateTimePicker4.Value.DayOfWeek
                    .ToString().ToUpper();

                    var schedule = db.GetSchedule(weekNum, dateTimePicker4.Value.DayOfWeek
                    .ToString().ToUpper(), (Shift)cbxremoveshift.SelectedIndex, ((Department)cbxremovedep.SelectedItem).Department_id);

                    string shift = ((Shift)cbxremoveshift.SelectedIndex).ToString();
                    employeeSchedules = db.GetAssignedEmployeeSchedulesByWeek(weekNum);
                    List<EmployeeSchedule> s = employeeSchedules.FindAll(x => (x.ScheduleWeek == weekNum) && (x.Date_of_work.DayOfWeek.ToString() == dateTimePicker4.Value.DayOfWeek
                    .ToString()) && (x.Shift_name.ToString() == shift) &&(x.ScheduleID
                     == schedule.ScheduleID));
                    
                    cbxremoveemp.Items.Clear();
                    if(s != null && s.Count != 0)
                    {
                        foreach (var item in s)
                        {
                            cbxremoveemp.Items.Add(appSystem.GetPerson(item.PersonID));
                        }
                    }
                    else
                    {
                        cbxremoveemp.Items.Add("No Employees Found");
                    }
                    
                }
            }
            


        }

        private void cbxremovedep_SelectedIndexChanged(object sender, EventArgs e)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(dateTimePicker4.Value, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            string day = dateTimePicker4.Value.DayOfWeek
                    .ToString().ToUpper();

            var schedule = db.GetSchedule(weekNum, dateTimePicker4.Value.DayOfWeek
            .ToString().ToUpper(), (Shift)cbxremoveshift.SelectedIndex, ((Department)cbxremovedep.SelectedItem).Department_id);

            string shift = ((Shift)cbxremoveshift.SelectedIndex).ToString();
            employeeSchedules = db.GetAssignedEmployeeSchedulesByWeek(weekNum);
            List<EmployeeSchedule> s = employeeSchedules.FindAll(x => (x.ScheduleWeek == weekNum) && (x.Date_of_work.DayOfWeek.ToString() == dateTimePicker4.Value.DayOfWeek
            .ToString()) && (x.Shift_name.ToString() == shift) && (x.ScheduleID
             == schedule.ScheduleID));

            cbxremoveemp.Items.Clear();
            if (s != null && s.Count != 0)
            {
                foreach (var item in s)
                {
                    cbxremoveemp.Items.Add(appSystem.GetPerson(item.PersonID));
                }
            }
            else
            {
                cbxremoveemp.Items.Add("No Employees Found");
            }
        }

        private void cbxupdatedep_SelectedIndexChanged(object sender, EventArgs e)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(dateTimePicker3.Value, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            List<Schedule> s = db.GetSchedulesByWeek(weekNum, ((Department)cbxupdatedep.SelectedItem).Department_id);
            var selected = s.Find(x => (x.ShiftName == (Shift)cbxupdateshift.SelectedIndex) && (x.DayName.ToString() == dateTimePicker3.Value.DayOfWeek.ToString().ToUpper()));
            if (selected != null)
            {
                tbmaxemployee.Text = selected.RequiredEmployeeNumber.ToString();
            }
            else
            {
                tbmaxemployee.Text = "No Data";
                MessageBox.Show("Week doesn't exists");
                
            }
        }

        private void BtnSendEmail_Click(object sender, EventArgs e)
        {
            s.SendEmailMethod();
            //BtnSendEmail.Enabled = false;
        }
    }
}
