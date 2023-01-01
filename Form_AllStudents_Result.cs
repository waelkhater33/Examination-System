using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSlProjrct_Version1
{
    public partial class Form_AllStudents_Result : Form
    {
        Context ctx = new Context();

        public Form_AllStudents_Result()
        {
            InitializeComponent();
            var res = ctx.StudentAnswers.
                Select(ss => new { 
                    ss.student_id, 
                    ss.Student.student_fn,
                    ss.Student.student_ln,
                    ss.Student.adress,
                    ss.Student.phone,
                    ss.Course.course_name,
                    ss.exam_id, ss.resultC,
                    ss.resultTF, 
                    ss.resultText, 
                    ss.exam_degree }).ToList();
            dataGridView1.DataSource = res;
            dataGridView1.Columns[0].HeaderCell.Value = "Student ID";
            dataGridView1.Columns[1].HeaderCell.Value = "First name";
            dataGridView1.Columns[2].HeaderCell.Value = "Last Name";
            dataGridView1.Columns[3].HeaderCell.Value = "Adress";
            dataGridView1.Columns[4].HeaderCell.Value = "Phone";
            dataGridView1.Columns[5].HeaderCell.Value = "Course";
            dataGridView1.Columns[6].HeaderCell.Value = "Exam Numbere";
            dataGridView1.Columns[7].HeaderCell.Value = "MCQ Result";
            dataGridView1.Columns[8].HeaderCell.Value = "Text Result";
            dataGridView1.Columns[9].HeaderCell.Value = "T/F Result";
            dataGridView1.Columns[10].HeaderCell.Value = "Total Degree";



        }

        private void btn_backtoHome_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
