using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPLApplication
{
    public partial class MainForm : Form
    {
        Graphics g;
        CommandValidation cvalidate;
        /// <summary>
        /// Load all the program logic 
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            g = Output_Box.CreateGraphics();
        }
        /// <summary>
        /// variables to generate different shapes through the commands provided by the users
        /// </summary>
        ShapesCreator factory = new ShapesFactory();
        Pen myPen = new Pen(Color.Red);
        public Color newcolor;
        int x = 0, y = 0;
        int counterLoop;
        public int counter = 0;
        public int dgSize = 0;
        public int radius = 0;
        public int width = 0;
        public int height = 0;

        /// <summary>
        /// used to save the ruuning commands in the specific drive as txt file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "TXT files (.txt)|*.txt|All files (.*)|*.*";
            if (save.ShowDialog() == DialogResult.OK)
            {
                StreamWriter write = new StreamWriter(File.Create(save.FileName));
                write.WriteLine(txt_Command_Box.Text);
                write.Close();
                MessageBox.Show("File has been saved successfully!");
            }
        }

        /// <summary>
        /// used for loading the commands that were saved in the txt files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Title = "Browse file from specified folder";
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "TXT files (.txt)|*.txt|All files (.*)|*.*";
            openFileDialog1.Filter = "DOCX files (.docx)|*.docx|All files (.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            //Browse .txt file from computer             
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            // Insert code to read the stream here.                        
                        }
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
                //displays the text inside the file on TextBox named as txtInput                
                txt_Command_Box.Text = File.ReadAllText(openFileDialog1.FileName);
            }
        }

        /// <summary>
        /// used to close the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// used to provide information about the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string des = "GPL Application. Sudip Bhandari";
            MessageBox.Show(des);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// used to provide guidelines about the application and commands that can be used in the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void applicationGuideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("file:///D:/GPLApplicationCommands.pdf");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: Could not read file from disk." + ex.Message);
            }



        }

        /// <summary>
        /// check the validation and run the program logic as per the commands provided in the execution box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_Execution_Box_TextChanged(object sender, EventArgs e)
        {

            if (txt_Execution_Box.Text.ToLower().Trim() == "run")
            {
                if (txt_Command_Box.Text != null && txt_Command_Box.Text != "")
                {
                    cvalidate = new CommandValidation(txt_Command_Box);

                    if (!cvalidate.isSomethingInvalid)
                    {

                        commandLoad();
                    }
                }


            }
            else
            {
                if (txt_Execution_Box.Text.ToLower().Trim() == "clear")
                {
                    Output_Box.Invalidate();

                }
                else if (txt_Execution_Box.Text.ToLower().Trim() == "reset")
                {
                    txt_Command_Box.Clear();
                }
            }
        }
        /// <summary>
        /// used to load the commands in the output panel
        /// </summary>
        private void commandLoad()
        {
            Graphics g = Output_Box.CreateGraphics();
            string command = txt_Command_Box.Text.ToLower();
            string[] commandline = command.Split(new String[] { "\n" },

            StringSplitOptions.RemoveEmptyEntries);
            int numberOfLines = txt_Command_Box.Lines.Length;
            for (int k = 0; k < commandline.Length; k++)
            {
                string[] cmd = commandline[k].Split(' ');
                if (cmd[0].Equals("moveto") == true)
                {
                    Output_Box.Refresh();
                    string[] param = cmd[1].Split(',');
                    if (param.Length != 2)
                    { 
                        MessageBox.Show("Incorrect Parameter"); 
                    }
                    else
                    {
                        Int32.TryParse(param[0], out x);
                        Int32.TryParse(param[1], out y);
                        moveTo(x, y);
                    }

                }

                for (counterLoop = 0; counterLoop < numberOfLines; counterLoop++)
                {
                    String oneLineCommand = txt_Command_Box.Lines[counterLoop];
                    oneLineCommand = oneLineCommand.Trim();
                    if (!oneLineCommand.Equals(""))
                    {
                        commandRun(oneLineCommand);
                    }

                }
            }
        }
        /// <summary>
        /// the codes are executed as per the users input after run command is provided in the execution box
        /// </summary>
        /// <param name="oneLineCommand"></param>
        private void commandRun(String oneLineCommand)
        {
            Boolean hasPlus = oneLineCommand.Contains("+");
            Boolean hasEquals = oneLineCommand.Contains("=");
            if (hasEquals)
            {
                oneLineCommand = Regex.Replace(oneLineCommand, @"\s+", " ");
                string[] cmd = oneLineCommand.Split(' ');
                for (int i = 0; i < cmd.Length; i++)
                {
                    cmd[i] = cmd[i].Trim();
                }
                String firstWord = cmd[0].ToLower();
                if (firstWord.Equals("if"))
                {
                    Boolean loop = false;
                    if (cmd[1].ToLower().Equals("radius"))
                    {
                        if (radius == int.Parse(cmd[3]))
                        {
                            loop = true;
                        }
                    }
                    else if (cmd[1].ToLower().Equals("width"))
                    {
                        if (width == int.Parse(cmd[3]))
                        {
                            loop = true;
                        }
                    }
                    else if (cmd[1].ToLower().Equals("height"))
                    {
                        if (height == int.Parse(cmd[3]))
                        {
                            loop = true;
                        }

                    }
                    else if (cmd[1].ToLower().Equals("counter"))
                    {
                        if (counter == int.Parse(cmd[3]))
                        {
                            loop = true;
                        }
                    }
                    int ifStartLine = (getIfStartLineNumber());
                    int ifEndLine = (getEndifEndLineNumber() - 1);
                    counterLoop = ifEndLine;
                    if (loop)
                    {
                        for (int j = ifStartLine; j <= ifEndLine; j++)
                        {
                            string oneLineCommand1 = txt_Command_Box.Lines[j];
                            oneLineCommand1 = oneLineCommand1.Trim();
                            if (!oneLineCommand1.Equals(""))
                            {
                                commandRun(oneLineCommand1);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Provided if statement is false");
                    }
                }
                else
                {
                    string[] cmd2 = oneLineCommand.Split('=');
                    for (int j = 0; j < cmd2.Length; j++)
                    {
                        cmd2[j] = cmd2[j].Trim();
                    }
                    if (cmd2[0].ToLower().Equals("radius"))
                    {
                        radius = int.Parse(cmd2[1]);
                    }
                    else if (cmd2[0].ToLower().Equals("width"))
                    {
                        width = int.Parse(cmd2[1]);
                    }
                    else if (cmd2[0].ToLower().Equals("height"))
                    {
                        height = int.Parse(cmd2[1]);
                    }
                    else if (cmd2[0].ToLower().Equals("counter"))
                    {
                        counter = int.Parse(cmd2[1]);
                    }
                }
            }
            else if (hasPlus)
            {
                oneLineCommand = System.Text.RegularExpressions.Regex.Replace(oneLineCommand, @"\s+", " ");
                string[] cmd = oneLineCommand.Split(' ');
                if (cmd[0].ToLower().Equals("repeat"))
                {
                    counter = int.Parse(cmd[1]);
                    if (cmd[2].ToLower().Equals("circle"))
                    {
                        int increaseValue = getSize(oneLineCommand);
                        radius = increaseValue;
                        for (int j = 0; j < counter; j++)
                        {
                            drawCircle(radius);
                            radius += increaseValue;
                        }
                    }
                    else if (cmd[2].ToLower().Equals("rectangle"))
                    {
                        int increaseValue = getSize(oneLineCommand);
                        dgSize = increaseValue;
                        for (int j = 0; j < counter; j++)
                        {
                            drawRectangle(dgSize, dgSize);
                            dgSize += increaseValue;
                        }
                    }
                    else if (cmd[2].ToLower().Equals("triangle"))
                    {
                        int increaseValue = getSize(oneLineCommand);
                        dgSize = increaseValue;
                        for (int j = 0; j < counter; j++)
                        {
                            drawTriangle(dgSize, dgSize, dgSize);
                            dgSize += increaseValue;
                        }
                    }
                }
                else
                {
                    string[] cmd2 = oneLineCommand.Split('+');
                    for (int j = 0; j < cmd2.Length; j++)
                    {
                        cmd2[j] = cmd2[j].Trim();
                    }
                    if (cmd2[0].ToLower().Equals("radius"))
                    {
                        radius += int.Parse(cmd2[1]);
                    }
                    else if (cmd2[0].ToLower().Equals("width"))
                    {
                        width += int.Parse(cmd2[1]);
                    }
                    else if (cmd2[0].ToLower().Equals("height"))
                    {
                        height += int.Parse(cmd2[1]);
                    }
                }
            }
            else
            {
                generateDrawCommand(oneLineCommand);
            }


        }
        /// <summary>
        /// used for returning the size of the shapes as per the commands provided
        /// </summary>
        /// <param name="lineCommand"></param>
        /// <returns></returns>
        private int getSize(string lineCommand)
        {
            int value = 0;
            if (lineCommand.ToLower().Contains("radius"))
            {
                int pos = (lineCommand.IndexOf("radius") + 6);
                int size = lineCommand.Length;
                String tempLine = lineCommand.Substring(pos, (size - pos));
                tempLine = tempLine.Trim();
                String newTempLine = tempLine.Substring(1, (tempLine.Length - 1));
                newTempLine = newTempLine.Trim();
                value = int.Parse(newTempLine);

            }
            else if (lineCommand.ToLower().Contains("size"))
            {
                int pos = (lineCommand.IndexOf("size") + 4);
                int size = lineCommand.Length;
                String tempLine = lineCommand.Substring(pos, (size - pos));
                tempLine = tempLine.Trim();
                String newTempLine = tempLine.Substring(1, (tempLine.Length - 1));
                newTempLine = newTempLine.Trim();
                value = int.Parse(newTempLine);
            }
            return value;
        }
        /// <summary>
        /// Initiate the shapes as the per the commands given by the user in command box
        /// </summary>
        /// <param name="lineOfCommand"></param>
        private void generateDrawCommand(string lineOfCommand)
        {
            String[] shapes = { "circle", "rectangle", "triangle" };
            String[] variable = { "radius", "width", "height", "counter", "size" };

            lineOfCommand = System.Text.RegularExpressions.Regex.Replace(lineOfCommand, @"\s+", " ");
            string[] cmd = lineOfCommand.Split(' ');
            for (int i = 0; i < cmd.Length; i++)
            {
                cmd[i] = cmd[i].Trim();
            }
            String firstWord = cmd[0].ToLower();
            Boolean firstcmdhape = shapes.Contains(firstWord);
            if (firstcmdhape)
            {

                if (firstWord.Equals("circle"))
                {
                    Boolean secondWordIsVariable = variable.Contains(cmd[1].ToLower());
                    if (secondWordIsVariable)
                    {
                        if (cmd[1].ToLower().Equals("radius"))
                        {
                            drawCircle(radius);
                        }
                    }
                    else
                    {
                        drawCircle(Int32.Parse(cmd[1]));
                    }

                }
                else if (firstWord.Equals("rectangle"))
                {
                    String args = lineOfCommand.Substring(9, (lineOfCommand.Length - 9));
                    String[] parms = args.Split(',');
                    for (int i = 0; i < parms.Length; i++)
                    {
                        parms[i] = parms[i].Trim();
                    }
                    Boolean secondWordIsVariable = variable.Contains(parms[0].ToLower());
                    Boolean thirdWordIsVariable = variable.Contains(parms[1].ToLower());
                    if (secondWordIsVariable)
                    {
                        if (thirdWordIsVariable)
                        {
                            drawRectangle(width, height);
                        }
                        else
                        {
                            drawRectangle(width, Int32.Parse(parms[1]));
                        }

                    }
                    else
                    {
                        if (thirdWordIsVariable)
                        {
                            drawRectangle(Int32.Parse(parms[0]), height);
                        }
                        else
                        {
                            drawRectangle(Int32.Parse(parms[0]), Int32.Parse(parms[1]));
                        }
                    }
                }
                else if (firstWord.Equals("triangle"))
                {
                    String args = lineOfCommand.Substring(8, (lineOfCommand.Length - 8));
                    String[] parms = args.Split(',');
                    for (int i = 0; i < parms.Length; i++)
                    {
                        parms[i] = parms[i].Trim();
                    }
                    drawTriangle(Int32.Parse(parms[0]), Int32.Parse(parms[1]), Int32.Parse(parms[2]));
                }

            }
            else
            {
                if (firstWord.Equals("loop"))
                {
                    counter = int.Parse(cmd[1]);
                    int loopStartLine = (getLoopStartLineNumber());
                    int loopEndLine = (getLoopEndLineNumber() - 1);
                    counterLoop = loopEndLine;
                    for (int i = 0; i < counter; i++)
                    {
                        for (int j = loopStartLine; j <= loopEndLine; j++)
                        {
                            String oneLineCommand = txt_Command_Box.Lines[j];
                            oneLineCommand = oneLineCommand.Trim();
                            if (!oneLineCommand.Equals(""))
                            {
                                commandRun(oneLineCommand);
                            }
                        }
                    }
                }
                else if (firstWord.Equals("if"))
                {
                    Boolean loop = false;
                    if (cmd[1].ToLower().Equals("radius"))
                    {
                        if (radius == int.Parse(cmd[1]))
                        {
                            loop = true;
                        }
                    }
                    else if (cmd[1].ToLower().Equals("width"))
                    {
                        if (width == int.Parse(cmd[1]))
                        {
                            loop = true;
                        }
                    }
                    else if (cmd[1].ToLower().Equals("height"))
                    {
                        if (height == int.Parse(cmd[1]))
                        {
                            loop = true;
                        }

                    }
                    else if (cmd[1].ToLower().Equals("counter"))
                    {
                        if (counter == int.Parse(cmd[1]))
                        {
                            loop = true;
                        }
                    }
                    int ifStartLine = (getIfStartLineNumber());
                    int ifEndLine = (getEndifEndLineNumber() - 1);
                    counterLoop = ifEndLine;
                    if (loop)
                    {
                        for (int j = ifStartLine; j <= ifEndLine; j++)
                        {
                            String oneLineCommand = txt_Command_Box.Lines[j];
                            oneLineCommand = oneLineCommand.Trim();
                            if (!oneLineCommand.Equals(""))
                            {
                                commandRun(oneLineCommand);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Initiate whether if statement is present in the commands given in the command box
        /// </summary>
        /// <returns></returns>
        private int getIfStartLineNumber()
        {
            int numberOfLines = txt_Command_Box.Lines.Length;
            int lineNum = 0;

            for (int i = 0; i < numberOfLines; i++)
            {
                String oneLineCommand = txt_Command_Box.Lines[i];
                oneLineCommand = Regex.Replace(oneLineCommand, @"\s+", " ");
                string[] cmd = oneLineCommand.Split(' ');
                //removing white spaces in between cmd
                for (int j = 0; j < cmd.Length; j++)
                {
                    cmd[j] = cmd[j].Trim();
                }
                String firstWord = cmd[0].ToLower();
                oneLineCommand = oneLineCommand.Trim();
                if (firstWord.Equals("if"))
                {
                    lineNum = i + 1;

                }
            }
            return lineNum;
        }

        /// <summary>
        /// Determine whether the if statment ended with the endif statement
        /// </summary>
        /// <returns></returns>
        private int getEndifEndLineNumber()
        {
            int numberOfLines = txt_Command_Box.Lines.Length;
            int lineNum = 0;

            for (int i = 0; i < numberOfLines; i++)
            {
                String oneLineCommand = txt_Command_Box.Lines[i];
                oneLineCommand = oneLineCommand.Trim();
                if (oneLineCommand.ToLower().Equals("endif"))
                {
                    lineNum = i + 1;

                }
            }
            return lineNum;
        }

        /// <summary>
        /// Initiate the loop as per the command given in the command box
        /// </summary>
        /// <returns></returns>
        private int getLoopStartLineNumber()
        {
            int numberOfLines = txt_Command_Box.Lines.Length;
            int lineNum = 0;

            for (int i = 0; i < numberOfLines; i++)
            {
                String oneLineCommand = txt_Command_Box.Lines[i];
                oneLineCommand = Regex.Replace(oneLineCommand, @"\s+", " ");
                string[] cmd = oneLineCommand.Split(' ');
                //removing white spaces in between cmd
                for (int j = 0; j < cmd.Length; j++)
                {
                    cmd[j] = cmd[j].Trim();
                }
                String firstWord = cmd[0].ToLower();
                oneLineCommand = oneLineCommand.Trim();
                if (firstWord.Equals("loop"))
                {
                    lineNum = i + 1;

                }
            }
            return lineNum;

        }

        /// <summary>
        /// Determine whether the loop has ended with end looop
        /// </summary>
        /// <returns></returns>
        private int getLoopEndLineNumber()
        {
            try
            {
                int numberOfLines = txt_Command_Box.Lines.Length;
                int lineNum = 0;

                for (int i = 0; i < numberOfLines; i++)
                {
                    String oneLineCommand = txt_Command_Box.Lines[i];
                    oneLineCommand = oneLineCommand.Trim();
                    if (oneLineCommand.ToLower().Equals("endloop"))
                    {
                        lineNum = i + 1;

                    }
                }
                return lineNum;
            }
            catch (Exception e)
            {

                return 0;
              
            }
        }

        /// <summary>
        /// Draw the rectangle as per the command provided
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private void drawRectangle(int width, int height)
        {
            Pen p = new Pen(Color.Red, 2);
            g.DrawRectangle(p, x - (width / 2), y - (height / 2), width * 2, height * 2);
        }

        /// <summary>
        /// Draw circle as per the command provided
        /// </summary>
        /// <param name="radius"></param>
        private void drawCircle(int radius)
        {
            Pen p = new Pen(Color.Aquamarine, 2);
            g.DrawEllipse(p, x - radius, y - radius, radius * 2, radius * 2);
        }

        /// <summary>
        /// Draw triangle as per the command provided
        /// </summary>
        /// <param name="rBase"></param>
        /// <param name="adj"></param>
        /// <param name="hyp"></param>
        private void drawTriangle(int rBase, int adj, int hyp)
        {
            Pen po = new Pen(Color.RosyBrown, 2);
            Point[] pnt = new Point[3];

            pnt[0].X = x;
            pnt[0].Y = y;

            pnt[1].X = x - rBase;
            pnt[1].Y = y;

            pnt[2].X = x;
            pnt[2].Y = y - adj;
            g.DrawPolygon(po, pnt);
        }

        /// <summary>
        /// Display the values of X-axis and Y-axis
        /// </summary>
        /// <param name="toX"></param>
        /// <param name="toY"></param>
        public void moveTo(int toX, int toY)
        {
            x = toX;
            y = toY;
        }

        /// <summary>
        /// Draw the pen position as per the X-cordinate and Y-cordinate
        /// </summary>
        /// <param name="toX"></param>
        /// <param name="toY"></param>
        public void drawTo(int toX, int toY)
        {
            x = toX;
            y = toY;
        }

    }
}

