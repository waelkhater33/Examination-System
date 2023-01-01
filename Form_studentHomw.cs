using CSlProjrct_Version1.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace CSlProjrct_Version1
{
   
    public partial class Form_studentHomw : Form
    {
        public int exam_id { get; set; }
        public int student_id { get; set; }
     
        private int examdeg=0;
        private int flagexammed=1;
        Context ctx = new Context();
        public Form_studentHomw()
        {
            InitializeComponent();
            #region insert into DB
            //Branch branch = new Branch() { };
            //ctx.Branches.Add(branch);
            //ctx.SaveChanges();

            //Track track  = new Track() { };
            //ctx.Tracks.Add(track);
            //ctx.SaveChanges();

            //Intake intake = new Intake() { };
            //ctx.Intakes.Add(intake);
            //ctx.SaveChanges();

            //Course course=new Course() {course_name="C#" };
            //ctx.Courses.Add(course);
            //ctx.SaveChanges();

            //Instructor instructor = new Instructor() { };
            //ctx.Instructors.Add(instructor);
            //ctx.SaveChanges();
            //Exam e = new Exam() {branch_id=1,track_id=1,intake_id=1,course_id=1 ,instructor_id=1};
            //ctx.Exams.Add(e);
            //ctx.SaveChanges();

            //Student s = new Student() {exam_id=8,student_fn="moussa", branch_id = 1, track_id = 1, intake_id = 1 };
            //ctx.Students.Add(s);
            //ctx.SaveChanges();


            //    List<ExamQuestionsChoice> chossequestionList = new List<ExamQuestionsChoice>()
            //{
            //new ExamQuestionsChoice{exam_id = 8,question_id=1, question_des="chose char a",option1="A",option2="B",option3="C",option4="D",answerChoice='a',degree=5},
            //new ExamQuestionsChoice{exam_id = 8,question_id=2,question_des="chose char  b",option1="Z",option2="M",option3="L",option4="B",answerChoice='d',degree=15},
            //new ExamQuestionsChoice {exam_id = 8,question_id=3, question_des = "chose char c", option1 = "Y", option2 = "B", option3 = "C", option4 = "X", answerChoice = 'c', degree = 5 },
            //new ExamQuestionsChoice {exam_id = 8,question_id=4, question_des = "chose char d", option1 = "U", option2 = "P", option3 = "L", option4 = "D", answerChoice = 'd', degree = 10 },
            //};

            //    List<ExamQuestionsTF> TFquestionList = new List<ExamQuestionsTF>()
            //{
            //new ExamQuestionsTF{exam_id = 8,question_id=1,question_des="do you love me " ,answerTF=true,degree=10},
            //new ExamQuestionsTF{exam_id = 8,question_id=2,question_des="do you need me " ,answerTF=false,degree=10},
            //new ExamQuestionsTF{exam_id = 8,question_id=3,question_des="do you want me " ,answerTF=true ,degree=10},
            //};
            //    List<ExamQuestionsText> TextquestionList = new List<ExamQuestionsText>()
            //{
            //new ExamQuestionsText{exam_id = 8,question_id=1,question_des="cat can fly " , answerText="yes it can",degree=20},
            //new ExamQuestionsText{exam_id = 8,question_id=2,question_des="do you need me " ,answerText="",degree=20},
            //new ExamQuestionsText{exam_id = 8,question_id=3,question_des="do you want me " ,answerText="3la 7sb",degree=10},
            //};
            //    ctx.ExamQuestionsChoices.AddRange(chossequestionList);
            //    ctx.ExamQuestionsTFs.AddRange(TFquestionList);
            //    ctx.ExamQuestionsTexts.AddRange(TextquestionList);

            //    ctx.SaveChanges(); 
            #endregion

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void btn_startExam_Click(object sender, EventArgs e)
        {

            if (flagexammed == 0 & exam_id != 0)
            {
                Form_exam fx = new Form_exam();
                fx.exam_id = exam_id;
                fx.student_id = student_id;
                fx.Show();
               
            }
            else if (exam_id == 0)
            {
                MessageBox.Show("Wait for you Exam time");

            }
            else if (flagexammed == 1)
            {
                MessageBox.Show("Sorry you have Submitted your EXam ");
            }


        }

        private void Form_studentHomw_Load(object sender, EventArgs e)
        {

            
            var stu = ctx.Students
                .Where(s => s.exam_id == exam_id & s.student_id == this.student_id)
                .Select(s => new
                {
                    s.student_fn,
                    s.student_ln,
                    s.adress,
                    s.Course.course_name,
                    s.Branch.branch_name,
                    s.Track.track_name,
                    s.Intake.intake_number,
                    s.userName,
                    s.Password,
                    s.phone,

                }).FirstOrDefault();

            var examdegree = ctx.StudentAnswerSheets.
               Where(ee => ee.exam_id == exam_id & ee.Student_id == student_id)
               .Select(deg => (int?)deg.degree).Sum();

            if (examdegree == null)
            {
                examdeg = 0;
            }
            var ctxflagexmmed = ctx.StudentAnswers?
                .
                Where(ee => ee.exam_id == exam_id & ee.student_id == student_id)
                .Select(ee => (int?)ee.result)?.FirstOrDefault();
            if (ctxflagexmmed == null)
            {
                flagexammed = 0;
            }
            label2.Text = stu.student_fn;
            label4.Text = stu.student_ln;
            label6.Text = student_id.ToString();
            label8.Text = exam_id.ToString();
            label10.Text = stu.userName;
            label12.Text = stu.Password;
            label14.Text = examdegree.ToString();
            label16.Text = stu.adress;
            label18.Text = stu.course_name;
            label20.Text = stu.branch_name;
            label22.Text = stu.intake_number.ToString();
            label23.Text = stu.track_name;
            label26.Text = stu.phone;



        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form_logIn frm = new Form_logIn();
            frm.Show();
            this.Hide();
        }
    }
}
