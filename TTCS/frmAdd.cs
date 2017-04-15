using System;
using System.Globalization;
using System.Windows.Forms;

namespace TTCS
{
    public partial class frmAdd : Form
    {
        public delegate void Add(Staff staff);
        public event Add AddEvent;
        public frmAdd()
        {
            InitializeComponent();
            txtDay.Text = DateTime.Now.Day.ToString();
            txtMonth.Text = DateTime.Now.Month.ToString();
            txtYear.Text = DateTime.Now.Year.ToString();
            cbbLevel.Items.AddRange(Level.Instance.ListLevel);
            cbbLevel.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtDay.Text = "";
            txtMonth.Text = "";
            txtYear.Text = "";
            cbbLevel.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Staff staff = new Staff();
            staff.Name = txtName.Text;
            foreach (var item in Level.Instance.ListLevel)
            {
                if (item.Key == cbbLevel.Text)
                {
                    staff.Level = item;
                    break;
                }
            }
            staff.DayOfBirth = new Date()
            {
                Day = int.Parse(txtDay.Text),
                Month = int.Parse(txtMonth.Text),
                Year = int.Parse(txtYear.Text)
            };
            staff.Salary = double.Parse(txtSalary.Text, CultureInfo.InvariantCulture);
            if (AddEvent != null)
            {
                AddEvent(staff);
                Close();
            }
            else
            {
                Close();
            }
        }
    }
}
