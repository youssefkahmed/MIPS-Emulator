using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MIPSEmulator
{
    class MipsCpu
    {
        // PC Register
        public int pcReg;

        // Array to hold all MIPS Registers
        public  int[] mipsRegisters = new int[32];

        // "PR" stands for Pipeline Register
        public String IFID, IDEX, EXMEM, MEMWB;

        // Table that hold all Instructions in Memory
        private OrderedDictionary instMemory;

        // Control Signals
        int pcSrc, regWrite, aluOp, aluSrc,
            regDst, memWrite, memRead, memToReg;

        // Helper Variables
        private int startAddress;
        private bool isBranch = false;
        private bool branchGUI = false;
        private int fetchNew = 0;
        private int branchValue = 0;
        private int currentIndex = -1;

        public MipsCpu(int PcReg, OrderedDictionary instructions)
        {
            this.pcReg = PcReg;
            this.mipsRegisters[0] = 0;
            for (int i = 1; i < mipsRegisters.Length; i++) this.mipsRegisters[i] = i + 100;
            this.instMemory = instructions;
            IFID = IDEX = EXMEM = MEMWB = "0";
            startAddress = PcReg;
        }




        #region MIPS Components

        public void ControlUnit(char opType) { 
            if(opType == 'r')
            {
                this.regDst = 1;
                this.memToReg = 0;
                this.regWrite = 1;
                this.aluOp = 10;

            }

            else
            {
                this.regDst = -1;
                this.memToReg = -1;
                this.regWrite = 0;
                this.aluOp = 01;
            }

            this.aluSrc = 0;
            this.memWrite = 0;
            this.memRead = 0;
        }

        public int Adder(int val1, int val2) {
            return val1 + val2;
        }

        public int[] RegisterFile(int reg1, int reg2) {
            int[] readData = { this.mipsRegisters[reg1], this.mipsRegisters[reg2] };
            return readData;
        }

        public int Mux2x1(int val1, int val2, int selector) {
            if (selector == 0) return val1;
            else if (selector == 1) return val2;
            else return -1;
        }

        public int ALU(int val1, int val2, String operatiion) {

            int finalVal;
           // char opType;
            switch (operatiion) {
                case "and":
                    finalVal = val1 & val2;
                    //opType = 'r';
                    break;
                case "or":
                    finalVal = val1 | val2;
                    //opType = 'r';
                    break;
                case "add":
                    finalVal = val1 + val2;
                    //opType = 'r';
                    break;
                case "sub":
                    finalVal = Math.Abs(val1 - val2);
                    //opType = 'r';
                    break;
                case "beq":
                    finalVal = Math.Abs(val1 - val2);
                    //opType = 'i';
                    break;
                default:
                    finalVal = 0;
                    //opType = 'r';
                    break;
            }

            //if (opType == 'r') this.mipsRegisters[writeReg] = finalVal;

            return finalVal;
        }

        #endregion


        #region Extra Functions
        private int[] TranslateInstruction(String instruction) {
            
            int[] translatedInstruction = new int[6];
            bool isImmediate = false;

            String op = "", rs = "", rt = "", rd = "", shamt= "", funct = "", immediate = "";


            for (int i = 0; i < 6; i++)
            {
                op += instruction[i];
            }

            translatedInstruction[0] = Convert.ToInt32(op, 2);
            if (translatedInstruction[0] != 0) isImmediate = true;

            for (int i = 6; i < 16; i++)
            {
                if (i < 11) rs += instruction[i];
                else rt += instruction[i];
            }
            translatedInstruction[1] = Convert.ToInt32(rs, 2);
            translatedInstruction[2] = Convert.ToInt32(rt, 2);


            if (isImmediate) {
                for (int i = 16; i < 32; i++)
                {
                    immediate += instruction[i];
                }

                translatedInstruction[3] = Convert.ToInt32(immediate, 2);
                return translatedInstruction;
            }

            for (int i = 16; i < 32; i++)
            {
                if (i < 21) rd += instruction[i];
                else if (i < 26) shamt += instruction[i];
                else funct += instruction[i];
            }

            translatedInstruction[3] = Convert.ToInt32(rd, 2);
            translatedInstruction[4] = Convert.ToInt32(shamt, 2);
            translatedInstruction[5] = Convert.ToInt32(funct, 2);

            return translatedInstruction;

        }

        public void Fetch()
        {
            if (!isBranch)
            {
                MEMWB = EXMEM;
                EXMEM = IDEX;
                IDEX = IFID;
            }
            else {
                MEMWB = EXMEM = IDEX = "0";
                isBranch = false;
                branchGUI = true;
                pcReg = branchValue;
            }

            //if (currentIndex >= instMemory.Keys.Count) {
            //    IFID = "0"; return;
            //}
            //String currentInstruction = instMemory[currentIndex++].ToString();

            bool keyExists = false;
            foreach (var key in instMemory.Keys)
            {
                if (key.ToString() == pcReg.ToString()) { keyExists = true; break;}

            }

            if (!keyExists)
            {
                IFID = "0"; return;
                
            }

            currentIndex = (pcReg - startAddress) / 4;
            String currentInstruction = instMemory[currentIndex].ToString();
            int[] translatedInstruction = TranslateInstruction(currentInstruction);
            int[] readData = RegisterFile(translatedInstruction[1], translatedInstruction[2]);

            IFID = translatedInstruction[0].ToString() + " "        // 0 - Op Code
                 + readData[0].ToString() + " "                     // 1 - Read Data 1
                 + readData[1].ToString() + " "                     // 2 - Read Data 2
                 + translatedInstruction[2].ToString() + " "        // 3 - RT
                 + translatedInstruction[3].ToString() + " "        // 4 - RD
                 + (pcReg + 4).ToString() + " "                     // 5 - PC Register + 4
                 + translatedInstruction[5].ToString() + " "       // 6 - Funct
                 + translatedInstruction[1].ToString() + " ";      // 7 - RS
            pcReg += 4;
            //if (translatedInstruction[0] != 0) IFID += " " + translatedInstruction[3].ToString();



            if (fetchNew != 0)
            {
                if (fetchNew == 1)
                {
                    IFID = "0";
                    fetchNew = 0;
                    pcReg -= 4;
                }
                else
                {
                    fetchNew++;
                }
            }

        }


        public void Decode() {
            if (IDEX == "0") return;

            String[] instruction = IDEX.Split(' ');
            char op = instruction[0] == "0" ? 'r' : 'i';
            ControlUnit(op);

            int val1 = int.Parse(instruction[2]);
            int val2 = int.Parse(instruction[4]);
            int muxValue = Mux2x1(val1, val2, aluSrc);

            String operation;
            switch (instruction[6]) {
                case "32":
                    operation = "add";
                    break;
                case "34":
                    operation = "sub";
                    break;
                case "36":
                    operation = "and";
                    break;
                case "37":
                    operation = "or";
                    break;
                default:
                    operation = "add";
                    break;
            }
            operation = op != 'r' ? "beq" : operation;


            int aluValue = ALU(int.Parse(instruction[1]), muxValue, operation);

            val1 = int.Parse(instruction[3]);
            val2 = int.Parse(instruction[4]);
            muxValue = Mux2x1(val1, val2, regDst);


            IDEX =  instruction[0].ToString() + " "           // 0 - Op Code
                  + aluValue.ToString() + " "                 // 1 - ALU Result
                  + instruction[2].ToString() + " "           // 2 - Read Data 2
                  + muxValue.ToString() + " ";                // 3 - Mux Result

            int addValue;
            if (operation == "beq") {
                val1 = int.Parse(instruction[4]) * 4;
                val2 = int.Parse(instruction[5]);
                addValue = Adder(val1, val2);

                IDEX += addValue.ToString();                // 4 - Adder Value
            }
        }


        public void Execute() {
            if (EXMEM == "0") return;

            String[] instruction = EXMEM.Split(' ');
            char op = instruction[0] == "0" ? 'r' : 'i';
            ControlUnit(op);

            if (op == 'i' && instruction[1] == "0")
            {
                // pcReg = int.Parse(instruction[4]);
                //IDEX = EXMEM = IFID = "0";
                // IFID = "0";
                fetchNew = 1;
                isBranch = true;
                branchValue = int.Parse(instruction[4]);
                return;
            }
            else if (op == 'i') { EXMEM = "0"; return; }

            EXMEM = instruction[0].ToString() + " "             // 0 - Op Code             
                  + instruction[1].ToString() + " "             // 1 - ALU Result
                  + instruction[2].ToString() + " "             // 2 - Read Data 2
                  + instruction[3].ToString() + " ";            // 3 - Mux Result

            
        }


        public void WriteBack() {
            if (MEMWB == "0") return;

            String[] instruction = MEMWB.Split(' ');
            char op = instruction[0] == "0" ? 'r' : 'i';
            ControlUnit(op);

            int val1 = int.Parse(instruction[1]);
            int val2 = int.Parse(instruction[2]);
            int writeData = Mux2x1(val1, val2, memToReg);

            int writeRegister = int.Parse(instruction[3]);
            mipsRegisters[writeRegister] = writeData;
        }


        public int[] GetPipelineValues() {

            int[] pipelineValues = new int[30];

            int currentAddress = (currentIndex * 4) + startAddress;
            pipelineValues[0] = currentIndex != -1 ? currentAddress + 4 : pcReg;
            pipelineValues[1] = currentIndex != -1 ? currentAddress + 8 : pcReg + 4;

            if (branchGUI) {
                pipelineValues[0] -= 4;
                pipelineValues[1] -= 4;
               branchGUI = false;

                
            }
        
            if (IFID != "0")
            {
                String[] instruction = IFID.Split(' ');
                pipelineValues[2] = int.Parse(instruction[7]);                                                       // RS
                pipelineValues[3] = int.Parse(instruction[3]);                                                      // RT
                pipelineValues[4] = int.Parse(instruction[1]);                                                     // Read data 1
                pipelineValues[5] = int.Parse(instruction[2]);                                                    // Read data 2
                pipelineValues[6] = instruction[0] == "0" ? int.Parse(instruction[4]) : -1;                       // Write Register
                pipelineValues[7] = instruction[0] == "0" ? mipsRegisters[int.Parse(instruction[4])] : -1;        // Write Data
                pipelineValues[8] = instruction[0] == "0" ? -1 : int.Parse(instruction[4]);                        // Immediate
                pipelineValues[9] = instruction[0] == "0" ? int.Parse(instruction[3]) : -1;                       // RT
                pipelineValues[10] = instruction[0] == "0" ? int.Parse(instruction[4]) : -1;                      // RD

            }

            else {
                pipelineValues[2] = -1;
                pipelineValues[3] = -1;
                pipelineValues[4] = -1;
                pipelineValues[5] = -1;
                pipelineValues[6] = -1;
                pipelineValues[7] = -1;
                pipelineValues[8] = -1;
                pipelineValues[9] = -1;
                pipelineValues[10] = -1;
            }



            if (IDEX != "0")
            {
                String[] instruction = IDEX.Split(' ');
                pipelineValues[11] = pipelineValues[4];                                                       // RS
                pipelineValues[12] = pipelineValues[5];                                                      // RT
                pipelineValues[13] = pipelineValues[8];                                                      // RT
                pipelineValues[14] = instruction[0] == "0" ? pipelineValues[12] : pipelineValues[13];        // Mux Value                                              // RT
                pipelineValues[15] = int.Parse(instruction[1]);                                              // ALU value
                pipelineValues[16] = pipelineValues[9];                                                     // RT
                pipelineValues[17] = pipelineValues[10];                                                    // RD
                pipelineValues[18] = instruction[0] == "0" ? pipelineValues[16] : -1;                       // RT
                pipelineValues[19] = pcReg - 4;                                                             // PC Reg
                pipelineValues[20] = instruction[0] == "0" ? -1 : int.Parse(instruction[4]);                // Adder value

            }

            else{
                
                pipelineValues[11] = -1;
                pipelineValues[12] = -1;
                pipelineValues[13] = -1;
                pipelineValues[14] = -1;
                pipelineValues[15] = -1;
                pipelineValues[16] = -1;
                pipelineValues[17] = -1;
                pipelineValues[18] = -1;
                pipelineValues[19] = -1;
                pipelineValues[20] = -1;
            }


            if (EXMEM != "0")
            {
                String[] instruction = EXMEM.Split(' ');
                pipelineValues[21] = pipelineValues[20];                                                       // Adder Value
                pipelineValues[22] = pipelineValues[15];                                                      // ALU Value
                pipelineValues[23] = pipelineValues[12];                                                      // Read Data 2
                pipelineValues[24] = pipelineValues[23];                                                      // Read Data 2
                pipelineValues[25] = pipelineValues[18];                                                      // Mux Value
            }

            else
            {

                pipelineValues[21] = -1;
                pipelineValues[22] = -1;
                pipelineValues[23] = -1;
                pipelineValues[24] = -1;
                pipelineValues[25] = -1;
            }


            if (MEMWB != "0")
            {
                String[] instruction = MEMWB.Split(' ');
                pipelineValues[26] = pipelineValues[24];                                                       // Read Data 2
                pipelineValues[27] = pipelineValues[22];                                                      // ALU Value
                pipelineValues[28] = instruction[0] == "0" ? pipelineValues[27] : -1;                         // ALU Value
                pipelineValues[29] = pipelineValues[25];                                                      // RD
            }

            else
            {

                pipelineValues[26] = -1;
                pipelineValues[27] = -1;
                pipelineValues[28] = -1;
                pipelineValues[29] = -1;
            }


            return pipelineValues;
        }
        #endregion


    }
}
