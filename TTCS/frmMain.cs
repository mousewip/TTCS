using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TTCS
{
    public partial class frmMain : Form
    {
        private LinkedList lStaff;
        private LinkedList lResult;
        /// <summary>
        /// modeView == 0 : View DTGV by default
        /// modeView == 1 : View DTGV by Search
        /// </summary>
        private int modeView = 0;

        public frmMain()
        {
            InitializeComponent();

            dtgvMain.ColumnCount = 5;
            dtgvMain.Columns[0].Name = "STT";
            dtgvMain.Columns[1].Name = "Họ và tên";
            dtgvMain.Columns[2].Name = "Chức vụ";
            dtgvMain.Columns[3].Name = "Ngày Sinh";
            dtgvMain.Columns[4].Name = "Hệ số lương";

            dtgvMain.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dtgvMain.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dtgvMain.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dtgvMain.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dtgvMain.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            lStaff = new LinkedList();
            lResult = new LinkedList();
            LoadCbb();

        }


        private void LoadCbb()
        {
            cbbTypeOfSort.Items.AddRange(new object[]{"Tên", "Chức vụ", "Ngày sinh", "Hệ số lương"});
            cbbTypeOfSort.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var f = new frmAdd();
            f.AddEvent += F_AddEvent;
            f.Show();
        }

        private void F_AddEvent(Staff staff)
        {
            Node staffNode = new Node()
            {
                Data = staff,
                Next = null
            };
            lStaff.Insert(staffNode, cbbTypeOfSort.SelectedIndex, rbtnAsc.Checked);
            LoadListStaff(lStaff);
        }

        private void LoadListStaff(LinkedList lkStaff)
        {
            dtgvMain.Rows.Clear();
            string[] row;
            Node current = lkStaff.Head;
            while (current != null)
            {
                Staff data = current.Data as Staff;
                row = new string[] {current.Index.ToString(), data.Name, data.Level.Key, data.DayOfBirth.ToString(), data.Salary.ToString(CultureInfo.InvariantCulture) };
                dtgvMain.Rows.Add(row);
                current = current.Next;
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {

            ofd.InitialDirectory = @"D:\IDE\CSharp\TTCS\TTCS\data";
            ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = false;
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                string fullPath = ofd.FileName;
                string fileName = ofd.SafeFileName;
                string path = fullPath.Replace(fileName, "") + fileName;
                try
                {
                    lStaff.Clear();
                    string[] fInput = File.ReadAllLines(path);
                    for (int i = 0; i < fInput.Length; i++)
                    {
                        Staff staff = new Staff();

                        #region Init Staff

                        staff.Name = fInput[i++];
                        foreach (var lev in Level.Instance.ListLevel)
                        {
                            if (lev.Key == fInput[i])
                            {
                                staff.Level = lev;
                                break;
                            }
                        }
                        i++;
                        string[] dob = fInput[i++].Split(' ');
                        try
                        {
                            Date dayOfBirth = new Date()
                            {
                                Day = Int32.Parse(dob[0]),
                                Month = Int32.Parse(dob[1]),
                                Year = Int32.Parse(dob[2])
                            };
                            staff.DayOfBirth = dayOfBirth;
                        }
                        catch
                        {
                            MessageBox.Show("Phát hiện ngày sinh không hợp lệ!", "Lỗi", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            return;
                        }

                        try
                        {
                            staff.Salary = double.Parse(fInput[i], CultureInfo.InvariantCulture);
                        }
                        catch
                        {
                            MessageBox.Show("Phát hiện hệ số lương không hợp lệ", "Lỗi", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            return;
                        }
                        #endregion
                        lStaff.AddLast(staff);
                    }
                    lStaff.CustomSort(cbbTypeOfSort.SelectedIndex, rbtnAsc.Checked);
                    LoadListStaff(lStaff);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }  
        }

        private bool TTCSContain(string dest, string source)
        {
            int result = dest.IndexOf(source);
            if (result < 0)
                return false;
            else
                return true;
        }


        private void Search(string key)
        {
            #region Init key (double and int)
            int intKey;
            try
            {
                intKey = Int32.Parse(key);
            }
            catch
            {
                intKey = -1;
            }

            double dKey;
            try
            {
                dKey = double.Parse(key, CultureInfo.InvariantCulture);
            }
            catch
            {
                dKey = -1.0;
            }
            #endregion
            lResult.Clear();
            Node current = lStaff.Head;
            while (current != null)
            {
                Staff data = current.Data as Staff;
                if (TTCSContain(data.Name, key) || data.DayOfBirth.Day == intKey || data.DayOfBirth.Month == intKey ||
                    data.DayOfBirth.Year == intKey ||
                    TTCSContain(data.Level.Key, key) || data.Salary == dKey)
                {
                    lResult.AddLast(data, current.Index);
                }
                current = current.Next;
            }
            lResult.CustomSort(cbbTypeOfSort.SelectedIndex, rbtnAsc.Checked);
            LoadListStaff(lResult);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text != string.Empty)
                modeView = 1;
            else
            {
                modeView = 0;
            }
            Search(txtSearch.Text);
        }

        private void cbbTypeOfSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void UpdateView()
        {
            lStaff.CustomSort(cbbTypeOfSort.SelectedIndex, rbtnAsc.Checked);
            lResult.CustomSort(cbbTypeOfSort.SelectedIndex, rbtnAsc.Checked);
            if (modeView == 0)
            {
                LoadListStaff(lStaff);
            }
            else
            {
                LoadListStaff(lResult);
            }
        }

        private void rbtnDes_CheckedChanged(object sender, EventArgs e)
        {
            UpdateView();
        }


        private void btnExport_Click(object sender, EventArgs e)
        {
            string path = @"D:\IDE\CSharp\TTCS\TTCS\data\output.txt";
            if (lStaff.ExportTextFile(path))
            {
                var result = MessageBox.Show("Xuất file text thành công.\n Mở file ngay?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if ( result == DialogResult.Yes)
                    Process.Start(path);
            }
            else
            {
                MessageBox.Show(@"Xuất file text thất bại.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text != string.Empty)
                modeView = 1;
            else
            {
                modeView = 0;
            }
            Search(txtSearch.Text);
            if (lResult.Head == null)
            {
                MessageBox.Show(@"Không tìm thấy nhân viên cần xóa");
            }
            else
            {
                //Node current = lResult.Head;
                while (lResult.Head != null)
                {
                    Node tmp = lResult.Head;
                    lStaff.Delete(tmp);
                    lResult.DeleteFirst();
                }
                lStaff.UpdateIndex();
                LoadListStaff(lStaff);
            }
            

        }
    }
}
