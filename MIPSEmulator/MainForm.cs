using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MIPSEmulator
{
    public partial class MainForm : Form
    {
        public int pcReg = 1000;
        public OrderedDictionary instructionsMemory;
        public String[] instruction;
        MipsCpu mipsCpu;
        int[] pipelineValues;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            initialForm();
        }

        private void initialForm()
        {
            reg_GridView.Rows.Clear();
            reg_GridView.Rows.Add("$" + 0, 0);
            for (int i = 1; i < 32; i++)
            {
                reg_GridView.Rows.Add("$" + i, i + 100);
            }
        }

        private void refreshForm() {

            reg_GridView.Rows.Clear();
            reg_GridView.Rows.Add("$" + 0, 0);
            for (int i = 1; i < 32; i++)
            {
                reg_GridView.Rows.Add("$" + i, mipsCpu.mipsRegisters[i]);
            }
        }

        private void displayPipelineRegisters() {
            pipelineValues = mipsCpu.GetPipelineValues();

            pr_GridView.Rows.Clear();
            for (int i = 0; i < 30; i++)
            {
                pr_GridView.Rows.Add(i+1, pipelineValues[i] != -1 ? pipelineValues[i].ToString() : "__");
            }
        }

        private void initButton_Click(object sender, EventArgs e)
        {
            instructionsMemory = new OrderedDictionary();

            uint address = (uint)pcReg;
            String memWord;
            foreach (var line in userCodeTextBox.Lines)
            {
                instruction = line.Split(' ', '\n');
                memWord = "";
                for (int i = 0; i < instruction.Length; i++) memWord += instruction[i];

                if (memWord.Length != 32)
                {
                    MessageBox.Show("All Instructions Must Consist Of 32-bits!");
                    return;
                }

                instructionsMemory.Add(address, memWord);
                address += 4;
            }

            int extraCount = instructionsMemory != null ? pcReg + (instructionsMemory.Keys.Count * 4) : pcReg;
            for (int i = 0; i < 4; i++) instructionsMemory.Add(extraCount + (i*4), "00000000000000000000000000000000");

            mipsCpu = new MipsCpu(pcReg, instructionsMemory);
            initialForm();
        }

        private void runCycleButton_Click(object sender, EventArgs e)
        {
            if (mipsCpu == null) {
                MessageBox.Show("Please Make Sure To Initialise The Machine Code First!");
                return;
            }  
            
            try
            {
                displayPipelineRegisters();
                mipsCpu.Fetch();
                mipsCpu.Decode();
                mipsCpu.Execute();
                mipsCpu.WriteBack();
                refreshForm();
            }
            catch (Exception ex) {
                MessageBox.Show("Make sure you entered the machine code of the instruction correctly!");
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
