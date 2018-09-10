using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Tree1
{
    public partial class Form1 : Form
    {
        MyTree myTree = new MyTree();
        
        bool drawing = false;
        bool flag = false;
        public Form1()
        {
            InitializeComponent();
            btnAdd.Enabled = false;
            btnDelete.Enabled = false;
            btnFind.Enabled = false;
            BtnSortAr.Enabled = false;
            btnSort.Enabled = false;
        }

        private void btnCreate_Click(object sender, EventArgs e)//СОздать дерево
        {
            //int L = richTextBox1.Lines.Count();
            //for(int i =0; i<L; i++)
            //{
            //    if (richTextBox1.Lines[i]!="")
            //    {
            //        int k = Convert.ToInt32(richTextBox1.Lines[i]);
            //        myTree.Insert(ref myTree.top, k, 200, 40);

            //    }
            //}
            //Random r = new Random();
            //for (int i = 0; i<1000; i++)
            //{
            //   // myTree.k = 15;
            //    int k = r.Next(0, 100);
            //    myTree.Insert(ref myTree.top, k, 200, 40);
            //}
             myTree = new MyTree();
            OpenFileDialog OPF = new OpenFileDialog();
            OPF.Filter = "Файлы txt|*.txt";
            if (OPF.ShowDialog() == DialogResult.OK)
            {
                string name = OPF.FileName;
                StreamReader sr = new System.IO.StreamReader(name, Encoding.Default);
                string line = null;
                while ((line = sr.ReadLine()) != null)
                {
                    String[] words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string s in words)
                    {
                        myTree.Insert(ref myTree.top, s, 200, 40);
                    }
                }
                sr.Close();
                
                  myTree.g = this.CreateGraphics();
                myTree.g.Clear(Color.WhiteSmoke);
                myTree.DrawNode(myTree.top);
                myTree.SetStrSort(myTree.top);
                richTextBox1.Text = "";

                foreach (KeyValuePair<string, int> keyValue in myTree.array)
                {
                    richTextBox1.Text += keyValue.Key + " : " + keyValue.Value + "\r\n";
                }
                richTextBox1.Enabled = true;
            }

            //btnAdd.Enabled = true;
            //btnDelete.Enabled = true;
            //btnFind.Enabled = true;
            //BtnSortAr.Enabled = true;
        }

        void MyDraw()
        {

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)//щелк
        {
            if (myTree.selectNode!=null)
            myTree.selectNode.visit = false;
            myTree.DeSelect(myTree.top);
            myTree.selectNode = myTree.FindNode(myTree.top, e.X, e.Y);
             drawing = (myTree.selectNode != null);
            if (drawing)
            {
                myTree.selectNode.visit = true;
                flag = true;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (flag)
            {
                if (drawing)
                    myTree.Delta(myTree.selectNode, myTree.selectNode.x - e.X, myTree.selectNode.y - e.Y);
                else
                {
                    myTree.DeSelect(myTree.top);
                    myTree.selectNode = myTree.FindNode(myTree.top, e.X, e.Y);
                    if (myTree.selectNode != null)
                        myTree.selectNode.visit = true;
                }
                myTree.g.Clear(Color.WhiteSmoke);
                myTree.g = this.CreateGraphics();
                myTree.DrawNode(myTree.top);


             //myTree.Draw(myTree.selectNode);
            }
        }//Двиг

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (flag)
            {
                drawing = false;
                flag = false;
            }
        }//Разщелк

        private void BtnSortAr_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.Text = "";
               // richTextBox1.Text = myTree.SetStrSort(myTree.top);
            }
            catch { }
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int data = Convert.ToInt32(tbDelete.Text);
                myTree.Delete(data, ref myTree.top);
                myTree.g.Clear(Color.WhiteSmoke);
                myTree.g = this.CreateGraphics();
                myTree.DrawNode(myTree.top);
            }
            catch
            { }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        { try
            {
                int data = Convert.ToInt32(tbAdd.Text);
                myTree.Insert(ref myTree.top, data, myTree.top.x, myTree.top.y);
                myTree.g.Clear(Color.WhiteSmoke);
                myTree.g = this.CreateGraphics();
                myTree.DrawNode(myTree.top);
            }
            catch
            { }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                int data = Convert.ToInt32(tbFind.Text);
                if (myTree.selectNode != null)
                    myTree.selectNode.visit = false;
                myTree.selectNode = myTree.FindNodeVal(data, myTree.top);
                if (myTree.selectNode != null)
                    myTree.selectNode.visit = true;
                myTree.g.Clear(Color.WhiteSmoke);
                myTree.g = this.CreateGraphics();
                myTree.DrawNode(myTree.top);
            }
            catch
            { }
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            
        //    List<int> val = new List<int>();
        //    if (myTree.selectNode != null)
        //        myTree.selectNode.visit = true;
        //    //while(myTree.top!=null)
        //    //{
        //    //    Node node = myTree.Competition(ref myTree.top);
        //    //    if (node!=null)
        //    //    val.Add(Convert.ToInt32(node.data));

        //    //}
        //    myTree.Competition(ref myTree.top);
        //    myTree.g.Clear(Color.WhiteSmoke);
        //    myTree.g = this.CreateGraphics();

            
        //    for (int i = 0; i < val.Count; i++)
        //    {

        //        int k = val[i];
        //            myTree.Insert(ref myTree.top, k, 200, 40);

                
        //    }
        //myTree.DrawNode(myTree.top);
        }
    }
}
