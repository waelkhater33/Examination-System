using CSlProjrct_Version1.Instructor_create_ExamForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSlProjrct_Version1
{
    public partial class Form_logIn : Form
    {
        Context ctx=new Context();

        List<User> users;
        List<User> instructors;
        List<User> managers;


        public Form_logIn()
        {
            InitializeComponent();
       
        }

        private void btn_logIn_Click(object sender, EventArgs e)
        {
            string email = textBox_Email.Text;
            string pass = textBox_password.Text;
            int flag = 0;

            if(comboBox1.SelectedIndex==0)
            {
                foreach (var item in users)
                {
                    if (email == item.mail && pass == item.pass)
                    {
                        Form_studentHomw frm = new Form_studentHomw();

                        frm.exam_id = item.exam_id;
                        frm.student_id = item.studen_id;
                        frm.Show();
                        this.Hide();
                        flag = 0;
                    }
                    else { flag = 1; }
                }
                if(flag == 1)
                {
                    label3.Visible = true;
                    label3.Text = "you are not Student";
                }

            }
           else if (comboBox1.SelectedIndex == 2)
            {
                foreach (var item in managers)
                {
                    if (email == item.mail && pass == item.pass)
                    {
                        Form1 frm = new Form1();
                        frm.Manager_id = item.studen_id;
                        frm.Show();
                        this.Hide();
                        flag = 0;
                    }
                    else
                    { flag = 1; }
                }
                if (flag == 1)
                {
                    label3.Visible = true;
                    label3.Text = "you are not Manager";
                }

            }

            // from Istructors
            else if (comboBox1.SelectedIndex == 1)
            {
                foreach (var item in instructors)
                {
                    if (email == item.mail && pass == item.pass)
                    {
                        Instructor_dashBooard frm = new Instructor_dashBooard();
                        frm.Instructor_id = item.studen_id;
                        frm.Show();
                        this.Hide();
                        flag = 0;

                    }
                    else
                    { flag = 1; }
                }
                
            if (flag == 1)
            {
                label3.Visible = true;
                label3.Text = "you are not Instructor";
            }

        }
             else
            {
                label3.Visible = true;
                label3.Text = "not vaild user or password";
            }
           






        }

        private void Form_logIn_Load(object sender, EventArgs e)
        {
            users = ctx?.Students.Select(obj => new User { mail = obj.userName, pass = obj.Password, studen_id = (int)obj.student_id, exam_id = (int)obj.exam_id }).ToList();
            instructors = ctx?.Instructors.Select(obj => new User { mail = obj.userName, pass = obj.Password, studen_id = (int)obj.instructor_id}).ToList();
            managers = (from c in ctx?.Instructors
                        join f in ctx?.Instructors
                        on c.instructor_id equals f.manager_ID
                        select new User { mail = c.userName, pass = c.Password, studen_id = (int)c.instructor_id }).ToList(); 
        }
    }
    public class User
    {
        public string mail { get; set; }
        public string pass { get; set; }
        public int exam_id { get; set; }
        public int studen_id { get; set; }
    }
}
