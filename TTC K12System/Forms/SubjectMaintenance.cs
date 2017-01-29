﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TTC_K12System.Forms
{
    public partial class SubjectMaintenance : Form
    {
        internal SubjectMaintenance()
        {
            InitializeComponent();
        }

        private Classes.Program program;
        List<Classes.Subject> subjects = new List<Classes.Subject>();
        Classes.Subject subject = new Classes.Subject();
        
        private void loadSubjects()
        {
            subjects = Classes.Subject.getAllByProgram(program.ID);
            dgvSubjects.Rows.Clear();
            foreach(Classes.Subject subject in subjects)
            {
                Classes.Program program = Classes.Program.getByID(subject.ProgramID);
                subject.ProgramID = program.ID;
                dgvSubjects.Rows.Add(subject.ID, subject.Type, subject.Code, subject.Title, subject.Hours);
            }
            dgvSubjects.ClearSelection();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            subject.ProgramID = program.ID;
            subject.Code = txtSubjectCode.Text;
            subject.Title = txtProgramTitle.Text;
            subject.Description = txtDescription.Text;
            subject.Save();
            loadSubjects();
        }

        private void btnChangeCourse_Click(object sender, EventArgs e)
        {
            ChooseProgram cp = new ChooseProgram();
            cp.ShowDialog();
            ChangeProgram(cp.program);
            loadSubjects();
        }

        private void ChangeProgram(Classes.Program program)
        {
            this.program = program;
            txtProgramTitle.Text = program.Title;
        }

        private void dgvSubjects_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvSubjects.SelectedRows.Count>0)
            {
                int id = Convert.ToInt32(dgvSubjects.SelectedRows[0].Cells[0].Value);
                subject = subjects.Find(p => p.ID == id);
            }
            else
            {
                subject = new Classes.Subject();
            }
            txtSubjectCode.Text = subject.Code;
            txtTitle.Text = subject.Title;
            txtDescription.Text = subject.Description;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.main.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
