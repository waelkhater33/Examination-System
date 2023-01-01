using CSlProjrct_Version1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure.Annotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace CSlProjrct_Version1
{
    public partial class Form_exam : Form
    {

        public int exam_id { get; set; }
        public int student_id { get; set; }

        private int Chossecounter = 0;
        private int[] resultOfChoose ;
        String[,] AnswerChosserquestions;

        private int tFcounter = 0;
        private int[] resultOfTf;
        bool[,] AnswerTFquestions;

        private int Textcounter = 0;
        private int[] resultOfText;
        String[,] AnswerTextquestions;
        Context ctx = new Context();
        List<ExamQuestionsChoice> chossequestionList = new List<ExamQuestionsChoice>();
        List<ExamQuestionsTF> TFquestionList=new List<ExamQuestionsTF>();
        List<ExamQuestionsText> TextquestionList=new List<ExamQuestionsText>();


        public Form_exam()
        {
            //exam_id = 8;
            //student_id = 3;
            
            InitializeComponent();
            //List<StudentAnswerSheet> sas = new List<StudentAnswerSheet>();
            //StudentAnswerSheet s = new StudentAnswerSheet() { exam_id = 8, Student_id = 3 };
            //sas.Add(s);
            //ctx.StudentAnswerSheets.AddRange(sas);
            //ctx.SaveChanges();
            
            
        }

        #region nextBtn

        private void button1_Click(object sender, EventArgs e)
        {
        

            if (button1.Text == "start1")
            {
                label_question.Text = "Prss next to view first question GOOD Luck ";
                button1.Text = "Next";
            }
            else if (Chossecounter <= chossequestionList.Count)
            {
                if (Chossecounter>0)
                {
                    takechooseanswer(Chossecounter-1);
                    if(Chossecounter == chossequestionList.Count)
                    {
                        Chossecounter++;
                    }
                }
                if(Chossecounter < chossequestionList.Count)
                {
                    checkBox1.Visible = true;
                    checkBox2.Visible = true;
                    checkBox3.Visible = true;
                    checkBox4.Visible = true;
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                    checkBox3.Checked = false;
                    checkBox4.Checked = false;
                    ExamQuestionsChoice c = chossequestionList[Chossecounter];
                    label_question.Text = c.question_des;
                    checkBox1.Text = c.option1;
                    checkBox2.Text = c.option2;
                    checkBox3.Text = c.option3;
                    checkBox4.Text = c.option4;

                    Chossecounter++;

                }
                
             }
           
            else if (tFcounter <= TFquestionList.Count )
            {
                if(tFcounter > 0)
                {
                    takeTFanswer(tFcounter-1);
                    if (tFcounter == TFquestionList.Count)
                    {
                        tFcounter++;
                    }
                }
                if(tFcounter < TFquestionList.Count)
                {

                    ExamQuestionsTF c = TFquestionList[tFcounter];
                    label_question.Text = c.question_des;
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                    checkBox1.Text = "True";
                    checkBox2.Text = "False";
                    checkBox3.Visible = false;
                    checkBox4.Visible = false;
                    tFcounter++;
                }
                
            }
          
            else if (Textcounter <= TextquestionList.Count)
            {
                    if (Textcounter > 0)
                    {
                        takeTextanswer(Textcounter - 1);
                      if (Textcounter == TextquestionList.Count)
                      {
                        Textcounter++;
                      }
                }
                    if (Textcounter < TextquestionList.Count)
                    {
                    ExamQuestionsText c = TextquestionList[Textcounter];
                    label_question.Text = c.question_des;
                    checkBox1.Visible = false;
                    checkBox2.Visible = false;
                    checkBox3.Visible = false;
                    checkBox4.Visible = false;
                    richTextBox1.Visible = true;
                    Textcounter++; 

                    }

              

             }
            else
            {
                calcChooseResult();
                calcTextResult();
                calcTfResult();


                
                
                int cout = 1;
                int coutchoose = 0;
                int couttf = 0;
                int coutText = 0;
                #region enter data to answers sheet

                for (int i = 0; i < AnswerChosserquestions.GetLength(0); i++)
                {
                    StudentAnswerSheet ss = new StudentAnswerSheet()
                    {
                        Student_id = student_id,
                        exam_id = exam_id,
                        question_id = cout,
                        question = AnswerChosserquestions[i, 0],
                        Answers = AnswerChosserquestions[i, 1],
                        degree = resultOfChoose[i],
                    };
                    coutchoose += resultOfChoose[i];
                    ctx.StudentAnswerSheets.Add(ss);

                    cout++;

                }
                for (int i = 0; i < AnswerTFquestions.GetLength(0); i++)
                {
                    StudentAnswerSheet ss = new StudentAnswerSheet()
                    {
                        Student_id = student_id,
                        exam_id = exam_id,
                        question_id = cout,
                        question = AnswerTFquestions[i, 0].ToString(),
                        Answers = AnswerTFquestions[i, 1].ToString(),
                        degree = resultOfTf[i],
                    };
                    ctx.StudentAnswerSheets.Add(ss);
                    couttf += resultOfTf[i];
                    cout++;
                }
                for (int i = 0; i < AnswerTextquestions.GetLength(0); i++)
                {
                    StudentAnswerSheet ss = new StudentAnswerSheet()
                    {
                        Student_id = student_id,
                        exam_id = exam_id,
                        question_id = cout,
                        question = AnswerTextquestions[i, 0],
                        Answers = AnswerTextquestions[i, 1],
                        degree = resultOfText[i],
                    };
                    ctx.StudentAnswerSheets.Add(ss);
                    coutText += resultOfText[i];
                    cout++;
                }
                ctx.SaveChanges();
                #endregion



                richTextBox1.Visible = false;
                checkBox1.Visible = false;
                checkBox2.Visible = false;
                checkBox3.Visible = false;
                checkBox4.Visible = false;
                label_question.Visible= false;
                groupBox_question.Visible = true;
                dataGridView_showAnswers.Visible = true;

                btn_backToStudentPage.Visible = true;

                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                btn_back.Visible = false;
                button1.Visible = false;
                
                dataGridView_showAnswers.DataSource = ctx.StudentAnswerSheets.Where(ans=>ans.exam_id==exam_id &ans.Student_id==student_id).Select( ans => new {ans.question_id,ans.question,ans.Answers,ans.degree}).ToList();
                var studentName = ctx.Students.Where(ss => ss.student_id == student_id).Select(ss=>ss.student_fn).FirstOrDefault();
                
                label2.Text = "student Name : " + studentName;
                label3.Text = "exam ID  : " + exam_id.ToString();

                int sum = 0;
                #region old data grid view
                /*
                            int counterQuestions = 0;
                        dataGridView_showAnswers.Columns.Add("question", "Question No");
                        dataGridView_showAnswers.Columns.Add("ans", "Your Answer");
                        dataGridView_showAnswers.Columns.Add("Value", "Correct Answer");
                        dataGridView_showAnswers.Columns.Add("deg", "degree");

                        for (int k = 0; k < AnswerChosserquestions.GetLength(0); k++)
                        {
                            counterQuestions++;
                            dataGridView_showAnswers.Rows.Add(new object[] { counterQuestions, AnswerChosserquestions[k, 0], AnswerChosserquestions[k, 1], resultOfChoose[k] });
                        }

                        for (int k = 0; k < AnswerTFquestions.GetLength(0); k++)
                        {
                            counterQuestions++;
                            dataGridView_showAnswers.Rows.Add(new object[] { counterQuestions, AnswerTFquestions[k, 0], AnswerTFquestions[k, 1], resultOfTf[k] });
                        }
                        for (int k = 0; k < AnswerTextquestions.GetLength(0); k++)
                        {
                            counterQuestions++;
                            dataGridView_showAnswers.Rows.Add(new object[] { counterQuestions, AnswerTextquestions[k, 0], AnswerTextquestions[k, 1], resultOfText[k] });
                        }
                        */
                #endregion

                for (int i = 0; i < dataGridView_showAnswers.Rows.Count; ++i)
                {
                    sum += Convert.ToInt32(dataGridView_showAnswers.Rows[i].Cells[3].Value);
                }

                label1.Text = "Your total Degree is : " + sum.ToString();
                var cours_id = ctx.Students.Where(ss => ss.student_id == student_id).Select(ss=>ss.course_id).FirstOrDefault();
                StudentAnswer s = new StudentAnswer() {
                    student_id = student_id,
                    exam_id = exam_id,
                    resultC=coutchoose,
                    resultTF=couttf,
                    resultText=coutText,
                    exam_degree=sum,
                    course_id=cours_id,
                    result=1
                };
                ctx.StudentAnswers.Add(s);
                ctx.SaveChanges();
            }
        }
        #endregion
       
        private void btn_back_Click(object sender, EventArgs e)
        {
            checkBox1.Visible = true;
            checkBox2.Visible = true;
            checkBox3.Visible = true;
            checkBox4.Visible = true;
            button1.Text = "Next";
            if (Textcounter > 0)
            {
                Textcounter--;
                ExamQuestionsText c = TextquestionList[Textcounter];
                label_question.Text = c.question_des;
                checkBox1.Visible = false;
                checkBox2.Visible = false;
                checkBox3.Visible = false;
                checkBox4.Visible = false;
                richTextBox1.Visible = true;
            }
            else if (tFcounter > 0)
            {
                tFcounter--;
                ExamQuestionsTF c = TFquestionList[tFcounter];
                label_question.Text = c.question_des;
                checkBox1.Text = "True";
                checkBox2.Text = "False";
                checkBox3.Visible = false;
                checkBox4.Visible = false;
            }
            else if (Chossecounter > 0)
            {
                Chossecounter--;
                ExamQuestionsChoice c = chossequestionList[Chossecounter];
                label_question.Text = c.question_des;
                checkBox1.Text = c.option1;
                checkBox2.Text = c.option2;
                checkBox3.Text = c.option3;
                checkBox4.Text = c.option4;
            }
            else
            {
                MessageBox.Show("No more Question !!");
            }

        }


        #region functions

        private void takechooseanswer(int index)
        {
            if (checkBox1.Checked)
            {
                AnswerChosserquestions[index, 0] = "A";
            }
            else if (checkBox2.Checked)
            {
                AnswerChosserquestions[index, 0] = "B";
            }
            else if (checkBox3.Checked)
            {
                AnswerChosserquestions[index, 0] = "C";
            }
            else if (checkBox4.Checked)
            {
                AnswerChosserquestions[index, 0] = "D";
            }
        }
        private void FillChooseCorrectData()
        {
            int index = 0;
            foreach (var item in chossequestionList)
            {
                AnswerChosserquestions[index, 1] = item.answerChoice;
                index++;
            }
        }
        private void calcChooseResult()
        {
            for (int k = 0; k < AnswerChosserquestions.GetLength(0); k++)
            {
                if (AnswerChosserquestions[k, 0] == AnswerChosserquestions[k, 1])
                {
                    resultOfChoose[k] = 10;
                }
                else
                    resultOfChoose[k] = 0;
            }

        }

        private void takeTFanswer(int index)
        {
            if (checkBox1.Checked)
            {
                AnswerTFquestions[index, 0] = true;
            }
            else if (checkBox2.Checked)
            {
                AnswerTFquestions[index, 0] = false;
            }


        }
        private void FillTFCorrectData()
        {
            int index = 0;
            foreach (var item in TFquestionList)
            {
                AnswerTFquestions[index, 1] = item.answerTF;
                index++;
            }
        }
        private void calcTfResult()
        {
            for (int k = 0; k < AnswerTFquestions.GetLength(0); k++)
            {
                if (AnswerTFquestions[k, 0] == AnswerTFquestions[k, 1])
                {
                    resultOfTf[k] = 5;
                }
                else
                    resultOfTf[k] = 0;
            }

        }

        private void takeTextanswer(int index)
        {
            AnswerTextquestions[index, 0] = richTextBox1.Text;
        }
        private void FillTextCorrectData()
        {
            int index = 0;
            foreach (var item in TextquestionList)
            {
                AnswerTextquestions[index, 1] = item.answerText;
                index++;
            }
        }
        private void calcTextResult()
        {
            for (int k = 0; k < AnswerTextquestions.GetLength(0); k++)
            {
                if (AnswerTextquestions[k, 0] == AnswerTextquestions[k, 1])
                {
                    resultOfText[k] = 20;
                }
                else
                    resultOfText[k] = 0;
            }

        }

        #endregion

        private void Form_exam_Load(object sender, EventArgs e)
        {
            chossequestionList = ctx.ExamQuestionsChoices.Where(ee => ee.exam_id == exam_id).ToList();
            TFquestionList = ctx.ExamQuestionsTFs.Where(ee => ee.exam_id == exam_id).ToList();
            TextquestionList = ctx.ExamQuestionsTexts.Where(ee => ee.exam_id == exam_id).ToList();

            AnswerChosserquestions = new string[chossequestionList.Count, 2];
            resultOfChoose = new int[chossequestionList.Count];

            AnswerTFquestions = new bool[TFquestionList.Count, 2];
            resultOfTf = new int[TFquestionList.Count];

            AnswerTextquestions = new string[TextquestionList.Count, 2];
            resultOfText = new int[TextquestionList.Count];

            FillChooseCorrectData();
            FillTextCorrectData();
            FillTFCorrectData();

            checkBox1.Visible = false;
            checkBox2.Visible = false;
            checkBox3.Visible = false;
            checkBox4.Visible = false;
            label1.Visible = false; label2.Visible = false;
            label3.Visible = false;
            label_question.Text = "Are You Ready";
            button1.Text = "start";
        }

        private void btn_backToStudentPage_Click(object sender, EventArgs e)
        {
            Form_logIn frm = new Form_logIn();
            frm.Show();
            this.Hide();
        }

        private void groupBox_question_Enter(object sender, EventArgs e)
        {

        }
    }
}
