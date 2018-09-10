using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tree1
{
   public class MyTree
    {
        public Node top;
        public Node selectNode = null;
        public Bitmap bitmap;
        public  Node node;
       public int k = 1;
        public Dictionary<string, int> array = new Dictionary<string, int>();

        const int step = 20;
        const int Geom = 1;
        Node q;
       public Graphics g;
        Pen MyPen = new Pen(Color.Black);
        SolidBrush MyBrush;
        Font MyFont = new Font("Arial",7);
        public void Search(int val)//Поиск и вставка
        { }
        public Node FindNode (Node p, int x, int y)//Поиск по координатам
        {

            Node result = null;
            if (p == null)
                return result;
            if (((p.x - x) * (p.x - x) + (p.y - y) * (p.y - y)) < 100)
                result = p;
            else
            {
                result = FindNode(p.left, x, y);
                if (result == null)
                    result = FindNode(p.right, x, y);
            }

            return result;
        }
        public Node FindNodeVal(int val, Node p)//Поиск по значению
        {
            Node result = null;

            if (p == null)
                return result;
            if (Convert.ToInt32(p.data) == val)
                result = p;
            else
            {
                result = FindNodeVal(val, p.left);
                if (result == null)
                    result = FindNodeVal(val, p.right);
            }

            return result;
        }
        public void Insert (ref Node t,object data, int x, int y)//Вставить узел
        {
            if (t == null)
            {
                t = new Node(null, null, data, x, y);
                t.count++;
            }
            else
            {
                int k = 0;
                foreach (char e in Convert.ToString(data))
                    {
                    k += Convert.ToInt32(e);

                }
                int kt = 0;
                foreach (char e in Convert.ToString(t.data))
                {
                    kt += Convert.ToInt32(e);

                }

                if (k < kt)
                {
                  //  k--;
                    Insert(ref t.left, data, t.x - step, t.y + step);
                    
                }
                else
                    if (k > kt)
                {
                  //  k--;
                    Insert(ref t.right, data, t.x + step, t.y + step);
                    
                }
                else
                    t.count++;
            }

        }
        public void DeSelect(Node p)//Снять выбор
        { }
        public void Delta(Node p, int dx, int dy)//смещение дерева
        {
            p.x -= dx; p.y -= dy;
            if (p.left != null)
                Delta(p.left, dx, dy);
            if (p.right != null)
                Delta(p.right,dx, dy);
        }
        public void Delete(int data, ref Node tree)//Удалить узел
        {
            if (tree!= null)
            {
                if (data < Convert.ToInt32(tree.data))
                    Delete(data, ref tree.left);
                else
                {
                    if (data > Convert.ToInt32(tree.data))
                        Delete(data, ref tree.right);
                    else
                    {
                        q.count--;
                        q = tree;
                        if (q.right == null)
                            tree = q.left;
                        else
                        {
                            if (q.left == null)
                                tree = q.right;
                            else
                                Del(ref q.left);
                        }
                    }
                }
            }
        }
        void Del(ref Node r)
        {
            if (r.right != null)
                Del(ref r.right);
            else
            { q.data = r.data; q = r; r = r.left;}
        }
        public void DrawNode(Node p)//НАрисовать дерево
        {
            try
            {
                int R = 12;
                if (p.left != null)
                    g.DrawLine(MyPen, p.x, p.y, p.left.x, p.left.y);
                if (p.right != null)
                    g.DrawLine(MyPen, p.x, p.y, p.right.x, p.right.y);
                if (p.visit)
                    MyBrush = (SolidBrush)Brushes.Yellow;
                else
                    MyBrush = (SolidBrush)Brushes.LightYellow;
                g.FillEllipse(MyBrush, p.x - R, p.y - R, 2 * R, 2 * R);
                string s = Convert.ToString(p.data)+" : "+ Convert.ToString(p.count);
                SizeF size = g.MeasureString(s, MyFont);
                g.DrawString(s, MyFont, Brushes.Black, p.x - size.Width / 2, p.y - size.Height / 2);
                if (p.left != null)
                    DrawNode(p.left);
                if (p.right != null)
                    DrawNode(p.right);
            }
            catch
            {
                
            }

        }
        public void SetStrSort(Node p)
        {
            if (p == null)
            { }
            if (p.right != null)
            {
                SetStrSort(p.right);
            }
            
            int k = p.count;
            array.Add(Convert.ToString(p.data), k);
            if (p.left != null)
            {
                SetStrSort(p.left);
            }
        }
        public Node Competition(ref Node ph)
        {
            if (ph.left != null)
            {
                if (ph.left.data == ph.data)
                    ph.left = Competition(ref ph.left);
                else
                    if (ph.right != null)
                    ph.right = Competition(ref ph.right);

            }
            else
            {
                if (ph.right != null)
                    ph.right = Competition(ref ph.right);
            }
            if ((ph.left == null) && (ph.right == null))
                ph = null;
            else
            {
                if ((ph.left == null) || ((ph.right != null) && (Convert.ToInt32(ph.left.data) > Convert.ToInt32(ph.right.data))))
                    ph.data = ph.right.data;
                else
                    ph.data = ph.left.data;
                return ph;
            }
            return ph;
        }
    }
}
